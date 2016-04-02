using NTRDebuggerTool.Forms.FormEnums;
using NTRDebuggerTool.Objects;
using NTRDebuggerTool.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace NTRDebuggerTool
{
    class Debug
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        internal static void Execute()
        {
            IntPtr Handle = GetConsoleWindow();
            if (Handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(Handle, SW_SHOW);
            }
            NTRRemoteConnection Conn = new NTRRemoteConnection();
            Console.WriteLine("Connecting...");
            Conn.IP = "192.168.1.29";
            Conn.Port = 8000;
            while (!Conn.Connect()) ;
            Thread t = new Thread(delegate()
                {
                    while (true)
                    {
                        Conn.SendHeartbeatPacket();
                        Thread.Sleep(100);
                    }
                });

            t.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Getting Processes...");
            Conn.SendListProcessesPacket();
            while (!Conn.IsProcessListUpdated)
            {
                Thread.Sleep(10);
            }
            Dictionary<string, ReadOnlyDictionary<uint, uint>> Procs = new Dictionary<string, ReadOnlyDictionary<uint, uint>>();
            foreach (string ProcFull in Conn.Processes)
            {
                string Proc = ProcFull.Split('|')[0];
                Console.WriteLine("Fetching memregions for process " + Proc);
                Conn.SendReadMemoryAddressesPacket(Proc);
                while (!Conn.IsMemoryListUpdated)
                {
                    Thread.Sleep(10);
                }
                Procs.Add(Proc, Conn.AddressSpaces);
                Conn.IsMemoryListUpdated = false;
            }

            Directory.CreateDirectory("Temp");

            for (int i = 0; i < 3; ++i)
            {
                Directory.CreateDirectory("Temp" + Path.DirectorySeparatorChar + i);
                Console.WriteLine("Dumping memory set " + i + "...");

                foreach (string Proc in Procs.Keys)
                {
                    foreach (uint Start in Procs[Proc].Keys)
                    {
                        uint Size = Procs[Proc][Start];
                        SearchCriteria Criteria = new SearchCriteria();
                        Criteria.ProcessID = BitConverter.ToUInt32(Utilities.GetByteArrayFromByteString(Proc), 0);
                        Criteria.DataType = DataTypeExact.Bytes1;
                        Criteria.StartAddress = Start;
                        Criteria.Length = Size;
                        Criteria.SearchType = SearchTypeBase.Unknown;
                        Criteria.SearchValue = new byte[] { 0 };
                        Criteria.Size = 1;
                        Conn.SearchCriteria.Add(Criteria);
                        byte[] Data = new byte[Size];
                        Console.WriteLine("Dumping PID " + Proc + ", MR " + Start + "+" + Size + "...");
                        Conn.SendReadMemoryPacket(Criteria);
                        while (!Criteria.SearchComplete)
                        {
                            Thread.Sleep(10);
                        }
                        foreach (uint Addr in Criteria.AddressesFound.Keys)
                        {
                            Data[Addr - Start] = Criteria.AddressesFound[Addr][0];
                        }
                        File.WriteAllBytes("Temp" + Path.DirectorySeparatorChar + i + Path.DirectorySeparatorChar + Proc + "_" + Start + ".raw", Data);
                    }
                }

                if (i == 1)
                {
                    Console.WriteLine("Press any key...");
                    Console.ReadKey(false);
                    Console.WriteLine();
                }
            }

            t.Abort();
            Conn.Disconnect();
        }
    }
}
