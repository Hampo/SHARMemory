using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class VehicleCentral : Pointer
    {
        public VehicleCentral(Memory memory) : base(memory, memory.SelectAddress(0x6C84D8, 0x6C8498, 0x6C8498, 0x6C84D0))
        {
            if (memory.ModLauncherOrdinals.TryGetValue(3360, out uint MaxVehiclesAddress) && memory.ModLauncherOrdinals.TryGetValue(3364, out uint ActiveVehiclesOffsetAddress))
            {
                MaxVehicles = memory.ReadUInt32(MaxVehiclesAddress);
                ActiveVehiclesOffset = memory.ReadUInt32(ActiveVehiclesOffsetAddress);
            }
            else
            {
                MaxVehicles = 30;
                ActiveVehiclesOffset = 180;
            }
        }

        private readonly uint MaxVehicles;
        private readonly uint ActiveVehiclesOffset;
        public PointerArray<Vehicle> ActiveVehicles => new(Memory, Value + ActiveVehiclesOffset, MaxVehicles);
    }
}
