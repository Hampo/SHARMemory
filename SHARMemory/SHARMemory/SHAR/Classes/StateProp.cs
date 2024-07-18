using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVStateProp@@")]
    public class StateProp : Class
    {
        public StateProp(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        private const uint StatePropDataOffset = 16;
        public SHARMemory.Memory.Class StatePropData => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(StatePropDataOffset));
    }
}
