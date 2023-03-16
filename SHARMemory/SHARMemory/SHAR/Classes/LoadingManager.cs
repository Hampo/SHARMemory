using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVLoadingManager@@")]
    public class LoadingManager : Class
    {
        public LoadingManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public uint RequestHead => ReadUInt32(29604);

        public uint RequestTail => ReadUInt32(29608);

        public bool IsLoading => ReadBoolean(29612);

        public bool IsCancellingLoads => ReadBoolean(29613);
    }
}
