using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using System;
using System.Linq;
using System.Threading;

namespace NTRDebuggerTool.Forms
{
    class MainFormThreadEventDispatcher
    {
        internal bool DispatchConnect = false;
        internal bool DispatchOpenProcess = false;
        internal bool DispatchSearch = false;

        internal string CurrentSelectedProcess = "";
        internal string CurrentMemoryRange = "";
        internal DataTypeExact CurrentSelectedDataType;
        internal SearchTypeBase CurrentSelectedSearchType;
        private MainForm Form;

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

                Thread.Sleep(100);
            }
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

                for (int i = 0; i < 5 && !Connected; ++i)
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

        private void DoSearch()
        {
            if (CurrentSelectedSearchType != SearchTypeBase.Same && CurrentSelectedSearchType != SearchTypeBase.Different && CurrentSelectedSearchType != SearchTypeBase.Unknown)
            {
                if (string.IsNullOrWhiteSpace(Form.SearchValue.Text))
                {
                    Form.NTRConnection.SetCurrentOperationText = "Invalid search criteria!";
                    return;
                }
                if (CurrentSelectedSearchType == SearchTypeBase.Range && string.IsNullOrWhiteSpace(Form.SearchValue2.Text))
                {
                    Form.NTRConnection.SetCurrentOperationText = "Invalid range criteria!";
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
            if (CurrentSelectedSearchType == SearchTypeBase.Range)
            {
                Form.LastSearchCriteria.SearchValue2 = GetValueForDataType(CurrentSelectedDataType, Form.SearchValue2.Text);
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
    }
}
