using NTRDebuggerTool.Forms.FormEnums;
using System;

namespace NTRDebuggerTool.Objects.Saving
{
    public class SaveCode
    {
        public DataTypeExact type;
        public String address;
        public String title;

        public SaveCode()
        {
            type = DataTypeExact.Raw;
            address = null;
            title = null;
        }

        public SaveCode(DataTypeExact type, String address, String title)
        {
            this.type = type;
            this.address = address;
            this.title = title;
        }

        public override string ToString()
        {
            return $"[{title}] {type}: {address}";
        }
    }
}
