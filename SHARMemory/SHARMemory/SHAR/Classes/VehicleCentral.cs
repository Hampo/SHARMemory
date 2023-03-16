﻿using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class VehicleCentral : Class
    {
        public VehicleCentral(Memory memory, uint address) : base(memory, address)
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
        public PointerArray<Vehicle> ActiveVehicles => new(Memory, Address + ActiveVehiclesOffset, MaxVehicles);
    }
}
