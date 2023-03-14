using SHARMemory.SHAR.Classes;
using System.Text;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(MissionCarDataStruct))]
    public struct MissionCarData
    {
        public const int Size = sizeof(uint) + 32 + sizeof(uint) + 4; // sizeof(bool) returns 1, not sure best way to handle padding

        public Vehicle Vehicle;

        public string Name;

        public Vehicle HuskVehicle;

        public bool UsingHusk;

        public MissionCarData(Vehicle vehicle, string name, Vehicle huskVehicle, bool usingHusk)
        {
            Vehicle = vehicle;
            Name = name;
            HuskVehicle = huskVehicle;
            UsingHusk = usingHusk;
        }

        public override string ToString() => $"{Vehicle} | {Name} | {HuskVehicle} | {UsingHusk}";
    }

    internal class MissionCarDataStruct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new MissionCarData(Memory.CreateClass<Vehicle>(Address), Memory.ReadString(Address + sizeof(uint), Encoding.UTF8, 32), Memory.CreateClass<Vehicle>(Address + sizeof(uint) + 32), Memory.ReadBoolean(Address + sizeof(uint) + 32 + sizeof(uint)));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not MissionCarData Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(MissionCarData)}'.", nameof(Value));

            Memory.WriteUInt32(Address, Value2.Vehicle.Address);
            Memory.WriteString(Address + sizeof(uint), Value2.Name, Encoding.UTF8, 32);
            Memory.WriteUInt32(Address + sizeof(uint) + 32, Value2.Vehicle.Address);
            Memory.WriteBoolean(Address + sizeof(uint) + 32 + sizeof(uint), Value2.UsingHusk);
        }
    }
}
