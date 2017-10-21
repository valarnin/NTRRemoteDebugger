using NTRDebuggerTool.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Xml;

namespace NTRDebuggerTool.Remote
{
    public class NTRRemoteConnection
    {
        #region Members

        public string HardwarePID = null;

        public String IP { get; set; }
        public Int16 Port { get; set; }

        public Dictionary<uint, uint> AddressSpaces;

        public uint ProgressRead = 0;
        public uint ProgressReadMax = 0;

        public uint ProgressScan = 0;
        public uint ProgressScanMax = 0;

        internal TcpClient Client;

        private uint Sequence = 0u;

        private object SendLock = new object();
        private object ReceiveLock = new object();
        public bool LockControls = false;
        public bool IsConnected = false;
        public bool IsProcessListUpdated = false;
        public bool IsMemoryListUpdated = false;

        private long LastHeartbeat = 0;

        internal uint LastListProcessesSequence = 0u;
        internal uint LastListMemoryRegionsSequence = 0u;
        internal uint LastReadMemorySequence = 0u;

        private Thread PacketThread = null;
        internal bool CanSendHeartbeat = true;
        public List<string> Processes = new List<string>();

        public string SetCurrentOperationText = "";
        public string SetCurrentOperationText2 = "";
        private NTRPacketReceiverThread PacketReceiverThread;

        internal XmlDocument ReleasesDocument;
        public List<SearchCriteria> SearchCriteria = new List<SearchCriteria>();
        private Stopwatch SearchTimerStopwatch = new Stopwatch();

        #endregion

        #region Constructor

        public NTRRemoteConnection()
        {
            this.PacketReceiverThread = new NTRPacketReceiverThread(this);
            try
            {
                this.ReleasesDocument = new XmlDocument();
                ReleasesDocument.Load(File.OpenRead(Path.GetTempPath() + "3dsreleases.xml"));
            }
            catch (Exception e)
            {
                Logger.Log(null, e);
                this.ReleasesDocument = null;
            }
        }

        #endregion

        #region Connection Management

        public bool Connect()
        {
            if (Client != null)
            {
                bool Disconnected = false;
                try
                {
                    if (Client.Connected)
                    {
                        if (!SendHeartbeatPacket(true))
                        {
                            Disconnected = true;
                        }
                    }
                    else
                    {
                        Disconnected = true;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(null, e);
                    Disconnected = true;
                }
                if (Disconnected)
                {
                    Disconnect();
                }
            }
            if (Client == null)
            {
                try
                {
                    Client = new TcpClient();
                    Client.NoDelay = true;
                    IAsyncResult res = Client.BeginConnect(IP, Port, null, null);
                    if (!res.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(Config.ConnectTimeout)))
                    {
                        Client.Close();
                        Client = null;
                        throw new Exception("Connect timeout");
                    }
                    Client.EndConnect(res);
                    PacketThread = new Thread(new ThreadStart(this.PacketReceiverThread.ThreadReceivePackets));
                    PacketThread.Name = "ReadPacketsThread";
                    PacketThread.Start();
                    IsConnected = true;
                    CanSendHeartbeat = true;
                    SendHeartbeatPacket(true);
                }
                catch (Exception e)
                {
                    Logger.Log(null, e);
                    return false;
                }
            }
            return true;
        }

        public void Disconnect()
        {
            try
            {
                if (Client != null)
                {
                    Client.Close();
                }
                if (PacketThread != null)
                {
                    PacketThread.Abort();
                }
            }
            catch (Exception e)
            {
                Logger.Log(null, e);
            }
            Client = null;
            IsConnected = false;
            HardwarePID = null;
        }

        #endregion

        #region Sending Packets

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, byte Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, new byte[] { Value });
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, ushort Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, BitConverter.GetBytes(Value));
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, uint Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, BitConverter.GetBytes(Value));
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, ulong Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, BitConverter.GetBytes(Value));
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, float Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, BitConverter.GetBytes(Value));
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, double Value)
        {
            SendWriteMemoryPacket(ProcessID, Address, BitConverter.GetBytes(Value));
        }

        public void SendWriteMemoryPacket(uint ProcessID, uint Address, byte[] Values)
        {
            this.SendPacket(PacketType.GeneralMemory, PacketCommand.Write, new uint[] { BitConverter.ToUInt32(BitConverter.GetBytes(ProcessID).Reverse().ToArray(), 0), Address, (uint)Values.Length }, Values);
        }

        public void SendReadMemoryPacket(SearchCriteria NewSearchCriteria)
        {
            if (!NewSearchCriteria.HideSearch)
            {
                this.LockControls = true;
            }
            while (SearchCriteria[0] != NewSearchCriteria)
            {
                Thread.Sleep(10);
            }
            SearchTimerStopwatch.Start();
            SendReadMemoryPacketPre();
            SearchTimerStopwatch.Stop();
            NewSearchCriteria.Duration = (uint)SearchTimerStopwatch.ElapsedMilliseconds;
            SearchTimerStopwatch.Reset();
            if (!NewSearchCriteria.HideSearch)
            {
                this.LockControls = false;
            }
        }

        private int GetAddressSpace(uint addr)
        {
            int ret = -1;
            foreach (uint Current in AddressSpaces.Keys)
            {
                if (addr >= Current && addr <= (Current + AddressSpaces[Current]))
                {
                    ret = (int)Current;
                    break;
                }
            }
            return (ret);
        }

        private bool AddressIsValid(uint address)
        {
            if (GetAddressSpace(address) == -1)
            {
               Console.WriteLine("Invalid address: {0:X}", address);
                return (false);
            }
            return (true);

        }
        private bool AddressIsNear(uint address)
        {
            if (SearchCriteria[0].SearchBlock.Size == 0
                || ((address - SearchCriteria[0].SearchBlock.EndAddress) <= 0x1000)
                    && SearchCriteria[0].SearchBlock.Size < 0x1000000
                    && SearchCriteria[0].SearchBlock.AddressSpace == GetAddressSpace(address))
                return (true);
            return (false);
        }

        private void AddToSearchBlock(uint address)
        {
            uint lastAddress;
            uint gap;

            if (SearchCriteria[0].SearchBlock.Size == 0)
            {
                SearchCriteria[0].SearchBlock.AddressSpace = GetAddressSpace(address);
                if (SearchCriteria[0].SearchBlock.AddressSpace == -1)
                {
                    SearchCriteria[0].SearchBlock.AddressSpace = 0;
                    return;
                }
                SearchCriteria[0].SearchBlock.Size = SearchCriteria[0].Size;
                SearchCriteria[0].SearchBlock.StartAddress = address;
                SearchCriteria[0].SearchBlock.EndAddress = address;
                SearchCriteria[0].SearchBlock.AddressList.Add(address);
                
            }
            else
            {
                lastAddress = SearchCriteria[0].SearchBlock.EndAddress;
                gap = address - lastAddress;
                SearchCriteria[0].SearchBlock.EndAddress = address;
                SearchCriteria[0].SearchBlock.Size += gap;
                SearchCriteria[0].SearchBlock.Size += SearchCriteria[0].Size;
                SearchCriteria[0].SearchBlock.AddressList.Add(address);
            }
        }

        private void SendReadMemoryPacketPre()
        {
            SearchCriteria[0].AllSearchesComplete = false;
            if (SearchCriteria[0].AddressesFound.Count > 0
                && (SearchCriteria[0].AddressesFound.Count < 200
                    || SearchCriteria[0].StartAddress == uint.MaxValue))
            {
                uint TempAddress = SearchCriteria[0].StartAddress;
                uint TempLength = SearchCriteria[0].Length;
                SearchCriteria[0].SearchBlock.AddressList = new List<uint>();
                SearchCriteria[0].SearchBlock.StartAddress = 0;
                SearchCriteria[0].SearchBlock.EndAddress = 0;
                SearchCriteria[0].SearchBlock.Size = 0;
                SearchCriteria[0].SearchBlock.AddressSpace = 0;
                SearchCriteria[0].SearchBlock.AddressList.Clear();
                //SearchCriteria[0].Length = (uint)SearchCriteria[0].AddressesFound[SearchCriteria[0].AddressesFound.Keys.First()].Length;
                //Clone the list to an array to prevent concurrent modification
                foreach (uint Address in new List<uint>(SearchCriteria[0].AddressesFound.Keys))
                {
                
                    if (AddressIsValid(Address))
                    {
                    retry:
                        if (AddressIsNear(Address))
                            AddToSearchBlock(Address);
                        else
                        {
                            SearchCriteria[0].SearchComplete = false;
                            SearchCriteria[0].StartAddress = SearchCriteria[0].SearchBlock.StartAddress;
                            SearchCriteria[0].Length = SearchCriteria[0].SearchBlock.Size;
                            SendReadMemoryPacket();
                            SearchCriteria[0].SearchBlock.StartAddress = 0;
                            SearchCriteria[0].SearchBlock.EndAddress = 0;
                            SearchCriteria[0].SearchBlock.Size = 0;
                            SearchCriteria[0].SearchBlock.AddressSpace = 0;
                            SearchCriteria[0].SearchBlock.AddressList.Clear();
                            goto retry;
                        }
                    }
                                        
                }
                SearchCriteria[0].SearchBlock.AddressList.Clear();
                SearchCriteria[0].SearchBlock.StartAddress = 0;
                SearchCriteria[0].SearchBlock.EndAddress = 0;
                SearchCriteria[0].SearchBlock.Size = 0;
                SearchCriteria[0].Length = TempLength;
                SearchCriteria[0].StartAddress = TempAddress;
            }
            else if (SearchCriteria[0].StartAddress == uint.MaxValue)
            {
                foreach (uint ActualAddressSpace in AddressSpaces.Keys)
                {
                    SearchCriteria[0].StartAddress = ActualAddressSpace;
                    SearchCriteria[0].Length = AddressSpaces[ActualAddressSpace];
                    SearchCriteria[0].SearchComplete = false;
                    SendReadMemoryPacket();
                }
                SearchCriteria[0].StartAddress = uint.MaxValue;
                SearchCriteria[0].Length = uint.MaxValue;
            }
            else
            {
                SearchCriteria[0].SearchComplete = false;
                SendReadMemoryPacket();
            }
            SearchCriteria[0].AllSearchesComplete = SearchCriteria[0].SearchComplete = true;
            SearchCriteria[0].FirstSearch = false;
            SearchCriteria.RemoveAt(0);
        }

        private void SendReadMemoryPacket()
        {
            if (!SearchCriteria[0].HideSearch)
            {
                SetCurrentOperationText = "Searching Memory " + Utilities.GetStringFromByteArray(BitConverter.GetBytes(SearchCriteria[0].StartAddress).Reverse().ToArray()) + " - " + Utilities.GetStringFromByteArray(BitConverter.GetBytes(SearchCriteria[0].StartAddress + SearchCriteria[0].Length).Reverse().ToArray());
            }
            //Console.WriteLine("SendPacket(General, Read, {0:X}, {1:X}, {2:X})", SearchCriteria[0].ProcessID, SearchCriteria[0].StartAddress, SearchCriteria[0].Length);
            this.SendPacket(PacketType.General, PacketCommand.Read, new uint[] { BitConverter.ToUInt32(BitConverter.GetBytes(SearchCriteria[0].ProcessID).Reverse().ToArray(), 0), SearchCriteria[0].StartAddress, SearchCriteria[0].Length });
            while (SearchCriteria[0].SearchComplete != true)
            {
                Thread.Sleep(100);
            }
            if (!SearchCriteria[0].HideSearch)
            {
                SetCurrentOperationText = "";
            }
        }
        public void SendListProcessesPacket()
        {
            this.SendPacket(PacketType.General, PacketCommand.ListProcesses, null);
        }

        public void SendReadMemoryAddressesPacket(string ProcessID)
        {
            uint ActualProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(ProcessID).Reverse().ToArray(), 0);
            this.SendPacket(PacketType.General, PacketCommand.ListAddresses, new uint[1] { ActualProcessID });
        }

        public void sendReloadPacket()
        {
            this.SendPacket(PacketType.General, PacketCommand.Reload, null);
        }

        public void SendHelloPacket()
        {
            this.SendPacket(PacketType.General, PacketCommand.Hello, null);
        }

        public bool SendHeartbeatPacket()
        {
            return SendHeartbeatPacket(false);
        }

        public bool SendHeartbeatPacket(bool IsConnecting)
        {
            if (IsConnecting || this.Client != null)
            {
                if (IsConnecting || (this.Client != null && this.Client.Connected))
                {
                    if (CanSendHeartbeat && LastHeartbeat < (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - 30000)
                    {
                        CanSendHeartbeat = false;
                        LastHeartbeat = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        try
                        {
                            SendPacket(PacketType.General, PacketCommand.Heartbeat, null);
                            return true;
                        }
                        catch (Exception e)
                        {
                            Logger.Log(null, e);
                        }
                    }
                }
            }
            return false;
        }

        private void SendPacket(uint Type, uint Command, uint[] Arguments)
        {
            SendPacket(Type, Command, Arguments, new byte[0]);
        }

        private void SendPacket(uint Type, uint Command, uint[] Arguments, byte[] AdditionalData)
        {
            lock (this.SendLock)
            {
                int num = 0;
                byte[] array = new byte[84];
                Array.Clear(array, 0, 84); //Force to zero
                BitConverter.GetBytes(0x12345678).CopyTo(array, num);
                BitConverter.GetBytes(this.Sequence += 1000u).CopyTo(array, num += 4);
                SetPacketSequence(Type, Command);
                BitConverter.GetBytes(Type).CopyTo(array, num += 4);
                BitConverter.GetBytes(Command).CopyTo(array, num += 4);
                if (Arguments != null)
                {
                    for (int i = 0; i < 16 && i < Arguments.Length; i++)
                    {
                        BitConverter.GetBytes(Arguments[i]).CopyTo(array, num += 4);
                    }
                }

                BitConverter.GetBytes(AdditionalData.Length).CopyTo(array, array.Length - 4);
                this.Client.GetStream().Write(array, 0, array.Length);
                if (AdditionalData.Length > 0u)
                {
                    this.Client.GetStream().Write(AdditionalData, 0, AdditionalData.Length);
                }
            }
        }

        private void SetPacketSequence(uint Type, uint Command)
        {
            if (Type == PacketType.General && Command == PacketCommand.ListProcesses)
            {
                LastListProcessesSequence = Sequence;
            }
            else if (Type == PacketType.General && Command == PacketCommand.ListAddresses)
            {
                LastListMemoryRegionsSequence = Sequence;
            }
            else if (Type == PacketType.General && Command == PacketCommand.Read)
            {
                LastReadMemorySequence = Sequence;
            }
        }

        #endregion
    }
}
