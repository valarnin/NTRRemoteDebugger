using NTRDebuggerTool.Forms.FormEnums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace NTRDebuggerTool.Remote
{
    class NTRPacketReceiverThread
    {
        private NTRRemoteConnection NTRConnection;

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
                    System.Console.WriteLine(e);
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
            }
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
                uint Start = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Parts[0].Split(new String[] { " - " }, StringSplitOptions.None)[0].Trim()).Reverse().ToArray(), 0);
                uint Size = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Parts[1].Split(new String[] { ": " }, StringSplitOptions.None)[1].Trim()).Reverse().ToArray(), 0);

                AddressSpaces.Add(Start, Size);
            }

            if (AddressSpaces.Count > 0)
            {
                this.NTRConnection.AddressSpaces = new ReadOnlyDictionary<uint, uint>(AddressSpaces);

                this.NTRConnection.IsMemoryListUpdated = true;
            }
        }

        private void ReadMemoryPacket(uint DataLength)
        {
            this.NTRConnection.ProgressReadMax = this.NTRConnection.ProgressScanMax = DataLength;
            if (DataLength < this.NTRConnection.SearchCriteria.SearchValue.Length)
            {
                this.NTRConnection.SearchCriteria.SearchComplete = true;
                return;
            }

            byte[] Buffer = new byte[DataLength];
            byte[] TemporaryBuffer = new byte[this.NTRConnection.SearchCriteria.SearchValue.Length];
            ReadBasePacket(Buffer);

            this.NTRConnection.SetCurrentOperationText = "Scanning Read Memory";

            uint RealAddress;

            for (uint i = 0; i <= DataLength - this.NTRConnection.SearchCriteria.SearchValue.Length; ++i)
            {
                this.NTRConnection.ProgressScan = i;
                RealAddress = (uint)(this.NTRConnection.SearchCriteria.StartAddress + i);
                if (this.NTRConnection.SearchCriteria.FirstSearch || this.NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                {
                    Array.Copy(Buffer, i, TemporaryBuffer, 0, TemporaryBuffer.Length);
                    if (!CheckCriteria(RealAddress, TemporaryBuffer))
                    {
                        this.NTRConnection.SearchCriteria.AddressesFound.Remove(RealAddress);
                    }
                    else if (this.NTRConnection.SearchCriteria.FirstSearch && !this.NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        this.NTRConnection.SearchCriteria.AddressesFound.Add(RealAddress, (byte[])TemporaryBuffer.Clone());
                    }
                }
            }

            this.NTRConnection.SearchCriteria.SearchComplete = true;
            this.NTRConnection.ProgressReadMax = this.NTRConnection.ProgressScanMax = this.NTRConnection.ProgressRead = this.NTRConnection.ProgressScan = 0;
        }

        #endregion

        #region ReadMemoryPacket Helpers

        private bool CheckCriteria(uint RealAddress, byte[] RemoteValue)
        {
            switch (NTRConnection.SearchCriteria.SearchType)
            {
                case SearchTypeBase.Exact:
                    return Enumerable.SequenceEqual(this.NTRConnection.SearchCriteria.SearchValue, RemoteValue);
                case SearchTypeBase.Range:
                    IComparable valc = GetValueFromByteArray(RemoteValue);
                    IComparable vall = GetValueFromByteArray(NTRConnection.SearchCriteria.SearchValue);
                    IComparable valh = GetValueFromByteArray(NTRConnection.SearchCriteria.SearchValue2);
                    if (vall.CompareTo(valh) > 0)
                    {
                        IComparable tmp = vall;
                        vall = valh;
                        valh = tmp;
                    }
                    return vall.CompareTo(valc) <= 0 && valc.CompareTo(valh) <= 0;
                case SearchTypeBase.IncreasedBy:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) == BitConverter.ToUInt32(NTRConnection.SearchCriteria.SearchValue, 0);
                case SearchTypeBase.DecreasedBy:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(RemoteValue).CompareTo(GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress])) == BitConverter.ToUInt32(NTRConnection.SearchCriteria.SearchValue, 0);
                case SearchTypeBase.Increased:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) > 0;
                case SearchTypeBase.Decreased:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) < 0;
                case SearchTypeBase.Same:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) == 0;
                case SearchTypeBase.Different:
                    if (!NTRConnection.SearchCriteria.AddressesFound.ContainsKey(RealAddress))
                    {
                        return false;
                    }
                    return GetValueFromByteArray(NTRConnection.SearchCriteria.AddressesFound[RealAddress]).CompareTo(GetValueFromByteArray(RemoteValue)) != 0;
                case SearchTypeBase.Unknown:
                    return true;
                default:
                    throw new InvalidOperationException("Invalid search type " + NTRConnection.SearchCriteria.SearchType.ToString() + " passed to NTRPacketReceiverThread.CheckCriteria");
            }


        }

        private IComparable GetValueFromByteArray(byte[] Value)
        {
            switch (NTRConnection.SearchCriteria.DataType)
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
                    throw new InvalidOperationException("Invalid data type " + NTRConnection.SearchCriteria.DataType.ToString() + " passed to NTRPacketReceiverThread.GetValueFromByteArray");
            }
        }

        #endregion
    }
}
