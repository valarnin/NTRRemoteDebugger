using NTRDebuggerTool.Forms;
using NTRDebuggerTool.Remote;
using System;
using System.IO;
using System.Net;
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
            try
            {
                File.Open(Path.GetTempPath() + "3dsreleases.xml", FileMode.Open).Close();
            }
            catch (FileNotFoundException ex)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile("http://3dsdb.com/xml.php", Path.GetTempPath() + "3dsreleases.xml");
                    }
                }
                catch (Exception e)
                {
                    System.Console.Write(e);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new NTRRemoteConnection()));
        }
    }
}
