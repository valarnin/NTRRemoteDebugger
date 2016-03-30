using System.Collections.Generic;
using System.Windows.Forms;

namespace NTRDebuggerTool.Forms
{
    public partial class ConfigDialog : Form
    {
        public ConfigDialog()
        {
            InitializeComponent();
            Dictionary<string, string> ConfigValues = Config.All;
            foreach (string Key in ConfigValues.Keys)
            {
                int Row = ConfigDataGrid.Rows.Add();
                ConfigDataGrid[0, Row].Value = Key;
                ConfigDataGrid[1, Row].Value = ConfigValues[Key];
            }
        }

        private void ConfigDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ConfigDataGrid.BeginEdit(true);
        }

        private void ConfigDataGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            ConfigDataGrid.EndEdit();
        }

        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            Dictionary<string, string> ConfigValues = new Dictionary<string, string>();
            for (int Row = 0; Row < ConfigDataGrid.RowCount; ++Row)
            {
                ConfigValues.Add(ConfigDataGrid[0, Row].Value.ToString(), ConfigDataGrid[1, Row].Value.ToString());
            }
            Config.All = ConfigValues;
            Close();
        }
    }
}
