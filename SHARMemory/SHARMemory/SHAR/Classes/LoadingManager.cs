namespace SHARMemory.SHAR.Classes
{
    public class LoadingManager : Class
    {
        public LoadingManager(Memory memory, uint address) : base(memory, address) { }

        public uint RequestHead => ReadUInt32(29604);

        public uint RequestTail => ReadUInt32(29608);

        public bool IsLoading => ReadBoolean(29612);

        public bool IsCancellingLoads => ReadBoolean(29613);
    }
}
