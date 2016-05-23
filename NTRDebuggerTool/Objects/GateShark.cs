using NTRDebuggerTool.Forms;
using NTRDebuggerTool.Forms.FormEnums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/**
 * This code was pulled from https://github.com/imthe666st/NTRClient/blob/master/ntrclient/Extra/gateshark.cs
 * 
 * Originally written by imthe666st, modified to work with this tool.
 */
namespace NTRDebuggerTool.Objects
{
    public class GateShark
    {
        private readonly List<GatesharkAr> _lines = new List<GatesharkAr>();
        private uint _offset;
        private uint _dxData;
        private bool _loop;
        private int _loopIndex;
        private uint _loopCount;

        private MainForm form;

        private string description;

        public GateShark(MainForm form, string code, string Description)
        {
            this.form = form;
            string[] l = Regex.Split(code, "\r\n|\r|\n");
            foreach (string line in l)
            {
                _lines.Add(new GatesharkAr(line, form));

            }
        }

        public void Execute()
        {
            int index = 0;
            int dummyCount = 0;
            bool gsIf = true;
            bool valid = true;
            int gsIfLayer = 0;
            int gsIfSLayer = 0;
            do
            {
                GatesharkAr gsAr = _lines[index];
                uint cmd = gsAr.GetCmd();

                if (gsIfLayer == 0 && valid)
                {

                    if ((cmd == 0) || (cmd == 1) || (cmd == 2))
                    {
                        valid = gsAr.Execute(_offset);
                    }
                    // Conditional codes
                    else if (cmd == 0x3)
                    {
                        uint read = BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes4), 0);
                        gsIf = read < gsAr.getBlock_B();
                    }
                    else if (cmd == 0x4)
                    {
                        UInt32 r1 = BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes4), 0);
                        UInt32 r2 = Utilities.ReverseEndianness(r1);

                        uint read = Convert.ToUInt32(r2);
                        gsIf = read > gsAr.getBlock_B();
                    }
                    else if (cmd == 0x5)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes4), 0));
                        gsIf = read == gsAr.getBlock_B();
                    }
                    else if (cmd == 0x6)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes4), 0));
                        gsIf = read != gsAr.getBlock_B();
                    }
                    else if (cmd == 0x7)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt16(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes2), 0));
                        gsIf = read < gsAr.getBlock_B();
                    }
                    else if (cmd == 0x8)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt16(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes2), 0));
                        gsIf = read > gsAr.getBlock_B();
                    }
                    else if (cmd == 0x9)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt16(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes2), 0));
                        gsIf = read == gsAr.getBlock_B();
                    }
                    else if (cmd == 0xA)
                    {
                        uint read = Utilities.ReverseEndianness(BitConverter.ToUInt16(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes2), 0));
                        gsIf = read != gsAr.getBlock_B();
                    }
                    else if (cmd == 0xB)
                    {
                        _offset = Utilities.ReverseEndianness(BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, gsAr.getBlock_A() + _offset, DataTypeExact.Bytes4), 0));
                    }
                    else if (cmd == 0xC)
                    {
                        _loop = true;
                        _loopIndex = index;
                        _loopCount = gsAr.getBlock_B() + 1;
                    }
                    else if (cmd == 0xD1)
                    {
                        if (_loop)
                        {
                            _loopCount--;
                            if (_loopCount == 0)
                            {
                                _loop = false;
                            }
                            else
                            {
                                index = _loopIndex;
                                _offset += Convert.ToUInt32(gsAr.getBlock_B());
                            }
                        }
                    }
                    else if (cmd == 0xD2) // RESET
                    {
                        _loop = false;
                        _loopCount = 0;
                        _loopIndex = 0;

                        _offset = 0;
                        gsIfLayer = 0;
                        gsIfSLayer = 0;
                    }
                    else if (cmd == 0xD3) // Read offset
                    {
                        // Fix for Issue #8
                        _offset = gsAr.getBlock_B();
                    }
                    else if (cmd == 0xD4)
                    {
                        // Fix for Issue #8
                        uint b = gsAr.getBlock_B();
                        //int bb;
                        //if (b > int.MaxValue)
                        //{
                        //    // Offset in negative.
                        //    int r = Convert.ToInt32(b % 0x80000000);
                        //    bb = Convert.ToInt32(int.MinValue + r);
                        //}
                        //else
                        //    bb = Convert.ToInt32(b);


                        _dxData += b;
                    }
                    else if (cmd == 0xD5) // DxData WRITE
                    {
                        _dxData = gsAr.getBlock_B();
                    }
                    else if (cmd == 0xD6) // DxData WORD
                    {

                        uint addr = gsAr.getBlock_B() + _offset;
                        _dxData = Utilities.ReverseEndianness(BitConverter.ToUInt32(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, addr, DataTypeExact.Bytes4), 0));
                    }
                    else if (cmd == 0xD7) // DxData SHORT
                    {
                        uint addr = gsAr.getBlock_B() + _offset;
                        _dxData = Utilities.ReverseEndianness(BitConverter.ToUInt16(form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, addr, DataTypeExact.Bytes2), 0));
                    }
                    else if (cmd == 0xD8) // DxData Byte
                    {
                        uint addr = gsAr.getBlock_B() + _offset;
                        _dxData = form.ThreadEventDispatcher.GetMemoryAtAddress(form.ThreadEventDispatcher.CurrentSelectedProcess, addr, DataTypeExact.Bytes1)[0];
                    }
                    else if (cmd == 0xD9) // DxData READ WORD
                    {
                        uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(form.ThreadEventDispatcher.CurrentSelectedProcess.Split('|')[0]), 0);
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, gsAr.getBlock_B() + _offset, _dxData);
                        _offset += 4;
                    }
                    else if (cmd == 0xDA) // DxData READ SHORT
                    {
                        uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(form.ThreadEventDispatcher.CurrentSelectedProcess.Split('|')[0]), 0);
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, gsAr.getBlock_B() + _offset, (ushort)_dxData);
                        _offset += 2;
                    }
                    else if (cmd == 0xDB) // DxData READ BYTE
                    {
                        uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(form.ThreadEventDispatcher.CurrentSelectedProcess.Split('|')[0]), 0);
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, gsAr.getBlock_B() + _offset, (byte)_dxData);
                        _offset += 1;
                    }
                    else if (cmd == 0xDC)
                    {
                        // Fix for Issue #8
                        uint b = gsAr.getBlock_B();
                        //int bb;
                        //if (b > int.MaxValue)
                        //{
                        //    // Offset in negative.
                        //    int r = Convert.ToInt32(b % 0x80000000);
                        //    bb = Convert.ToInt32(int.MinValue + r);
                        //}
                        //else
                        //    bb = Convert.ToInt32(b);


                        _offset += b;
                    }
                    else if (cmd == 0xDF)
                    {
                        // This doesn't actually exist! It's for testing only!
                        dummyCount++;
                        MessageBox.Show(string.Format(
                            "I: {0} \r\n" +
                            "O: {1:X} \r\n" +
                            "LOOP: {2} {3} {4} \r\n" +
                            "DX: {5:X} \r\n" +
                            "DUMMY: {6}\r\n" +
                            "LAYERS: {7} {8}"
                            , index, _offset, _loop, _loopIndex, _loopCount, _dxData, dummyCount, gsIfSLayer, gsIfLayer));
                    }

                    if (!gsIf)
                    {
                        gsIf = true;
                        gsIfLayer += 1;
                    }
                }
                else if (cmd >= 0x3 && cmd <= 0xA)
                {
                    gsIfSLayer += 1;
                }
                else if (cmd == 0xD0)
                {
                    if (gsIfSLayer > 0)
                        gsIfSLayer -= 1;
                    else if (gsIfLayer > 0)
                        gsIfLayer -= 1;
                }
                else if (cmd == 0xD2)
                {
                    _loop = false;
                    _loopCount = 0;
                    _loopIndex = 0;

                    _offset = 0;
                    gsIfLayer = 0;
                    gsIfSLayer = 0;
                }
                index++;
            } while (index < _lines.Count);
        }

        public GatesharkAr[] GetAllCodes()
        {
            return _lines.ToArray();
        }

        public override string ToString()
        {
            return description;
        }
    }

    public class GatesharkAr
    {
        public string Line { get; private set; }
        private readonly uint _cmd;
        private readonly uint _blockA;
        private readonly uint _blockB;
        public string Replace { get; private set; }

        private MainForm form;

        public GatesharkAr(string ar, MainForm form)
        {
            this.form = form;
            Line = ar;
            string[] blocks = ar.Split(' ');

            if (ar.Length != 17)
            {
                _cmd = 0xff;
                _blockA = 0x0fffffff;
                _blockB = 0x7fffffff;
                return;
            }
            Replace = ar.Replace(" ", string.Empty);
            _cmd = Convert.ToUInt32(ar[0].ToString(), 16);
            /*
            0   Write   Word
            1   Write   Short
            2   Write   Byte
            3   X > Y
            4   X < Y
            5   X = Y
            6   X ~ Y
            B   OFFSET = READ(X)
            D3  OFFSET = X
            */

            if (_cmd == 0xD)
            {
                _cmd = 0xD0;
                _cmd += Convert.ToUInt32(ar[1].ToString(), 16); // DX codes
                _blockA = 0x0;
            }
            else
            {
                _blockA = Convert.ToUInt32(blocks[0], 16);
                _blockA -= _cmd * 0x10000000;
            }


            _blockB = Convert.ToUInt32(blocks[1], 16);

        }

        public uint GetCmd()
        {
            return _cmd;
        }

        public uint getBlock_A()
        {
            return _blockA;
        }

        public uint getBlock_B()
        {
            return _blockB;
        }

        public bool Execute(uint offset)
        {
            if ((_cmd == 0) || (_cmd == 1) || (_cmd == 2))
            {
                if (form.IsValidMemoryAddress(_blockA + offset))
                {
                    uint ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(form.ThreadEventDispatcher.CurrentSelectedProcess.Split('|')[0]), 0);
                    if (_cmd == 2)
                    {
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, _blockA + offset, (byte)_blockB);
                    }
                    else if (_cmd == 1)
                    {
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, _blockA + offset, (ushort)_blockB);
                    }
                    else
                    {
                        form.NTRConnection.SendWriteMemoryPacket(ProcessID, _blockA + offset, _blockB);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}