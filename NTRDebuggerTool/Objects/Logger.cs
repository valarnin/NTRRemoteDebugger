using System;
using System.IO;

namespace NTRDebuggerTool.Objects
{
    static class Logger
    {
        private static string LOG_LOCATION = Config.ConfigFileDirectory + @"\NTRDebuggerTool-Log.txt";

        public static void Log(string msg)
        {
            Log(msg, null);
        }

        public static void Log(string msg, Exception ex)
        {
            File.AppendAllText(LOG_LOCATION, new DateTime().ToString() + " - " + (msg == null ? "null" : msg) + (ex != null ? ex.ToString() : "") + "\n");
        }
    }
}
