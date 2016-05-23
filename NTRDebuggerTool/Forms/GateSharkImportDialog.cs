using NTRDebuggerTool.Objects;
using System.Windows.Forms;

namespace NTRDebuggerTool.Forms
{
    public partial class GateSharkImportDialog : Form
    {
        MainForm Form;

        internal GateShark code = null;

        public GateSharkImportDialog(MainForm Form)
        {
            this.Form = Form;
            InitializeComponent();
        }


        private void ButtonCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ButtonImport_Click(object sender, System.EventArgs e)
        {
            code = new GateShark(Form, CodeTextBox.Text, DescriptionTextBox.Text);
            Close();
        }
    }
}
