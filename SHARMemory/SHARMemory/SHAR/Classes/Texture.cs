using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtTexture@@")]
    public class Texture : Class
    {
        public Texture(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public pddiTexture PDDITexture => Memory.ClassFactory.Create<pddiTexture>(ReadUInt32(16));
    }
}
