using NTRDebuggerTool.Forms;
using NTRDebuggerTool.Remote;
using System;
using System.Windows.Forms;

namespace NTRDebuggerTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new NTRRemoteConnection()));
        }
    }
}
