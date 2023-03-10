using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class VehicleCentral : Pointer
    {
        public VehicleCentral(Memory memory) : base(memory, memory.SelectAddress(0x6C84D8, 0x6C8498, 0x6C8498, 0x6C84D0))
        {
            if (memory.IsModLauncherLoaded)
            {
                MaxVehicles = memory.ReadUInt32(memory.ModLauncherOrdinals[3360]);
                ActiveVehiclesOffset = memory.ReadUInt32(memory.ModLauncherOrdinals[3364]);
            }
            else
            {
                MaxVehicles = 30;
                ActiveVehiclesOffset = 180;
            }
        }

        public uint MaxVehicles { get; }

        private uint ActiveVehiclesOffset { get; }
        public Vehicle ActiveVehicles(uint Index) => new Vehicle(Memory, ReadUInt32(ActiveVehiclesOffset + Index * 4u));
    }
}
