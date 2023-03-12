using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class PrimGroup : Class
    {
        public PrimGroup(Memory memory, uint address) : base(memory, address) { }

        public Shader Shader => Memory.CreateClass<Shader>(ReadUInt32(8));
    }
}
