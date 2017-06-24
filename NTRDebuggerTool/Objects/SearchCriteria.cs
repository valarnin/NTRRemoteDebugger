using NTRDebuggerTool.Forms.FormEnums;
using System.Collections.Generic;

namespace NTRDebuggerTool.Objects
{
    public struct SearchBlock_s
    {
        public uint StartAddress;
        public uint EndAddress;
        public uint Size;
        public int AddressSpace;
        public List<uint> AddressList;
    }
    public class SearchCriteria
    {
        public uint Duration;
        public uint ProcessID;
        public uint StartAddress, Length;
        public uint Size;
        public SearchTypeBase SearchType;
        public DataTypeExact DataType;
        public byte[] SearchValue;
        public byte[] SearchValue2;

        public SearchBlock_s SearchBlock = new SearchBlock_s();
        
        public bool SearchComplete = false;
        public bool AllSearchesComplete = false;

        public bool FirstSearch = true;
        public bool HideSearch = false;

        public Dictionary<uint, byte[]> AddressesFound = new Dictionary<uint, byte[]>();
    }
}
