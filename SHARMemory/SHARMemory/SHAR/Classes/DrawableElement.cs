using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDrawableElement@tCompositeDrawable@@")]
public class DrawableElement : Class
{
    public enum Types
    {
        Base,
        Prop,
        Skin,
        Effect
    }

    public DrawableElement(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public Types Type => (Types)ReadInt32(4);
}
