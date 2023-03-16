using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtPrimGroup@@")]
    public class PrimGroup : Class
    {
        public PrimGroup(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Shader Shader => Memory.ClassFactory.Create<Shader>(ReadUInt32(8));
    }
}
