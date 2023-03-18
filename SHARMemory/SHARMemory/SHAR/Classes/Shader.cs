using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtShader@@")]
    public class Shader : Class
    {
        public Shader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public pddiShader PDDIShader => Memory.ClassFactory.Create<pddiShader>(ReadUInt32(20));
    }
}
