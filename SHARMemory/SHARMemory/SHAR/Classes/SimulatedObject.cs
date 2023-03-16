using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVSimulatedObject@sim@@")]
    public class SimulatedObject : Class
    {
        public SimulatedObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public SimState SimState => Memory.ClassFactory.Create<SimState>(ReadUInt32(16));
    }
}
