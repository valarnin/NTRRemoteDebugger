using System;
using System.Linq;

namespace NTRDebuggerTool
{
    static class Utilities
    {
        public static byte[] GetByteArrayFromByteString(string Text)
        { 
            uint v = uint.Parse(Text, System.Globalization.NumberStyles.HexNumber);
            return BitConverter.GetBytes(v).Reverse().ToArray();
        }

        public static string GetStringFromByteArray(byte[] Buffer)
        {
            return BitConverter.ToString(Buffer).Replace("-", "");
        }

        public static uint ReverseEndianness(uint Value)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(Value).Reverse().ToArray(), 0);
        }

        public static ushort ReverseEndianness(ushort Value)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes(Value).Reverse().ToArray(), 0);
        }
    }
}
