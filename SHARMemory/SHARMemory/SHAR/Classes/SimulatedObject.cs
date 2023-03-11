namespace SHARMemory.SHAR.Classes
{
    public class SimulatedObject : Class
    {
        public SimulatedObject(Memory memory, uint address) : base(memory, address) { }

        public SimState SimState => Memory.CreateClass<SimState>(ReadUInt32(16));
    }
}
