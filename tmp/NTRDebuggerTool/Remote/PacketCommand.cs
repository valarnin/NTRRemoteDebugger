
namespace NTRDebuggerTool.Remote
{
    static class PacketCommand
    {
        //General
        public const uint Heartbeat = 0u;
        public const uint Hello = 3u;
        public const uint Reload = 4u;
        public const uint ListProcesses = 5u;

        //Memory
        public const uint ListAddresses = 8u;
        public const uint Read = 9u;
        public const uint Write = 10u;
    }
}
