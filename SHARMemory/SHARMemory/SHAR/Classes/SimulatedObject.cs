﻿namespace SHARMemory.SHAR.Classes
{
    public class SimulatedObject : Class
    {
        public SimulatedObject(Memory memory, uint address) : base(memory, address) { }

        public SimState SimState => new SimState(Memory, ReadUInt32(16));
    }
}