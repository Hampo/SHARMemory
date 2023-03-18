using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVd3dTexture@@")]
#pragma warning disable IDE1006 // Naming Styles
    public class d3dTexture : pddiTexture
#pragma warning restore IDE1006 // Naming Styles
    {
        public d3dTexture(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO
    }
}
