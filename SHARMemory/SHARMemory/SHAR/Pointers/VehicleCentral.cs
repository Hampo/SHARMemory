using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class VehicleCentral : Pointer
    {
        public VehicleCentral(Memory memory) : base(memory, memory.SelectAddress(0x6C84D8, 0x6C8498, 0x6C8498, 0x6C84D0)) { }

        public Vehicle ActiveVehicles(uint Index) => new Vehicle(Memory, ReadUInt32(180 + Index * 4u));
    }
}
