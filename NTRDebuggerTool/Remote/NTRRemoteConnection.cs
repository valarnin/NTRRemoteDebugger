using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public String IP { get; set; }
        public Int16 Port { get; set; }
        public List<uint> AddressesFound = new List<uint>();

        public ReadOnlyDictionary<uint, uint> AddressSpaces;

        public uint ProgressRead = 0;
        public uint ProgressReadMax = 0;

        public uint ProgressScan = 0;
        public uint ProgressScanMax = 0;

        internal uint MemoryReadAddress = 0;
        internal byte[] SearchBytes;

        internal bool NewSearch = true;

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
        public bool IsMemorySearchFinished = false;

        public string SetCurrentOperationText = "";
        private NTRPacketReceiverThread PacketReceiverThread;

        internal XmlDocument ReleasesDocument;

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
                this.ReleasesDocument = null;
            }
        }

        #endregion

        #region Other Methods

        public void ResetSearch()
        {
            NewSearch = true;
            AddressesFound.Clear();
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
                    System.Console.WriteLine(e);
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
                    Client.Connect(IP, Port);
                    PacketThread = new Thread(new ThreadStart(this.PacketReceiverThread.ThreadReceivePackets));
                    PacketThread.Name = "ReadPacketsThread";
                    PacketThread.Start();
                    IsConnected = true;
                    CanSendHeartbeat = true;
                    SendHeartbeatPacket(true);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                    return false;
                }
            }
            return true;
        }

        public void Disconnect()
        {
            try
            {
                Client.Close();
                PacketThread.Abort();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            Client = null;
            IsConnected = false;
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

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, byte Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, new byte[] { Value });
        }

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, ushort Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, BitConverter.GetBytes(Value));
        }

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, uint Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, BitConverter.GetBytes(Value));
        }

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, ulong Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, BitConverter.GetBytes(Value));
        }

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, float Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, BitConverter.GetBytes(Value));
        }

        public void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size, double Value)
        {
            SendReadMemoryPacket(ProcessID, Address, Size, BitConverter.GetBytes(Value));
        }

        public void SendReadMemoryPacket(uint ProcessID, uint AddressSpace, uint Size, byte[] SearchValue)
        {
            this.SearchBytes = SearchValue;
            this.LockControls = true;
            if (AddressesFound.Count > 0)
            {
                //Clone the list to an array to prevent concurrent modification
                foreach (uint Address in AddressesFound.ToArray())
                {
                    SendReadMemoryPacket(ProcessID, Address, Size);
                }
            }
            else if (AddressSpace == uint.MaxValue)
            {
                foreach (uint ActualAddressSpace in AddressSpaces.Keys)
                {
                    SendReadMemoryPacket(ProcessID, ActualAddressSpace, AddressSpaces[ActualAddressSpace]);
                }
            }
            else
            {
                SendReadMemoryPacket(ProcessID, AddressSpace, Size);
            }
            IsMemorySearchFinished = true;
            this.LockControls = false;
        }

        private void SendReadMemoryPacket(uint ProcessID, uint Address, uint Size)
        {
            SetCurrentOperationText = "Searching Memory " + Utilities.GetStringFromByteArray(BitConverter.GetBytes(Address).Reverse().ToArray());
            this.MemoryReadAddress = Address;
            this.SendPacket(PacketType.General, PacketCommand.Read, new uint[] { BitConverter.ToUInt32(BitConverter.GetBytes(ProcessID).Reverse().ToArray(), 0), Address, Size });
            while (MemoryReadAddress != uint.MaxValue)
            {
                Thread.Sleep(100);
            }
            SetCurrentOperationText = "";
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
                            System.Console.WriteLine(e);
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
