using System;

namespace NTRDebuggerTool.Objects
{
    class GateSharkCode
    {
        public GateSharkCode(string code)
        {

        }

        #region Pulled from http://gbatemp.net/threads/i-need-help-understanding-these-d-code-lines-please.417985/#post-6151997

        private enum OperationType : uint
        {
            Write4Byte = 0x00000000,
            Write2Byte = 0x10000000,
            Write1Byte = 0x20000000,
            ConditionalGreaterThan4Byte = 0x30000000,
            ConditionalLessThan4Byte = 0x40000000,
            ConditionalEqual4Byte = 0x50000000,
            ConditionalNotEqual4Byte = 0x60000000,
            ConditionalGreaterThan2Byte = 0x70000000,
            ConditionalLessThan2Byte = 0x80000000,
            ConditionalEqual2Byte = 0x90000000,
            ConditionalNotEqual2Byte = 0xA0000000,
            LoadOffset = 0xB0000000,
            WriteRange = 0xC0000000,
            DTypeTest = 0xD0000000,
            EndConditional = 0xD0000000,
            EndRepeat = 0xD1000000,
            ResetState = 0xD2000000,
            LoadPointer = 0xD3000000,
            AddToLoadedPointer = 0xD4000000,
            SetLoadedPointer = 0xD5000000,
            Unknown14Byte = 0xD6000000,
            Unknown12Byte = 0xD7000000,
            Unknown11Byte = 0xD8000000,
            LoadValue4Byte = 0xD9000000,
            LoadValue2Byte = 0xDA000000,
            LoadValue1Byte = 0xDB000000,
            Unknown24Byte = 0xD0000000,
            ButtonStateRequire = 0xDD000000,
            WriteRegion = 0xE0000000,
            UnusedFCode = 0xF0000000
        }

        [Flags]
        private enum GateSharkButtonState
        {
            A = 0x00000001,
            B = 0x00000002,
            Select = 0x00000004,
            Start = 0x00000008,
            Right = 0x00000010,
            Left = 0x00000020,
            Up = 0x00000040,
            Down = 0x00000080,
            R = 0x00000100,
            L = 0x00000200,
            X = 0x00000400,
            Y = 0x00000800,
        }

        #endregion
    }
}
