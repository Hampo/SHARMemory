using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtEffect@@")]
    public class Effect : Drawable
    {
        public Effect(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO
    }
}
