using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtShader@@")]
    public class Shader : Class
    {
        public Shader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public d3dShader D3DShader => Memory.ClassFactory.Create<d3dShader>(ReadUInt32(20));
    }
}
