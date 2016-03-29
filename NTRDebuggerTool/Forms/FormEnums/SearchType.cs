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
        Unknown = 9
    }

    public static class SearchTypeNumericTool
    {
        static Dictionary<string, SearchTypeNumeric> Mapping = new Dictionary<string, SearchTypeNumeric>();

        static SearchTypeNumericTool()
        {
            Mapping.Add("Exact", SearchTypeNumeric.Exact);
            Mapping.Add("Range", SearchTypeNumeric.Range);
            Mapping.Add("Increased By", SearchTypeNumeric.IncreasedBy);
            Mapping.Add("Decreased By", SearchTypeNumeric.DecreasedBy);
            Mapping.Add("Increased", SearchTypeNumeric.Increased);
            Mapping.Add("Decreased", SearchTypeNumeric.Decreased);
            Mapping.Add("Same", SearchTypeNumeric.Same);
            Mapping.Add("Different", SearchTypeNumeric.Different);
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
        Decreased = 6,
        Same = 7,
        Different = 8
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
        Exact = 1,
        Same = 7,
        Different = 8
    }

    public static class SearchTypeBaseTool
    {
        static Dictionary<string, SearchTypeBase> Mapping = new Dictionary<string, SearchTypeBase>();

        static SearchTypeBaseTool()
        {
            Mapping.Add("Exact", SearchTypeBase.Exact);
            Mapping.Add("Range", SearchTypeBase.Range);
            Mapping.Add("Increased By", SearchTypeBase.IncreasedBy);
            Mapping.Add("Decreased By", SearchTypeBase.DecreasedBy);
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
        Same = 7,
        Different = 8,
        Unknown = 9
    }
}
