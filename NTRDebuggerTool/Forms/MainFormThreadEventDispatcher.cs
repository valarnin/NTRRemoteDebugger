using Newtonsoft.Json;
using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace NTRDebuggerTool.Forms
{
    class MainFormThreadEventDispatcher
    {
        internal bool DispatchConnect = false;
        internal bool DispatchOpenProcess = false;
        internal bool DispatchSearch = false;
        internal bool DispatchConfig = false;
        internal bool DispatchImport = false;
        internal string DispatchPointerSearch = null;
        public ConcurrentQueue<MemoryDispatch> RefreshValueAddresses = new ConcurrentQueue<MemoryDispatch>(),
        RefreshValueReturn = new ConcurrentQueue<MemoryDispatch>(),
        WriteAddress = new ConcurrentQueue<MemoryDispatch>();

        internal GateShark ImportedCode = null;

        internal string CurrentSelectedProcess = "";
        internal string CurrentMemoryRange = "";
        internal DataTypeExact CurrentSelectedDataType;
        internal SearchTypeBase CurrentSelectedSearchType;

        internal string FoundPointerAddress = null;

        private MainForm Form;
        private Regex HexRegex = new Regex("^[0-9A-F]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex ParserRegex = new Regex("\\(\\*(?<Address>.+)\\)(?<Offset>(?:\\[[0-9A-F]+\\])?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Dictionary<uint, uint> Pointers = new Dictionary<uint, uint>();

        internal MainFormThreadEventDispatcher(MainForm Form)
        {
            // TODO: Complete member initialization
            this.Form = Form;
        }

        internal void ThreadEventDispatcher()
        {
            while (true)
            {
                if (DispatchConnect)
                {
                    DispatchConnect = false;
                    DoConnect();
                }
                if (DispatchOpenProcess)
                {
                    DispatchOpenProcess = false;
                    DoOpenProcess();
                }
                if (DispatchSearch)
                {
                    DispatchSearch = false;
                    DoSearch();
                }
                if (DispatchConfig)
                {
                    DispatchConfig = false;
                    DoConfig();
                }
                if (DispatchImport)
                {
                    DispatchImport = false;
                    DoImport();
                }
                if (DispatchPointerSearch != null)
                {
                    string TempAddress = DispatchPointerSearch;
                    DispatchPointerSearch = null;
                    DoPointerSearch(TempAddress);
                }
                while (RefreshValueAddresses.Count > 0)
                {
                    MemoryDispatch Row = new MemoryDispatch();
                    RefreshValueAddresses.TryDequeue(out Row);
                    uint Address;

                    if (HexRegex.IsMatch(Row.TextAddress))
                    {
                        Address = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Row.TextAddress).Reverse().ToArray(), 0); ;
                    }
                    else
                    {
                        Match TopMatch = ParserRegex.Match(Row.TextAddress);

                        if (!TopMatch.Success)
                        {
                            return;
                        }

                        Address = ResolvePointer(TopMatch);
                    }

                    if (Form.IsValidMemoryAddress(Address))
                    {
                        Row.ResolvedAddress = Utilities.GetStringFromByteArray(BitConverter.GetBytes(Address).Reverse().ToArray());
                        Row.Value = GetMemoryAtAddress(CurrentSelectedProcess, Address, Row.Type);
                        RefreshValueReturn.Enqueue(Row);
                    }
                }
                while (WriteAddress.Count > 0)
                {
                    MemoryDispatch Row = new MemoryDispatch();
                    WriteAddress.TryDequeue(out Row);
                    uint Address;

                    if (HexRegex.IsMatch(Row.TextAddress))
                    {
                        Address = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Row.TextAddress).Reverse().ToArray(), 0); ;
                    }
                    else
                    {
                        Match TopMatch = ParserRegex.Match(Row.TextAddress);

                        if (!TopMatch.Success)
                        {
                            return;
                        }

                        Address = ResolvePointer(TopMatch);
                    }

                    if (Form.IsValidMemoryAddress(Address))
                    {
                        Row.ResolvedAddress = Utilities.GetStringFromByteArray(BitConverter.GetBytes(Address).Reverse().ToArray());
                        uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(CurrentSelectedProcess.Split('|')[0]), 0);
                        Form.NTRConnection.SendWriteMemoryPacket(ProcessID, Address, Row.Value);
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void DoPointerSearch(string TempAddress)
        {
            Form.FormEnabled = false;
            PointerScanDialog Dialog = new PointerScanDialog(Form, TempAddress, CurrentSelectedProcess);
            Dialog.ShowDialog();
            FoundPointerAddress = Dialog.PointerFound;
            Dialog.Dispose();
            Form.FormEnabled = true;
        }

        private void DoConfig()
        {
            Form.FormEnabled = false;
            ConfigDialog Dialog = new ConfigDialog(Form);
            Dialog.ShowDialog();
            Dialog.Dispose();
            Form.FormEnabled = true;
        }

        private void DoImport()
        {
            Form.FormEnabled = false;
            GateSharkImportDialog Dialog = new GateSharkImportDialog(Form);
            Dialog.ShowDialog();
            ImportedCode = Dialog.code;
            Dialog.Dispose();
            Form.FormEnabled = true;
        }

        private void DoConnect()
        {
            if (Form.NTRConnection.IsConnected || Form.ButtonConnectDisconnect.Text == "Disconnect")
            {
                Form.NTRConnection.SetCurrentOperationText = "Disconnecting";
                Form.NTRConnection.Disconnect();
                Form.SetConnectedControls(false);
                Form.SetProcessSelectedControls(false);
                Form.SetConnectText = "Connect";
                Form.ControlEnabledButtonConnectDisconnect = true;
                Form.NTRConnection.SetCurrentOperationText = "";
            }
            else
            {
                Form.SetConnectionControls(false);
                Form.NTRConnection.SetCurrentOperationText = "Connecting";
                Form.NTRConnection.IP = Form.IP.Text;
                Form.NTRConnection.Port = short.Parse(Form.Port.Text);

                bool Connected = false;

                for (int i = 0; i < Config.ConnectTries && !Connected; ++i)
                {
                    Connected = Form.NTRConnection.Connect();
                }

                if (Connected)
                {
                    Form.SetConnectText = "Disconnect";
                    Form.NTRConnection.SetCurrentOperationText = "Fetching Processes";
                    Form.NTRConnection.SendListProcessesPacket();
                }
                else
                {
                    Form.SetConnectionControls(true);
                    Form.NTRConnection.SetCurrentOperationText = "";
                }
            }
        }

        private void DoOpenProcess()
        {
            Form.SetConnectedControls(false);
            //File.WriteAllText(, JsonConvert.SerializeObject(NTRConnection.AddressSpaces));

            string cacheFile = PathHelper.BuildFileConfigPath(CurrentSelectedProcess.Split('|')[0], "json", "memrangecache");
            bool hasCacheFile = File.Exists(cacheFile);
            if(hasCacheFile && MessageBox.Show("Found old memory range cache. It's possible to load this one for skipping scan time.\r\nBe sure the ranges didn't change. If you'r not sure don't load!\r\nLoad cache file?","Memory ranges", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                //user wants cache file
                try { 
                Form.NTRConnection.AddressSpaces = JsonConvert.DeserializeObject<Dictionary<uint, uint>>(File.ReadAllText(cacheFile));
                Form.NTRConnection.IsMemoryListUpdated = true;

                }
                catch
                {
                    MessageBox.Show("Exception while loading memory range cache file. Deleting file and fetching live data.");
                    File.Delete(cacheFile);
                    DoOpenProcess();
                    return;
                }
            }
            else
            {
                Form.NTRConnection.SetCurrentOperationText = "Fetching Memory Ranges";
                //Dummy search criteria to prevent operation collision
                while (Form.NTRConnection.SearchCriteria.Count > 0)
                {
                    Thread.Sleep(10);
                }
                Form.NTRConnection.SearchCriteria.Add(new SearchCriteria());
                Form.NTRConnection.SendReadMemoryAddressesPacket(CurrentSelectedProcess.Split('|')[0]);
                while (!Form.NTRConnection.IsMemoryListUpdated && !Form.ControlEnabledButtonOpenProcess)
                {
                    Thread.Sleep(10);
                }
                Form.NTRConnection.SearchCriteria.RemoveAt(0);

            }
        }

        private void DoSearch()
        {
            if (CurrentSelectedSearchType != SearchTypeBase.Same && CurrentSelectedSearchType != SearchTypeBase.Different && CurrentSelectedSearchType != SearchTypeBase.Unknown)
            {
                if (string.IsNullOrWhiteSpace(Form.SearchValue.Text))
                {
                    Form.NTRConnection.SetCurrentOperationText = "Invalid search criteria!";
                    Form.SearchComplete = true;
                    if (Form.LastSearchCriteria == null)
                    {
                        Form.ControlEnabledSearchButton = Form.ControlEnabledDataType = Form.ControlEnabledMemoryRange = true;
                    }
                    return;
                }
                if (CurrentSelectedSearchType == SearchTypeBase.Range && string.IsNullOrWhiteSpace(Form.SearchValue2.Text))
                {
                    Form.NTRConnection.SetCurrentOperationText = "Invalid range criteria!";
                    Form.SearchComplete = true;
                    if (Form.LastSearchCriteria == null)
                    {
                        Form.ControlEnabledSearchButton = Form.ControlEnabledDataType = Form.ControlEnabledMemoryRange = true;
                    }
                    return;
                }
            }
            if (Form.LastSearchCriteria == null)
            {
                Form.LastSearchCriteria = new SearchCriteria();
                Form.LastSearchCriteria.ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(CurrentSelectedProcess.Split('|')[0]), 0);
                Form.LastSearchCriteria.DataType = this.CurrentSelectedDataType;

                if (CurrentMemoryRange.Equals("All"))
                {
                    Form.LastSearchCriteria.StartAddress = Form.LastSearchCriteria.Length = uint.MaxValue;
                }
                else
                {
                    Form.LastSearchCriteria.StartAddress = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Form.MemoryStart.Text).Reverse().ToArray(), 0);
                    Form.LastSearchCriteria.Length = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Form.MemorySize.Text).Reverse().ToArray(), 0);
                }
            }
            Form.LastSearchCriteria.SearchType = this.CurrentSelectedSearchType;

            Form.NTRConnection.SetCurrentOperationText = "Searching Memory";

            string Value1 = Form.SearchValue.Text;

            if (string.IsNullOrWhiteSpace(Value1))
            {
                Value1 = "0";
            }

            Form.LastSearchCriteria.SearchValue = GetValueForDataType(CurrentSelectedDataType, Value1);
            Form.LastSearchCriteria.Size = (uint)Form.LastSearchCriteria.SearchValue.Length;
            if (CurrentSelectedSearchType == SearchTypeBase.Range)
            {
                Form.LastSearchCriteria.SearchValue2 = GetValueForDataType(CurrentSelectedDataType, Form.SearchValue2.Text);
            }

            if (Form.LastSearchCriteria.FirstSearch && CurrentMemoryRange.Equals("All") && Form.LastSearchCriteria.SearchType != SearchTypeBase.Range && Form.LastSearchCriteria.SearchValue.All(x => x.Equals(0)))
            {
                Form.FormEnabled = false;
                DialogResult DialogResult = MessageBox.Show("You're about to search for value 0 (or functional equivalent) across all memory ranges. This operation will take a long time and may cause issues. Are you sure you want to search for this value?", "Warning", MessageBoxButtons.YesNo);
                Form.FormEnabled = true;
                if (DialogResult != DialogResult.Yes)
                {
                    Form.LastSearchCriteria = null;
                    Form.SearchComplete = true;
                    Form.ControlEnabledSearchButton = Form.ControlEnabledDataType = Form.ControlEnabledMemoryRange = true;
                    return;
                }
            }

            Form.NTRConnection.SearchCriteria.Add(Form.LastSearchCriteria);

            Form.NTRConnection.SendReadMemoryPacket(Form.LastSearchCriteria);

            Form.SearchComplete = true;
        }

        private byte[] GetValueForDataType(DataTypeExact CurrentSelectedDataType, string Value)
        {
            switch (CurrentSelectedDataType)
            {
                case DataTypeExact.Bytes1: //1 Byte
                    return new byte[] { (byte)uint.Parse(Value) };
                case DataTypeExact.Bytes2: //2 Bytes
                    return BitConverter.GetBytes(ushort.Parse(Value));
                case DataTypeExact.Bytes4: //4 Bytes
                    return BitConverter.GetBytes(uint.Parse(Value));
                case DataTypeExact.Bytes8: //8 Bytes
                    return BitConverter.GetBytes(ulong.Parse(Value));
                case DataTypeExact.Float: //Float
                    return BitConverter.GetBytes(float.Parse(Value));
                case DataTypeExact.Double: //Double
                    return BitConverter.GetBytes(double.Parse(Value));
                case DataTypeExact.Raw: //Raw Bytes
                    return Utilities.GetByteArrayFromByteString(Value);
                default: //Text
                    return System.Text.Encoding.Default.GetBytes(Value);
            }
        }

        private uint ResolvePointer(Match Match)
        {
            string AddressString = Match.Groups["Address"].Value;
            string OffsetString = Match.Groups["Offset"].Value;


            uint Address;

            Match RecurseMatch = ParserRegex.Match(AddressString);

            if (RecurseMatch.Success)
            {
                Address = ResolvePointer(RecurseMatch);
            }
            else
            {
                uint Pointer = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(AddressString).Reverse().ToArray(), 0);
                if (!Pointers.ContainsKey(Pointer))
                {
                    byte[] Data = GetMemoryAtAddress(CurrentSelectedProcess, AddressString, DataTypeExact.Bytes4);

                    Address = BitConverter.ToUInt32(Data, 0);
                    Pointers[Pointer] = Address;
                }
                else if (!Form.IsValidMemoryAddress(Pointer))
                {
                    return 0;
                }
                else
                {
                    Address = Pointers[Pointer];
                }
            }

            if (Address != 0 && !string.IsNullOrWhiteSpace(OffsetString))
            {
                OffsetString = OffsetString.Replace("[", "").Replace("]", "");
                Address += BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(OffsetString.PadLeft(8, '0')).Reverse().ToArray(), 0);
            }

            return Address;
        }

        internal byte[] GetMemoryAtAddress(string ProcessID, string Address, DataTypeExact DataType)
        {
            return GetMemoryAtAddress(BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(ProcessID), 0), BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Address).Reverse().ToArray(), 0), DataType);
        }

        internal byte[] GetMemoryAtAddress(uint ProcessID, string Address, DataTypeExact DataType)
        {
            return GetMemoryAtAddress(ProcessID, BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Address).Reverse().ToArray(), 0), DataType);
        }

        internal byte[] GetMemoryAtAddress(string ProcessID, uint Address, DataTypeExact DataType)
        {
            return GetMemoryAtAddress(BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(ProcessID), 0), Address, DataType);
        }

        internal byte[] GetMemoryAtAddress(uint ProcessID, uint Address, DataTypeExact DataType)
        {
            SearchCriteria Criteria = new SearchCriteria();
            Criteria.ProcessID = ProcessID;
            Criteria.DataType = DataType;
            Criteria.StartAddress = Address;
            Criteria.Length = Criteria.Size = Form.GetSearchMemorySize(DataType);
            Criteria.SearchType = SearchTypeBase.Unknown;
            Criteria.SearchValue = new byte[] { 0 };
            Form.NTRConnection.SearchCriteria.Add(Criteria);
            Form.NTRConnection.SendReadMemoryPacket(Criteria);
            return Criteria.AddressesFound.Values.First();
        }
    }
}
