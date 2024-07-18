using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVradLoadObject@@")]
    public class radLoadObject : Class
    {
        public radLoadObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        internal const uint RefCountOffset = 0x4;
        public uint RefCount
        {
            get => ReadUInt32(RefCountOffset);
            set => WriteUInt32(RefCountOffset, value);
        }
    }
}
