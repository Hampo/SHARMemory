using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVpddiTexture@@")]
#pragma warning disable IDE1006 // Naming Styles
    public class pddiTexture : pddiObject
#pragma warning restore IDE1006 // Naming Styles
    {
        public pddiTexture(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }
    }
}
