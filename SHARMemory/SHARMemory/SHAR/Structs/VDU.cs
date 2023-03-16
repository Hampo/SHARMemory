using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;

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

    internal class VDUStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address)
        {
            Vehicle[] Ratings = new Vehicle[GameplayManager.MAX_VDU_CARS];
            for (uint i = 0; i < GameplayManager.MAX_VDU_CARS; i++)
                Ratings[i] = Memory.ClassFactory.Create<Vehicle>(Address + sizeof(uint) * i);
            int Counter = Memory.ReadInt32(Address + sizeof(uint) * GameplayManager.MAX_VDU_CARS);

            return new VDU(Ratings, Counter);
        }

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not VDU Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelData)}'.", nameof(Value));

            for (uint i = 0; i < GameplayManager.MAX_VDU_CARS; i++)
                Memory.WriteUInt32(Address + sizeof(uint) * i, Value2.Vehicles[i].Address);
            Memory.WriteInt32(Address + sizeof(uint) * GameplayManager.MAX_VDU_CARS, Value2.Counter);
        }
    }
}
