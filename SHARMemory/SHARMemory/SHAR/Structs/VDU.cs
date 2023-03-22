using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(VDUStruct))]
    public struct VDU
    {
        public const int Size = sizeof(uint) * GameplayManager.MAX_VDU_CARS + sizeof(int);

        public Vehicle[] Vehicles;

        public int Counter;

        public VDU(Vehicle[] vehicles, int counter)
        {
            Vehicles = vehicles;
            Counter = counter;
        }

        public override string ToString() => $"{Vehicles} | {Counter}";
    }

    internal class VDUStruct : Struct
    {
        public override int Size => VDU.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            Vehicle[] Vehicles = new Vehicle[GameplayManager.MAX_VDU_CARS];
            for (int i = 0; i < GameplayManager.MAX_VDU_CARS; i++)
            {
                Vehicles[i] = Memory.ClassFactory.Create<Vehicle>(BitConverter.ToUInt32(Bytes, Offset));
                Offset += sizeof(uint);
            }
            int Counter = BitConverter.ToInt32(Bytes, Offset);
            return new VDU(Vehicles, Counter);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not VDU Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelData)}'.", nameof(Value));

            for (uint i = 0; i < GameplayManager.MAX_VDU_CARS; i++)
            {
                BitConverter.GetBytes(Value2.Vehicles[i]?.Address ?? 0).CopyTo(Buffer, Offset);
                Offset += sizeof(uint);
            }
            BitConverter.GetBytes(Value2.Counter).CopyTo(Buffer, Offset);
        }
    }
}
