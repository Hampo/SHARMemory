using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class CompositeDrawable : DrawablePose
    {
        public CompositeDrawable(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<DrawableElement> Elements => PointerArrayExtensions.FromPtrArray<DrawableElement>(Memory, this, 68);
    }
}
