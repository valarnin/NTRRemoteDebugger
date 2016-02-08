using System;
using System.Linq;

namespace NTRDebuggerTool
{
    static class Utilities
    {
        public static byte[] GetByteArrayFromByteString(string Text)
        {
            return Enumerable.Range(0, Text.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(Text.Substring(x, 2), 16))
                     .ToArray();
        }

        public static string GetStringFromByteArray(byte[] Buffer)
        {
            return BitConverter.ToString(Buffer).Replace("-", "");
        }

    }
}
