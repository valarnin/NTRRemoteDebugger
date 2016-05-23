using NTRDebuggerTool.Forms.FormEnums;

namespace NTRDebuggerTool.Objects
{
    public struct MemoryDispatch
    {
        public int Row;
        public DataTypeExact Type;
        public string TextAddress, ResolvedAddress;
        public byte[] Value;
    }
}
