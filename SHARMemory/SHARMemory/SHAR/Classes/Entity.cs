using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtEntity@@")]
    public class Entity : RefCounted
    {
        public Entity(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        internal const uint NameOffset = RefCountOffset + sizeof(uint);
        public long Name
        {
            get => ReadInt64(NameOffset);
            set => WriteInt64(NameOffset, value);
        }
    }
}
