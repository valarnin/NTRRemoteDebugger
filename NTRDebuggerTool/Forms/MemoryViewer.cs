using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTRDebuggerTool.Objects;
using Be.Windows.Forms;
using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects.Saving;

namespace NTRDebuggerTool.Forms
{
    public partial class MemoryViewer : Form
    {
        public class HexboxProvider : IByteProvider
        {
            public byte[] Data;

            public HexboxProvider(uint buffersize)
            {
                Data = new byte[buffersize];
            }
            public long Length => Data.Length;

            public event EventHandler LengthChanged;
            public event EventHandler Changed;

            public void ApplyChanges()
            {
                throw new NotImplementedException();
            }


            public bool HasChanges()
            {
                throw new NotImplementedException();
            }

            #region unsupported
            public void InsertBytes(long index, byte[] bs)
            {
                throw new NotImplementedException();
            }
            public void DeleteBytes(long index, long length)
            {
                throw new NotImplementedException();
            }
            #endregion
            public byte ReadByte(long index) => Data[index];
            public bool SupportsDeleteBytes() => false;
            public bool SupportsInsertBytes() => false;
            public bool SupportsWriteByte() => true;

            public void WriteByte(long index, byte value)
            {

                Data[index] = value;
                // Changed?.Invoke(this, null);
            }
        }
        MainFormThreadEventDispatcher ted;
        HexboxProvider hexProvider;


        uint rangeBothDirections = 512;
        uint centerAdress;
        uint startingAddress
        {
            get
            {
                uint tmp = centerAdress - rangeBothDirections;
                while (tmp % 8 != 0) tmp++; //be sure to keep a clear 8-byte offset
                return tmp;
            }
        }
        uint endAddress { get => startingAddress + rangeBothDirections * 2; }


        internal MemoryViewer(MainFormThreadEventDispatcher ted, string address)
        {
            InitializeComponent();
            this.ted = ted;
            this.centerAdress = uint.Parse(address, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture);
        }

        private void MemoryViewer_Load(object sender, EventArgs e)
        {
            hexProvider = new HexboxProvider(rangeBothDirections * 2);
            hexEditBox.ByteProvider = hexProvider;
            hexEditBox.LineInfoOffset = startingAddress;
            hexEditBox.GroupSeparatorVisible = false;
            comboSelType.Items.AddRange(DataTypeExactTool.GetValues());
            hexEditBox.BytesPerLine = 16;
        }

        private void OnReadProgress(uint bytesRead, uint bytesTotal)
        {
            pBar.Minimum = 0;
            pBar.Maximum = (int)bytesTotal;
            pBar.Value = (int)bytesRead;
        }
        private void ScanAndFillRange(uint dataOffset, uint subrangeStart, uint subrangeEnd)
        {
            //prepare range in blocks of 8
            uint blocksNeeded = (uint)Math.Ceiling((subrangeEnd - subrangeStart) / 8d);
            byte[,] cache = new byte[blocksNeeded, 8];

            //now fill cache
            for (uint i = 0; i < blocksNeeded; i++)
            {
                byte[] tmp = ted.GetMemoryAtAddress(ted.CurrentSelectedProcess, subrangeStart + (i * 8), DataTypeExact.Bytes8);
                for (uint ii = 0; ii < 8; ii++) cache[i, ii] = tmp[ii];
                this.Invoke(new MethodInvoker(() => OnReadProgress(i, blocksNeeded)));
            }

            //now set the data array accordingly

            for (uint iBlock = 0; iBlock < cache.GetLength(0); iBlock++)
            {
                for (uint iByte = 0; iByte < cache.GetLength(1); iByte++) hexProvider.Data[dataOffset + (8 * iBlock) + iByte] = cache[iBlock, iByte];
            }

            hexEditBox.Refresh();
            hexEditBox.Select(dataOffset, subrangeEnd - subrangeStart);
        }
        private void ScanAndFillArray()
        {

            for (uint i = 0; i < rangeBothDirections * 2; i = i + 8)
            {
                uint cAddr = startingAddress + i;
                byte[] tmp = ted.GetMemoryAtAddress(ted.CurrentSelectedProcess, cAddr, DataTypeExact.Bytes8);
                for (int ii = 0; ii < 8; ii++) hexProvider.Data[i + ii] = tmp[ii];
                this.Invoke(new MethodInvoker(() => OnReadProgress(i, rangeBothDirections * 2)));
            }
            hexEditBox.Refresh();
            hexEditBox.Select(rangeBothDirections, 1);
        }
        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            ScanAndFillArray();
        }


        private void btnReloadSelection_Click(object sender, EventArgs e)
        {

            long viewOffsetStart = hexEditBox.SelectionStart;
            long viewOffsetLength = hexEditBox.SelectionLength;
            uint rangeStart = startingAddress + (uint)viewOffsetStart;
            uint rangeEnd = rangeStart + (uint)viewOffsetLength - 1;
            ScanAndFillRange((uint)viewOffsetStart, rangeStart, rangeEnd);
        }

        uint _selectedAddress;
        uint selectedAddress
        {
            get
            {
                return _selectedAddress;
            }
            set
            {
                _selectedAddress = value;
                lblSelAddr.Text = "0x" + _selectedAddress.ToString("x2");
            }
        }
        private void hexEditBox_SelectionStartChanged(object sender, EventArgs e)
        {
            selectedAddress = (uint)(((HexBox)sender).SelectionStart + startingAddress);

        }
        private void RefreshDataGrid()
        {
            wantedAddresses.Rows.Clear();
            ResultingCodes.ForEach((sc) =>
            {
                int RowIndex = wantedAddresses.Rows.Add();
                wantedAddresses[0, RowIndex].Value = sc.address;
                wantedAddresses[1, RowIndex].Value = sc.title;
                wantedAddresses[2, RowIndex].Value = DataTypeExactTool.GetKey(sc.type);
            });
            wantedAddresses.Refresh();

        }
        public List<SaveCode> ResultingCodes { get; private set; } = new List<SaveCode>();
        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            DataTypeExact addrType = DataTypeExactTool.GetValue(comboSelType.Text);
            if (addrType == DataTypeExact.INVALID) return;
            String addrStr = selectedAddress.ToString("x2").PadLeft(8, '0');
            int existingAddressIdx = ResultingCodes.FindIndex(c => c.address == addrStr);
            if (existingAddressIdx != -1) ResultingCodes[existingAddressIdx] = new SaveCode(addrType, addrStr, txtSelTitle.Text);
            else ResultingCodes.Add(new SaveCode(DataTypeExactTool.GetValue(comboSelType.Text), addrStr, txtSelTitle.Text));
            RefreshDataGrid();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < wantedAddresses.SelectedRows.Count; i++)
            {
                string addr = wantedAddresses[0, i].Value.ToString();
                ResultingCodes.RemoveAll((sc) => sc.address == addr);
            }
            RefreshDataGrid();
        }
    }
}
