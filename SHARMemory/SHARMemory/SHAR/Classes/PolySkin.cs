using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class PolySkin : DrawablePose
    {
        public PolySkin(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<PrimGroup> PrimGroups => PointerArrayExtensions.FromPtrArray<PrimGroup>(Memory, this, 32);
    }
}
