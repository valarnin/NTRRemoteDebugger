using NTRDebuggerTool.Forms.FormEnums;
using System.Collections.Generic;

namespace NTRDebuggerTool.Objects
{
    public class SearchCriteria
    {
        public uint ProcessID;
        public uint StartAddress, Length;
        public SearchTypeBase SearchType;
        public DataTypeExact DataType;
        public byte[] SearchValue;
        public byte[] SearchValue2;

        public bool SearchComplete = false;
        public bool AllSearchesComplete = false;

        public bool FirstSearch = true;

        public Dictionary<uint, byte[]> AddressesFound = new Dictionary<uint, byte[]>();
    }
}
