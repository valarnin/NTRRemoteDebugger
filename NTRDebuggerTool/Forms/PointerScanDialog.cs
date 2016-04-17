using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NTRDebuggerTool.Forms
{
    public partial class PointerScanDialog : Form
    {
        MainForm MainForm;

        internal string PointerFound = null;
        internal string ProcessID;

        public PointerScanDialog(MainForm MainForm, string Address, string ProcessID)
        {
            InitializeComponent();
            TextAddress.Text = Address;
            TextMaxOffset.Text = "1000";
            this.MainForm = MainForm;
            this.ProcessID = ProcessID;
        }

        private void ResultsDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PointerFound = "(*" + (string)ResultsDataGrid[0, e.RowIndex].Value + ")[" + (string)ResultsDataGrid[1, e.RowIndex].Value + "]";
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            uint Pointer = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(TextAddress.Text).Reverse().ToArray(), 0);
            SearchCriteria Criteria = new SearchCriteria();
            Criteria.ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(ProcessID), 0);
            Criteria.DataType = DataTypeExact.Bytes4;
            if (CheckFullSearch.Checked)
            {
                Criteria.StartAddress = Criteria.Length = uint.MaxValue;
                Criteria.Length = 4;
            }
            else
            {
                KeyValuePair<uint, uint> KVP = MainForm.GetAddressSpaceForAddress(Pointer).Value;
                Criteria.StartAddress = KVP.Key;
                Criteria.Length = KVP.Value;
            }
            Criteria.Size = 4;
            Criteria.SearchType = SearchTypeBase.Range;
            Criteria.SearchValue = BitConverter.GetBytes(Pointer - BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(TextMaxOffset.Text.PadLeft(8, '0')).Reverse().ToArray(), 0));
            Criteria.SearchValue2 = BitConverter.GetBytes(Pointer);
            MainForm.NTRConnection.SearchCriteria.Add(Criteria);
            MainForm.NTRConnection.SendReadMemoryPacket(Criteria);
            foreach (var a in Criteria.AddressesFound.OrderByDescending(x => BitConverter.ToUInt32(x.Value, 0)).ThenBy(x => x.Key))
            {
                int RowIndex = ResultsDataGrid.Rows.Add();
                ResultsDataGrid[0, RowIndex].Value = Utilities.GetStringFromByteArray(BitConverter.GetBytes(a.Key).Reverse().ToArray());
                ResultsDataGrid[1, RowIndex].Value = Utilities.GetStringFromByteArray(BitConverter.GetBytes(Pointer - BitConverter.ToUInt32(a.Value, 0)).Reverse().ToArray()).TrimStart('0');
            }
        }
    }
}
