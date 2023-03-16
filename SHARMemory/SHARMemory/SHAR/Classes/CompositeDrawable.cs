using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtCompositeDrawable@@")]
    public class CompositeDrawable : DrawablePose
    {
        public CompositeDrawable(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public PointerArray<DrawableElement> Elements => PointerArrayExtensions.FromPtrArray<DrawableElement>(Memory, this, 68);
    }
}
