using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml;

namespace NTRDebuggerTool.Remote
{
    class NTRPacketReceiverThread
    {
        private NTRRemoteConnection NTRConnection;

        private uint DataRead = 0;

        internal NTRPacketReceiverThread(NTRRemoteConnection NTRConnection)
        {
            this.NTRConnection = NTRConnection;
        }

        #region Receiving Packets

        internal void ThreadReceivePackets()
        {
            byte[] Buffer = new byte[84];
            uint[] Arguments = new uint[16];
            uint Magic, Sequence, Type, Command;

            while (true)
            {
                try
                {
                    Array.Clear(Buffer, 0, Buffer.Length);
                    int PacketSize = ReadBasePacket(Buffer, true);
                    if (PacketSize == 0)
                    {
                        break;
                    }
                    if (PacketSize < Buffer.Length && ShouldSkipPacket(Buffer))
                    {
                        continue;
                    }
                    int t = 0;
                    Magic = BitConverter.ToUInt32(Buffer, t);
                    Sequence = BitConverter.ToUInt32(Buffer, t += 4);
                    Type = BitConverter.ToUInt32(Buffer, t += 4);
                    Command = BitConverter.ToUInt32(Buffer, t += 4);
                    for (int i = 0; i < Arguments.Length; i++)
                    {
                        Arguments[i] = BitConverter.ToUInt32(Buffer, t += 4);
                    }
                    uint ExtraDataLength = BitConverter.ToUInt32(Buffer, t += 4);

                    if (Magic != 0x12345678)
                    {
                        break;
                    }

                    if (ExtraDataLength > 0)
                    {
                        ReadExtraData(Command, Sequence, ExtraDataLength);
                    }

                    if (Type == PacketType.General && Command == PacketCommand.Heartbeat)
                    {
                        this.NTRConnection.CanSendHeartbeat = true;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(null, e);
                    break;
                }
            }

            this.NTRConnection.Disconnect();
        }

        private bool ShouldSkipPacket(byte[] Buffer)
        {
            if (System.Text.Encoding.Default.GetString(Buffer).Equals("finished")) //Finshed writing memory
            {
                return true;
            }
            return false;
        }

        int ReadBasePacket(byte[] Buffer)
        {
            return ReadBasePacket(Buffer, false);
        }

        int ReadBasePacket(byte[] Buffer, bool MainLoop)
        {
            int Read = 0;
            int Position = this.NTRConnection.Client.GetStream().Read(Buffer, 0, Buffer.Length);
            if (Position == 0)
            {
                return 0;
            }
            if (MainLoop)
            {
                if (ShouldSkipPacket(Buffer))
                {
                    return Read;
                }
            }
            while (Position < Buffer.Length)
            {
                if (this.NTRConnection.ProgressReadMax > 0)
                {
                    this.NTRConnection.ProgressRead = (uint)Position;
                }
                Read = this.NTRConnection.Client.GetStream().Read(Buffer, Position, Buffer.Length - Position);
                if (Read == 0)
                {
                    return 0;
                }
                Position += Read;
                DataRead = (uint)Position;
            }
            DataRead = (uint)Position;
            return Position;
        }

        private void ReadExtraData(uint Command, uint Sequence, uint DataLength)
        {
            if (Sequence == this.NTRConnection.LastListProcessesSequence + 1000)
            {
                ReadProcessesPacket(DataLength);
            }
            else if (Sequence == this.NTRConnection.LastListMemoryRegionsSequence + 1000)
            {
                ReadAddressesPacket(DataLength);
            }
            else if (Sequence == this.NTRConnection.LastReadMemorySequence) //For some reason, not +1000?
            {
                ReadMemoryPacket(DataLength);
            }
            else
            {
                switch (Command)
                {
                    default:
                        ReadBasePacket(new byte[DataLength]);
                        break;
                }
            }
        }

        private void ReadProcessesPacket(uint DataLength)
        {
            this.NTRConnection.Processes.Clear();
            byte[] Buffer = new byte[DataLength];
            ReadBasePacket(Buffer);
            string BufferText = System.Text.Encoding.Default.GetString(Buffer);
            BufferText = BufferText.Replace("rtRecvSocket failed: 00000000", "");
            //Line format:
            //pid: 0x00000029, pname:       ro, tid: 0004013000003702, kpobj: fff7b5f8
            //Split on comma, then split KV on `: `, trim both key and value
            foreach (string Line in BufferText.Split('\n'))
            {
                if (!Line.StartsWith("pid"))
                {
                    continue;
                }
                string[] KVStrings = Line.Split(',');
                string ProcessID = KVStrings[0].Split(new String[] { ": " }, StringSplitOptions.None)[1].Trim().Substring(2);
                string ProcessName = KVStrings[1].Split(new String[] { ": " }, StringSplitOptions.None)[1].Trim();
                string TitleID = KVStrings[2].Split(new String[] { ": " }, StringSplitOptions.None)[1].Trim();
                XmlNode Node = null;
                if (ProcessName.Equals("hid"))
                {
                    NTRConnection.HardwarePID = ProcessID;
                }

                if (NTRConnection.ReleasesDocument != null)
                {
                    Node = NTRConnection.ReleasesDocument.DocumentElement.SelectSingleNode("/releases/release[translate(./titleid, 'ABCDEF', 'abcdef') = '" + TitleID.ToLower() + "']/name");
                }

                if (Node != null)
                {
                    this.NTRConnection.Processes.Add(ProcessID + "|" + Node.InnerText);
                }
                else
                {
                    this.NTRConnection.Processes.Add(ProcessID + "|" + ProcessName + "," + TitleID);
                }
            }

            if (this.NTRConnection.Processes.Count > 0)
            {
                //Bubble processes we ID'd to the top
                List<string> TempProcesses = this.NTRConnection.Processes.FindAll(x => x.Contains(','));
                this.NTRConnection.Processes.RemoveAll(x => TempProcesses.Contains(x));
                foreach (string Process in TempProcesses)
                {
                    this.NTRConnection.Processes.Add(Process);
                }

                this.NTRConnection.IsProcessListUpdated = true;
            }
        }

        private void ReadAddressesPacket(uint DataLength)
        {
            Dictionary<uint, uint> AddressSpaces = new Dictionary<uint, uint>();
            byte[] Buffer = new byte[DataLength];
            ReadBasePacket(Buffer);
            string BufferText = System.Text.Encoding.Default.GetString(Buffer);
            BufferText = BufferText.Replace("rtRecvSocket failed: 00000000", "");
            //Line format:
            //00100000 - 0093cfff , size: 0083d000
            //Split on comma, then split on ` - ` for address start and `: ` for size
            foreach (string Line in BufferText.Split('\n'))
            {
                if (!Line.Contains("size"))
                {
                    continue;
                }
                string[] Parts = Line.Split(new String[] { " , " }, StringSplitOptions.None);
                string StartString = Parts[0].Split(new String[] { " - " }, StringSplitOptions.None)[0].Trim().PadLeft(8, '0');
                string SizeString = Parts[1].Split(new String[] { ": " }, StringSplitOptions.None)[1].Trim().PadLeft(8, '0');
                uint Start = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(StartString).Reverse().ToArray(), 0);
                uint Size = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(SizeString).Reverse().ToArray(), 0);

                AddressSpaces.Add(Start, Size);
            }

            if (AddressSpaces.Count > 0)
            {
                this.NTRConnection.AddressSpaces = new Dictionary<uint, uint>(AddressSpaces);

                this.NTRConnection.IsMemoryListUpdated = true;
            }
        }

        private void SearchThroughSearchBlock()
        {
            //Console.WriteLine("SearchThroughSearchBlock");
            uint    PatternSize = NTRConnection.SearchCriteria[0].Size;
            uint    BufferSize = NTRConnection.SearchCriteria[0].SearchBlock.Size;
            byte[]  Buffer = new byte[BufferSize];
            byte[]  TemporaryBuffer = new byte[PatternSize];
            int     i;
            uint     offset;
            uint     target;
            int     targetCount = NTRConnection.SearchCriteria[0].SearchBlock.AddressList.Count;

            //this.NTRConnection.SetCurrentOperationText = "Get data through network";
            ReadBasePacket(Buffer);
           // this.NTRConnection.SetCurrentOperationText = "Scan data";
            Thread MemoryScanThread = new Thread(delegate()
            {
                for (i = 0; i < targetCount; i++)
                {
                    target = NTRConnection.SearchCriteria[0].SearchBlock.AddressList.ElementAt(i);
                    offset = target - NTRConnection.SearchCriteria[0].SearchBlock.StartAddress;
                    this.NTRConnection.ProgressScan = offset;
                    if (NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(target))
                    {
                        Array.Copy(Buffer, offset, TemporaryBuffer, 0, TemporaryBuffer.Length);
                        if (CheckCriteria(target, TemporaryBuffer))
                        {
                            NTRConnection.SearchCriteria[0].AddressesFound.Remove(target);
                            NTRConnection.SearchCriteria[0].AddressesFound.Add(target, (byte[])TemporaryBuffer.Clone());
                        }
                        else
                        {
                            NTRConnection.SearchCriteria[0].AddressesFound.Remove(target);
                        }
                    }
                }
            });

            MemoryScanThread.Start();
            MemoryScanThread.Join();
        }

        private void ReadMemoryPacket(uint DataLength)
        {
            this.NTRConnection.ProgressReadMax = this.NTRConnection.ProgressScanMax = DataLength;
            if (DataLength < NTRConnection.SearchCriteria[0].Size)
            {
                NTRConnection.SearchCriteria[0].SearchComplete = true;
                return;
            }
            if (NTRConnection.SearchCriteria[0].SearchBlock.Size > 0)
            {
                SearchThroughSearchBlock();
                goto exit;
            }
            uint    PatternSize = NTRConnection.SearchCriteria[0].Size;
            byte[]  Buffer = new byte[DataLength];
            byte[]  TemporaryBuffer = new byte[PatternSize];

            DataRead = 0;
           // Console.WriteLine("ReadMemoryPacket({0:X}), patternSize: {1:X}", DataLength, PatternSize);
            Thread MemoryScanThread = new Thread(delegate()
            {
                uint RealAddress;
                for (uint i = 0; i <= DataLength - PatternSize; ++i)
                {
                    if (DataRead < i + PatternSize)
                    {
                        --i;
                        Thread.Sleep(50);
                        continue;
                    }
                    this.NTRConnection.ProgressScan = i;
                    RealAddress = (uint)(NTRConnection.SearchCriteria[0].StartAddress + i);
                    if (NTRConnection.SearchCriteria[0].FirstSearch || NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        Array.Copy(Buffer, i, TemporaryBuffer, 0, TemporaryBuffer.Length);
                        if (CheckCriteria(RealAddress, TemporaryBuffer))
                        {
                            NTRConnection.SearchCriteria[0].AddressesFound.Remove(RealAddress);
                            NTRConnection.SearchCriteria[0].AddressesFound.Add(RealAddress, (byte[])TemporaryBuffer.Clone());
                        }
                        else
                        {
                            NTRConnection.SearchCriteria[0].AddressesFound.Remove(RealAddress);
                        }
                    }
                }
            });

            MemoryScanThread.Start();

            ReadBasePacket(Buffer);

            MemoryScanThread.Join();
exit:
            DataRead = 0;
            
            this.NTRConnection.SetCurrentOperationText = "Scanning Read Memory";

            NTRConnection.SearchCriteria[0].SearchComplete = true;
            this.NTRConnection.ProgressReadMax = this.NTRConnection.ProgressScanMax = this.NTRConnection.ProgressRead = this.NTRConnection.ProgressScan = 0;
        }

        #endregion

        #region ReadMemoryPacket Helpers

        private bool CheckCriteria(uint RealAddress, byte[] RemoteValue)
        {
            switch (NTRConnection.SearchCriteria[0].SearchType)
            {
                case SearchTypeBase.Exact:
                    return Enumerable.SequenceEqual(NTRConnection.SearchCriteria[0].SearchValue, RemoteValue);
                case SearchTypeBase.Range:
                    IComparable valc = GetValueFromByteArray(RemoteValue);
                    IComparable vall = GetValueFromByteArray(NTRConnection.SearchCriteria[0].SearchValue);
                    IComparable valh = GetValueFromByteArray(NTRConnection.SearchCriteria[0].SearchValue2);
                    if (vall.CompareTo(valh) > 0)
                    {
                        IComparable tmp = vall;
                        vall = valh;
                        valh = tmp;
                    }
                    return vall.CompareTo(valc) <= 0 && valc.CompareTo(valh) <= 0;
                case SearchTypeBase.IncreasedBy:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return IsIncreasedBy(RealAddress, RemoteValue);
                    return GetValueFromByteArray(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) == BitConverter.ToUInt32(NTRConnection.SearchCriteria[0].SearchValue, 0);
                case SearchTypeBase.DecreasedBy:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return IsDecreasedBy(RealAddress, RemoteValue);
                case SearchTypeBase.Increased:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) < 0;
                case SearchTypeBase.Decreased:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) > 0;
                case SearchTypeBase.Same:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) == 0;
                case SearchTypeBase.Different:
                    if (!NTRConnection.SearchCriteria[0].AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) != 0;
                case SearchTypeBase.Unknown:
                    return true;
                default:
                    throw new InvalidOperationException("Invalid search type " + NTRConnection.SearchCriteria[0].SearchType.ToString() + " passed to NTRPacketReceiverThread.CheckCriteria");
            }
        }

        private IComparable GetValueFromByteArray(byte[] Value)
        {
            switch (NTRConnection.SearchCriteria[0].DataType)
            {
                case DataTypeExact.Bytes1:
                    return Value[0];
                case DataTypeExact.Bytes2:
                    return BitConverter.ToUInt16(Value, 0);
                case DataTypeExact.Bytes4:
                    return BitConverter.ToUInt32(Value, 0);
                case DataTypeExact.Bytes8:
                    return BitConverter.ToUInt64(Value, 0);
                case DataTypeExact.Float:
                    return BitConverter.ToSingle(Value, 0);
                case DataTypeExact.Double:
                    return BitConverter.ToDouble(Value, 0);
                default:
                    throw new InvalidOperationException("Invalid data type " + NTRConnection.SearchCriteria[0].DataType.ToString() + " passed to NTRPacketReceiverThread.GetValueFromByteArray");
            }
        }

        private bool IsIncreasedBy(uint RealAddress, byte[] RemoteValue)
        {
            checked
            {
                switch (NTRConnection.SearchCriteria[0].DataType)
                {
                    case DataTypeExact.Bytes1:
                        return NTRConnection.SearchCriteria[0].AddressesFound[RealAddress][0] ==
                            RemoteValue[0] - NTRConnection.SearchCriteria[0].SearchValue[0];
                    case DataTypeExact.Bytes2:
                        return BitConverter.ToUInt16(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt16(RemoteValue, 0) - BitConverter.ToUInt16(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Bytes4:
                        return BitConverter.ToUInt32(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt32(RemoteValue, 0) - BitConverter.ToUInt32(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Bytes8:
                        return BitConverter.ToUInt64(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt64(RemoteValue, 0) - BitConverter.ToUInt64(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Float:
                        return IsLessThan(BitConverter.ToSingle(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0),
                            BitConverter.ToSingle(RemoteValue, 0) + BitConverter.ToSingle(NTRConnection.SearchCriteria[0].SearchValue, 0));
                    case DataTypeExact.Double:
                        return IsLessThan(BitConverter.ToDouble(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0),
                            BitConverter.ToDouble(RemoteValue, 0) + BitConverter.ToDouble(NTRConnection.SearchCriteria[0].SearchValue, 0));
                    default:
                        throw new InvalidOperationException("Invalid data type " + NTRConnection.SearchCriteria[0].DataType.ToString() + " passed to NTRPacketReceiverThread.GetValueFromByteArray");
                }
            }
        }

        private bool IsDecreasedBy(uint RealAddress, byte[] RemoteValue)
        {
            checked
            {
                switch (NTRConnection.SearchCriteria[0].DataType)
                {
                    case DataTypeExact.Bytes1:
                        return NTRConnection.SearchCriteria[0].AddressesFound[RealAddress][0] ==
                            RemoteValue[0] + NTRConnection.SearchCriteria[0].SearchValue[0];
                    case DataTypeExact.Bytes2:
                        return BitConverter.ToUInt16(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt16(RemoteValue, 0) + BitConverter.ToUInt16(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Bytes4:
                        return BitConverter.ToUInt32(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt32(RemoteValue, 0) + BitConverter.ToUInt32(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Bytes8:
                        return BitConverter.ToUInt64(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0) ==
                            BitConverter.ToUInt64(RemoteValue, 0) + BitConverter.ToUInt64(NTRConnection.SearchCriteria[0].SearchValue, 0);
                    case DataTypeExact.Float:
                        return IsGreaterThan(BitConverter.ToSingle(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0),
                            BitConverter.ToSingle(RemoteValue, 0) + BitConverter.ToSingle(NTRConnection.SearchCriteria[0].SearchValue, 0));
                    case DataTypeExact.Double:
                        return IsGreaterThan(BitConverter.ToDouble(NTRConnection.SearchCriteria[0].AddressesFound[RealAddress], 0),
                            BitConverter.ToDouble(RemoteValue, 0) + BitConverter.ToDouble(NTRConnection.SearchCriteria[0].SearchValue, 0));
                    default:
                        throw new InvalidOperationException("Invalid data type " + NTRConnection.SearchCriteria[0].DataType.ToString() + " passed to NTRPacketReceiverThread.GetValueFromByteArray");
                }
            }
        }

        #endregion

        #region Handle comparison of precision numbers (float, double)

        private bool IsLessThan(float Left, float Right)
        {
            checked
            {
                return Left < Right + float.Epsilon;
            }
        }
        private bool IsGreaterThan(float Left, float Right)
        {
            checked
            {
                return Left + float.Epsilon > Right;
            }
        }
        private bool IsLessThan(double Left, double Right)
        {
            checked
            {
                return Left < Right + double.Epsilon;
            }
        }
        private bool IsGreaterThan(double Left, double Right)
        {
            checked
            {
                return Left + double.Epsilon > Right;
            }
        }

        #endregion
    }
}
