using NTRDebuggerTool.Remote;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NTRDebuggerTool.Forms
{
    public partial class MainForm : Form
    {
        internal bool ControlEnabledButtonConnectDisconnect = true;
        private bool ControlEnabledIP = true;
        private bool ControlEnabledPort = true;

        private bool ControlEnabledProcesses = false;
        private bool ControlEnabledButtonOpenProcess = false;

        private bool ControlEnabledMemoryRange = false;
        private bool ControlEnabledButtonRefreshMemoryRange = false;
        private bool ControlEnabledButtonAddResults = false;
        private bool ControlEnabledResetButton = false;
        private bool ControlEnabledResultsGrid = false;
        internal bool ControlEnabledSearchButton = false;
        private bool ControlEnabledSearchType = false;
        private bool ControlEnabledSearchValue = false;
        private bool ControlEnabledValuesGrid = false;

        private bool ControlEnabledStart = false;
        private bool ControlEnabledSize = false;

        private Thread EventDispatcherThread;

        internal string SetConnectText = null;

        internal NTRRemoteConnection NTRConnection;

        private MainFormThreadEventDispatcher ThreadEventDispatcher;

        public MainForm(NTRRemoteConnection NTRConnection)
        {
            InitializeComponent();
            this.NTRConnection = NTRConnection;
            this.ThreadEventDispatcher = new MainFormThreadEventDispatcher(this);
            this.EventDispatcherThread = new Thread(new ThreadStart(this.ThreadEventDispatcher.ThreadEventDispatcher));
            this.EventDispatcherThread.Name = "EventDispatcherThread";
            this.EventDispatcherThread.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchType.SelectedIndex = 2;
        }

        private void UpdateLockedControls()
        {
            this.ButtonAddResults.Enabled = !NTRConnection.LockControls && this.ControlEnabledButtonAddResults;
            this.ButtonConnectDisconnect.Enabled = !NTRConnection.LockControls && this.ControlEnabledButtonConnectDisconnect;
            this.ButtonOpenProcess.Enabled = !NTRConnection.LockControls && this.ControlEnabledButtonOpenProcess;
            this.ButtonRefreshMemoryRange.Enabled = !NTRConnection.LockControls && this.ControlEnabledButtonRefreshMemoryRange;
            this.IP.Enabled = !NTRConnection.LockControls && this.ControlEnabledIP;
            this.MemoryRange.Enabled = !NTRConnection.LockControls && this.ControlEnabledMemoryRange;
            this.Port.Enabled = !NTRConnection.LockControls && this.ControlEnabledPort;
            this.Processes.Enabled = !NTRConnection.LockControls && this.ControlEnabledProcesses;
            this.ResetButton.Enabled = !NTRConnection.LockControls && this.ControlEnabledResetButton;
            this.ResultsGrid.Enabled = !NTRConnection.LockControls && this.ControlEnabledResultsGrid;
            this.SearchButton.Enabled = !NTRConnection.LockControls && this.ControlEnabledSearchButton;
            this.SearchType.Enabled = !NTRConnection.LockControls && this.ControlEnabledSearchType;
            this.SearchValue.Enabled = !NTRConnection.LockControls && this.ControlEnabledSearchValue;
            this.ValuesGrid.Enabled = !NTRConnection.LockControls && this.ControlEnabledValuesGrid;
            this.MemoryStart.Enabled = !NTRConnection.LockControls && this.ControlEnabledStart;
            this.MemorySize.Enabled = !NTRConnection.LockControls && this.ControlEnabledSize;
        }

        internal void SetConnectedControls(bool Enabled)
        {
            ControlEnabledProcesses = Enabled;
            ControlEnabledButtonOpenProcess = Enabled;
        }

        internal void SetProcessSelectedControls(bool Enabled)
        {
            ControlEnabledMemoryRange = Enabled;
            ControlEnabledButtonRefreshMemoryRange = Enabled;
            ControlEnabledButtonAddResults = Enabled;
            ControlEnabledResetButton = Enabled;
            ControlEnabledResultsGrid = Enabled;
            ControlEnabledSearchButton = Enabled;
            ControlEnabledSearchType = Enabled;
            ControlEnabledSearchValue = Enabled;
            ControlEnabledValuesGrid = Enabled;
        }

        internal void SetConnectionControls(bool Enabled)
        {
            ControlEnabledButtonConnectDisconnect = Enabled;
            ControlEnabledIP = Enabled;
            ControlEnabledPort = Enabled;
        }

        private void GUIUpdateTimer_Tick(object sender, EventArgs e)
        {
            NTRConnection.SendHeartbeatPacket();

            if (NTRConnection.ProgressReadMax > 0)
            {
                ProgressBarMemoryRead.Maximum = (int)NTRConnection.ProgressReadMax;
                ProgressBarMemoryRead.Value = (int)NTRConnection.ProgressRead;
            }
            else
            {
                ProgressBarMemoryRead.Maximum = ProgressBarMemoryRead.Value = 0;
            }

            if (NTRConnection.ProgressScanMax > 0)
            {
                ProgressBarMemoryScan.Maximum = (int)NTRConnection.ProgressScanMax;
                ProgressBarMemoryScan.Value = (int)NTRConnection.ProgressScan;
            }
            else
            {
                ProgressBarMemoryScan.Maximum = ProgressBarMemoryScan.Value = 0;
            }

            if (NTRConnection.IsProcessListUpdated)
            {
                NTRConnection.IsProcessListUpdated = false;
                string CurrentProcess = Processes.SelectedValue == null ? null : Processes.SelectedValue.ToString();
                Processes.Items.Clear();
                Processes.Items.AddRange(NTRConnection.Processes.ToArray());
                if (CurrentProcess != null && Processes.Items.Contains(CurrentProcess))
                {
                    Processes.SelectedValue = CurrentProcess;
                    Processes.SelectedIndex = Processes.Items.IndexOf(CurrentProcess);
                }
                SetConnectedControls(true);
                ControlEnabledButtonConnectDisconnect = true;
                NTRConnection.SetCurrentOperationText = "";
            }

            if (NTRConnection.IsMemoryListUpdated)
            {
                NTRConnection.IsMemoryListUpdated = false;
                string CurrentRange = MemoryRange.SelectedValue == null ? "All" : MemoryRange.SelectedValue.ToString();
                MemoryRange.Items.Clear();
                MemoryRange.Items.Add("All");
                foreach (uint Start in NTRConnection.AddressSpaces.Keys)
                {
                    MemoryRange.Items.Add(Utilities.GetStringFromByteArray(BitConverter.GetBytes(Start).Reverse().ToArray()) + "|" + Utilities.GetStringFromByteArray(BitConverter.GetBytes(NTRConnection.AddressSpaces[Start]).Reverse().ToArray()));
                }
                MemoryRange.Items.Add("Custom");
                if (CurrentRange != null && MemoryRange.Items.Contains(CurrentRange))
                {
                    MemoryRange.SelectedValue = CurrentRange;
                    MemoryRange.SelectedIndex = MemoryRange.Items.IndexOf(CurrentRange);
                }
                SetProcessSelectedControls(true);
                SetConnectedControls(true);
                NTRConnection.SetCurrentOperationText = "";
            }

            if (NTRConnection.IsMemorySearchFinished)
            {
                NTRConnection.IsMemorySearchFinished = false;
                LabelResults.Text = "Results: " + NTRConnection.AddressesFound.Count;

                if (NTRConnection.AddressesFound.Count < 50)
                {
                    ResultsGrid.Rows.Clear();
                    foreach (uint Address in NTRConnection.AddressesFound)
                    {
                        int Row = ResultsGrid.Rows.Add();
                        ResultsGrid[0, Row].Value = Utilities.GetStringFromByteArray(BitConverter.GetBytes(Address).Reverse().ToArray());
                        ResultsGrid[1, Row].Value = SearchValue.Text;
                    }
                }
                NTRConnection.SetCurrentOperationText = "";
            }

            if (SetConnectText != null)
            {
                ButtonConnectDisconnect.Text = SetConnectText;
                SetConnectText = null;
            }

            LabelCurrentOperation.Text = NTRConnection.SetCurrentOperationText;

            UpdateLockedControls();

            if (NTRConnection.IsConnected)
            {
                for (int i = 0; i < ValuesGrid.Rows.Count; ++i)
                {
                    if (ValuesGrid[0, i].Value is string)
                    {
                        SetMemory(i);
                    }
                }
            }
        }

        internal uint GetSearchMemorySize()
        {
            switch (ThreadEventDispatcher.CurrentSelectedDataType)
            {
                case 0: //1 Byte
                    return 1;
                case 1: //2 Bytes
                    return 2;
                case 3: //8 Bytes
                    return 8;
                case 4: //Float
                    return (uint)BitConverter.GetBytes(float.MinValue).Length;
                case 5: //Double
                    return (uint)BitConverter.GetBytes(double.MinValue).Length;
                case 6: //Raw Bytes
                    return (uint)Utilities.GetByteArrayFromByteString(SearchValue.Text).Length;
                case 2: //4 Bytes
                default:
                    return 4;
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            NTRConnection.AddressesFound.Clear();
            ResultsGrid.Rows.Clear();
            ControlEnabledSearchType = ControlEnabledMemoryRange = true;
            if (MemoryRange.SelectedIndex > 0)
            {
                ControlEnabledStart = ControlEnabledSize = true;
            }

            LabelResults.Text = "Results: ";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NTRConnection.Disconnect();
            this.EventDispatcherThread.Abort();
        }

        private void ButtonAddResults_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow Row in ResultsGrid.SelectedRows)
            {
                if (!IsInValues((string)Row.Cells[0].Value))
                {
                    int RowIndex = ValuesGrid.Rows.Add();
                    ValuesGrid[0, RowIndex].Value = null;
                    ValuesGrid[1, RowIndex].Value = Row.Cells[0].Value;
                    ValuesGrid[2, RowIndex].Value = SearchValue.Text;
                    ValuesGrid[3, RowIndex].Value = SearchType.Text;
                }
            }
        }

        private bool IsInValues(String Address)
        {
            for (int i = 0; i < ValuesGrid.RowCount; ++i)
            {
                if (((string)ValuesGrid[1, i].Value) == Address)
                {
                    return true;
                }
            }
            return false;
        }

        private void ValuesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                SetMemory(e.RowIndex);
            }
        }

        private void SetMemory(int RowIndex)
        {
            if (IsValidMemoryAddress(BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0)))
            {
                switch ((string)ValuesGrid[3, RowIndex].Value)
                {
                    case "1 Byte": //1 Byte
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            (byte)uint.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "2 Bytes": //2 Bytes
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            ushort.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "4 Bytes": //4 Bytes
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            uint.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "8 Bytes": //8 Bytes
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            ulong.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "Float": //Float
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            float.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "Double": //Double
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                            double.Parse((string)ValuesGrid[2, RowIndex].Value));
                        break;
                    case "Raw Bytes": //Raw Bytes
                        NTRConnection.SendWriteMemoryPacket(
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Processes.Text.Split('|')[0]), 0),
                            BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString((string)ValuesGrid[1, RowIndex].Value).Reverse().ToArray(), 0),
                             Utilities.GetByteArrayFromByteString((string)ValuesGrid[2, RowIndex].Value));
                        break;
                }
            }
        }

        private void ValuesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ValuesGrid.BeginEdit(true);
        }

        private void ValuesGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            ValuesGrid.EndEdit();
        }

        private void MemoryRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MemoryRange.SelectedIndex == 0)
            {
                this.MemoryStart.Text = this.MemorySize.Text = "";
                ControlEnabledStart = ControlEnabledSize = false;
            }
            else if (MemoryRange.SelectedIndex == MemoryRange.Items.Count - 1)
            {
                ControlEnabledStart = ControlEnabledSize = true;
            }
            else
            {
                string[] Parts = MemoryRange.Text.Split('|');
                this.MemoryStart.Text = Parts[0];
                this.MemorySize.Text = Parts[1];
                ControlEnabledStart = ControlEnabledSize = false;
            }
        }

        private void ButtonConnectDisconnect_Click(object sender, EventArgs e)
        {
            ButtonConnectDisconnect.Enabled = ControlEnabledButtonConnectDisconnect = false;
            ThreadEventDispatcher.DispatchConnect = true;
        }

        private void ButtonOpenProcess_Click(object sender, EventArgs e)
        {
            ButtonOpenProcess.Enabled = ControlEnabledButtonOpenProcess = false;
            ThreadEventDispatcher.CurrentSelectedProcess = Processes.Text;
            ThreadEventDispatcher.DispatchOpenProcess = true;
        }

        private void ButtonRefreshMemoryRange_Click(object sender, EventArgs e)
        {
            ButtonRefreshMemoryRange.Enabled = ControlEnabledButtonRefreshMemoryRange = false;
            ThreadEventDispatcher.CurrentSelectedProcess = Processes.Text;
            ThreadEventDispatcher.DispatchOpenProcess = true;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchButton.Enabled = ControlEnabledSearchButton = false;
            ThreadEventDispatcher.CurrentSelectedDataType = SearchType.SelectedIndex;
            ThreadEventDispatcher.CurrentMemoryRange = this.MemoryRange.Text;
            ThreadEventDispatcher.DispatchSearch = true;
        }

        private void ValuesGridContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "ValuesGridDeleteItem":
                    foreach (DataGridViewRow Row in ValuesGrid.SelectedRows)
                    {
                        ValuesGrid.Rows.Remove(Row);
                    }
                    foreach (DataGridViewCell Cell in ValuesGrid.SelectedCells)
                    {
                        ValuesGrid.Rows.Remove(Cell.OwningRow);
                    }
                    break;
                case "ValuesGridAddItem":
                    ValuesGrid.Rows.Add();
                    break;
                case "ValuesGridConvertCode":
                    foreach (DataGridViewRow Row in ValuesGrid.SelectedRows)
                    {
                        Row.Cells[1].Value = ConvertCode((string)Row.Cells[1].Value);
                    }
                    foreach (DataGridViewCell Cell in ValuesGrid.SelectedCells)
                    {
                        Cell.OwningRow.Cells[1].Value = ConvertCode((string)Cell.OwningRow.Cells[1].Value);
                    }
                    break;
            }
        }

        private string ConvertCode(string Address)
        {
            uint OriginalCodeAddress = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Address).Reverse().ToArray(), 0);
            uint ConvertedMemoryAddress = OriginalCodeAddress + 0x14000000u; //High memory region
            if (IsValidMemoryAddress(ConvertedMemoryAddress))
            {
                return Utilities.GetStringFromByteArray(BitConverter.GetBytes(ConvertedMemoryAddress).Reverse().ToArray());
            }
            uint ConversionModifier = GetConversionModifier(ConvertedMemoryAddress);
            if (ConversionModifier == 0)
            {
                return Address;
            }
            uint Offset = ConvertedMemoryAddress - ConversionModifier;
            ConvertedMemoryAddress = 0x08000000 + Offset;
            if (IsValidMemoryAddress(ConvertedMemoryAddress))
            {
                return Utilities.GetStringFromByteArray(BitConverter.GetBytes(ConvertedMemoryAddress).Reverse().ToArray());
            }
            ConvertedMemoryAddress = 0x00100000 + Offset;
            if (IsValidMemoryAddress(ConvertedMemoryAddress))
            {
                return Utilities.GetStringFromByteArray(BitConverter.GetBytes(ConvertedMemoryAddress).Reverse().ToArray());
            }
            return Address;
        }

        private bool IsValidMemoryAddress(uint Address)
        {
            foreach (uint RangeStart in NTRConnection.AddressSpaces.Keys)
            {
                if (Address >= RangeStart && Address <= (RangeStart + NTRConnection.AddressSpaces[RangeStart]))
                {
                    return true;
                }
            }
            return false;
        }

        private uint GetConversionModifier(uint Address)
        {
            uint[] Keys = NTRConnection.AddressSpaces.Keys.ToArray();
            Array.Sort(Keys);
            foreach (uint RangeStart in Keys.Reverse())
            {
                if (RangeStart <= Address)
                {
                    //This is ugly.
                    return BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Utilities.GetStringFromByteArray(BitConverter.GetBytes(RangeStart + NTRConnection.AddressSpaces[RangeStart]).Reverse().ToArray()).Substring(0, 3).PadRight(8, '0')).Reverse().ToArray(), 0) + 0x00100000u;
                }
            }
            return 0;
        }

    }
}
