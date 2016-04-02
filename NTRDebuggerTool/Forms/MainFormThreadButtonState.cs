using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using System;
using System.Threading;
namespace NTRDebuggerTool.Forms
{
    class MainFormThreadButtonState
    {
        private const uint BUTTON_STATES_START = 0x0010C0B5;
        private const uint BUTTON_STATES_SIZE = 0x00000027;

        private const int Offset_Button = 0x23, Offset_Shoulder = 0x24, Offset_Stick = 0x26, Offset_Horiz = 0x00, Offset_Vert = 0x02;

        public ButtonState ButtonState;
        public ShoulderState ShoulderState;
        public StickState StickState;
        public byte StickHoriz;
        public byte StickVert;
        private MainForm Form;

        public MainFormThreadButtonState(MainForm Form)
        {
            // TODO: Complete member initialization
            this.Form = Form;
        }

        internal void ThreadButtonState()
        {
            int LastSearchTime = 250;
            while (true)
            {
                if (Form.NTRConnection.HardwarePID != null)
                {
                    SearchCriteria Criteria = new SearchCriteria();
                    Criteria.ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Form.NTRConnection.HardwarePID), 0);
                    Criteria.DataType = DataTypeExact.Bytes1;
                    Criteria.StartAddress = BUTTON_STATES_START;
                    Criteria.Length = BUTTON_STATES_SIZE;
                    Criteria.SearchType = SearchTypeBase.Unknown;
                    Criteria.SearchValue = new byte[] { 0 };
                    Criteria.HideSearch = true;
                    Form.NTRConnection.SearchCriteria.Add(Criteria);

                    Form.NTRConnection.SendReadMemoryPacket(Criteria);

                    ButtonState = (ButtonState)Criteria.AddressesFound[BUTTON_STATES_START + Offset_Button][0];
                    ShoulderState = (ShoulderState)Criteria.AddressesFound[BUTTON_STATES_START + Offset_Shoulder][0];
                    StickState = (StickState)Criteria.AddressesFound[BUTTON_STATES_START + Offset_Stick][0];
                    StickHoriz = Criteria.AddressesFound[BUTTON_STATES_START + Offset_Horiz][0];
                    StickVert = Criteria.AddressesFound[BUTTON_STATES_START + Offset_Vert][0];

                    LastSearchTime = Math.Max(LastSearchTime, (int)Criteria.Duration);

                    Form.NTRConnection.SetCurrentOperationText2 = ButtonState.ToString() + "|" + ShoulderState.ToString() + "|" + StickState.ToString() + "|" + StickHoriz + "|" + StickVert + "|" + LastSearchTime;
                }

                Thread.Sleep(LastSearchTime * 2);
            }
        }
    }

    [Flags]
    public enum ButtonState
    {
        A = 0x01,
        B = 0x02,
        Select = 0x04,
        Start = 0x08,
        DPAD_Right = 0x10,
        DPAD_Left = 0x20,
        DPAD_Up = 0x40,
        DPAD_Down = 0x80
    }

    [Flags]
    public enum ShoulderState
    {
        R = 0x01,
        L = 0x02
    }

    [Flags]
    public enum StickState
    {
        LStick_Right = 0x10,
        LStick_Left = 0x20,
        LStick_Up = 0x40,
        LStick_Down = 0x80
    }
}
