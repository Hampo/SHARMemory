using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVDrawablePropElement@tCompositeDrawable@@")]
    public class DrawablePropElement : DrawableElement
    {
        public DrawablePropElement(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Drawable Drawable => Memory.ClassFactory.Create<Drawable>(ReadUInt32(20));
    }
}
