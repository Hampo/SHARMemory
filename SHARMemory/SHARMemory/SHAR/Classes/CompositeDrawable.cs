using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class CompositeDrawable : Class
    {
        public CompositeDrawable(Memory memory, uint address) : base(memory, address) { }

        public PtrArray<DrawableElement> Elements => Memory.CreateClass<PtrArray<DrawableElement>>(Address + 68);
    }
}
