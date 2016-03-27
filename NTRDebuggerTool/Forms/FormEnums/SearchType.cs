using System.Collections.Generic;
using System.Linq;

namespace NTRDebuggerTool.Forms.FormEnums
{

    public static class SearchTypeInitialTool
    {
        static Dictionary<string, SearchTypeInitial> Mapping = new Dictionary<string, SearchTypeInitial>();

        static SearchTypeInitialTool()
        {
            Mapping.Add("Exact", SearchTypeInitial.Exact);
            Mapping.Add("Range", SearchTypeInitial.Range);
            Mapping.Add("Unknown", SearchTypeInitial.Unknown);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static SearchTypeInitial GetValue(string key)
        {
            return Mapping[key];
        }
    }

    public enum SearchTypeInitial
    {
        Exact = 1,
        Range = 2,
        Unknown = 7
    }

    public static class SearchTypeNumericTool
    {
        static Dictionary<string, SearchTypeNumeric> Mapping = new Dictionary<string, SearchTypeNumeric>();

        static SearchTypeNumericTool()
        {
            Mapping.Add("Exact", SearchTypeNumeric.Exact);
            Mapping.Add("Range", SearchTypeNumeric.Range);
            Mapping.Add("IncreasedBy", SearchTypeNumeric.IncreasedBy);
            Mapping.Add("DecreasedBy", SearchTypeNumeric.DecreasedBy);
            Mapping.Add("Increased", SearchTypeNumeric.Increased);
            Mapping.Add("Decreased", SearchTypeNumeric.Decreased);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static SearchTypeNumeric GetValue(string key)
        {
            return Mapping[key];
        }
    }

    public enum SearchTypeNumeric
    {
        Exact = 1,
        Range = 2,
        IncreasedBy = 3,
        DecreasedBy = 4,
        Increased = 5,
        Decreased = 6
    }

    public static class SearchTypeTextTool
    {
        static Dictionary<string, SearchTypeText> Mapping = new Dictionary<string, SearchTypeText>();

        static SearchTypeTextTool()
        {
            Mapping.Add("Exact", SearchTypeText.Exact);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static SearchTypeText GetValue(string key)
        {
            return Mapping[key];
        }
    }

    public enum SearchTypeText
    {
        Exact = 1
    }

    public static class SearchTypeBaseTool
    {
        static Dictionary<string, SearchTypeBase> Mapping = new Dictionary<string, SearchTypeBase>();

        static SearchTypeBaseTool()
        {
            Mapping.Add("Exact", SearchTypeBase.Exact);
            Mapping.Add("Range", SearchTypeBase.Range);
            Mapping.Add("IncreasedBy", SearchTypeBase.IncreasedBy);
            Mapping.Add("DecreasedBy", SearchTypeBase.DecreasedBy);
            Mapping.Add("Increased", SearchTypeBase.Increased);
            Mapping.Add("Decreased", SearchTypeBase.Decreased);
            Mapping.Add("Unknown", SearchTypeBase.Unknown);
        }

        public static string[] GetValues()
        {
            return Mapping.Keys.ToArray();
        }

        public static SearchTypeBase GetValue(string key)
        {
            return Mapping[key];
        }
    }

    public enum SearchTypeBase
    {
        Exact = 1,
        Range = 2,
        IncreasedBy = 3,
        DecreasedBy = 4,
        Increased = 5,
        Decreased = 6,
        Unknown = 7
    }
}
