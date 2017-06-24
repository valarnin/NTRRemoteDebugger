using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTRDebuggerTool.Objects
{
    static class PathHelper
    {
        public static String RemoveInvalidFileNameChars(string input) => new String(input.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) || ch == ' ' ? '_' : ch).ToArray());
        public static String BuildFileConfigPath(string input, string extension="json", string suffix = null)
        {
            return string.IsNullOrWhiteSpace(input) ? null : Path.Combine(Config.ConfigFileDirectory, RemoveInvalidFileNameChars(input) + (suffix != null ? "_" + RemoveInvalidFileNameChars(suffix) : "") + @"." + RemoveInvalidFileNameChars(extension));
        }
    }
}
