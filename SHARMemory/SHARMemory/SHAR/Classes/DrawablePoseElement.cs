using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVDrawablePoseElement@tCompositeDrawable@@")]
    public class DrawablePoseElement : DrawableElement
    {
        public DrawablePoseElement(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public DrawablePose Skin => Memory.ClassFactory.Create<DrawablePose>(ReadUInt32(20));
    }
}
