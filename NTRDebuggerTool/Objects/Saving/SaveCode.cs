using NTRDebuggerTool.Forms.FormEnums;
using System;

namespace NTRDebuggerTool.Objects.Saving
{
    public class SaveCode
    {
        public DataTypeExact type;
        public String address;

        public SaveCode()
        {
            type = DataTypeExact.Raw;
            address = null;
        }

        public SaveCode(DataTypeExact type, String address)
        {
            this.type = type;
            this.address = address;
        }

        public override string ToString()
        {
            return type + ": " + address;
        }
    }
}
