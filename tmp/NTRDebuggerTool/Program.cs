using NTRDebuggerTool.Forms;
using NTRDebuggerTool.Remote;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace NTRDebuggerTool
{
    static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, bargs) =>
            {
                String dllName = new AssemblyName(bargs.Name).Name + ".dll";
                var assem = Assembly.GetExecutingAssembly();
                String resourceName = assem.GetManifestResourceNames().FirstOrDefault(rn => rn.EndsWith(dllName));
                if (resourceName == null) return null; // Not found, maybe another handler will find it
                using (var stream = assem.GetManifestResourceStream(resourceName))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        public const bool DEBUG = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (DEBUG || (args.Length > 0 && args[0].Equals("-c")))
                {
                    ConsoleHelper.EnableConsole();
                }
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
                        //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(null, ex);
                        //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(null, e);
                    }
                }

                //Debug code
                if (!DEBUG)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(new NTRRemoteConnection()));
                }
                else
                {
                    Debug.Execute();
                }
            }
            catch (Exception ex)
            {
                //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(null, ex);
                MessageBox.Show("An exception has occurred. Check the log file at " + System.IO.Path.GetTempPath() + System.IO.Path.DirectorySeparatorChar + "NTRDebuggerTool-Log.txt");
                Application.Exit();
            }
        }
    }
}
