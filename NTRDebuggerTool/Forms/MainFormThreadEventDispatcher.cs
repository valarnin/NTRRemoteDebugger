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
        internal int CurrentSelectedDataType = 2;
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
                if (Form.NTRConnection.Connect())
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
            Form.NTRConnection.SendReadMemoryAddressesPacket(CurrentSelectedProcess.Split('|')[0]);
        }

        private void DoSearch()
        {
            Form.NTRConnection.SetCurrentOperationText = "Searching Memory";
            uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(CurrentSelectedProcess.Split('|')[0]), 0);
            uint StartAddress, MemorySize;
            if (CurrentMemoryRange.Equals("All"))
            {
                StartAddress = MemorySize = uint.MaxValue;
            }
            else
            {
                StartAddress = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Form.MemoryStart.Text).Reverse().ToArray(), 0);
                MemorySize = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Form.MemorySize.Text).Reverse().ToArray(), 0);
            }

            if (Form.ResultsGrid.Rows.Count > 0 || Form.NTRConnection.AddressesFound.Count > 0)
            {
                MemorySize = Form.GetSearchMemorySize();
            }

            switch (CurrentSelectedDataType)
            {
                case 0: //1 Byte
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, (byte)uint.Parse(Form.SearchValue.Text));
                    break;
                case 1: //2 Bytes
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, ushort.Parse(Form.SearchValue.Text));
                    break;
                case 2: //4 Bytes
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, uint.Parse(Form.SearchValue.Text));
                    break;
                case 3: //8 Bytes
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, ulong.Parse(Form.SearchValue.Text));
                    break;
                case 4: //Float
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, float.Parse(Form.SearchValue.Text));
                    break;
                case 5: //Double
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, double.Parse(Form.SearchValue.Text));
                    break;
                case 6: //Raw Bytes
                    Form.NTRConnection.SendReadMemoryPacket(ProcessID, StartAddress, MemorySize, Utilities.GetByteArrayFromByteString(Form.SearchValue.Text));
                    break;
            }
            Form.ControlEnabledSearchButton = true;
        }
    }
}
