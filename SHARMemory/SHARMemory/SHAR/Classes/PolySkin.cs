using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtPolySkin@@")]
    public class PolySkin : DrawablePose
    {
        public PolySkin(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public PointerArray<PrimGroup> PrimGroups => PointerArrayExtensions.FromPtrArray<PrimGroup>(Memory, this, 32);
    }
}
