using System.Collections.Generic;
using System.Linq;

namespace NTRDebuggerTool.Forms.FormEnums
{
    public static class DataTypeExactTool
    {
        static Dictionary<string, DataTypeExact> Mapping = new Dictionary<string, DataTypeExact>();

        static DataTypeExactTool()
        {
            Mapping.Add("1 Byte", DataTypeExact.Bytes1);
            Mapping.Add("2 Bytes", DataTypeExact.Bytes2);
            Mapping.Add("4 Bytes", DataTypeExact.Bytes4);
            Mapping.Add("8 Bytes", DataTypeExact.Bytes8);
            Mapping.Add("Float", DataTypeExact.Float);
            Mapping.Add("Double", DataTypeExact.Double);
            Mapping.Add("Raw", DataTypeExact.Raw);
            Mapping.Add("Text", DataTypeExact.Text);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static DataTypeExact GetValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || !Mapping.ContainsKey(key)) return DataTypeExact.INVALID;
            return Mapping[key];
        }

        public static string GetKey(DataTypeExact value)
        {
            foreach (string key in Mapping.Keys)
            {
                if (Mapping[key] == value)
                {
                    return key;
                }
            }
            return null;
        }
    }

    public enum DataTypeExact
    {

        INVALID = 0,
        Bytes1 = 1,
        Bytes2 = 2,
        Bytes4 = 3,
        Bytes8 = 4,
        Float = 5,
        Double = 6,
        Raw = 7,
        Text = 8
    }

    public static class DataTypeNumericTool
    {
        static Dictionary<string, DataTypeNumeric> Mapping = new Dictionary<string, DataTypeNumeric>();

        static DataTypeNumericTool()
        {
            Mapping.Add("1 Byte", DataTypeNumeric.Bytes1);
            Mapping.Add("2 Bytes", DataTypeNumeric.Bytes2);
            Mapping.Add("4 Bytes", DataTypeNumeric.Bytes4);
            Mapping.Add("8 Bytes", DataTypeNumeric.Bytes8);
            Mapping.Add("Float", DataTypeNumeric.Float);
            Mapping.Add("Double", DataTypeNumeric.Double);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static DataTypeNumeric GetValue(string key)
        {
            return Mapping[key];
        }
    }

    public enum DataTypeNumeric
    {
        Bytes1 = 1,
        Bytes2 = 2,
        Bytes4 = 3,
        Bytes8 = 4,
        Float = 5,
        Double = 6
    }
}
