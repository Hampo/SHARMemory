using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class CompositeDrawable : DrawablePose
    {
        public CompositeDrawable(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<DrawableElement> Elements => PointerArray<DrawableElement>.FromPtrArray(Memory, this, 68);
    }
}
