using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System.Text;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(CarDataStruct))]
    public struct CarData
    {
        public const int Size = 64 + 32 + Vector3.Size + sizeof(float) + sizeof(uint) + sizeof(uint) + 4; // sizeof(bool) returns 1, not sure best way to handle padding

        public string Filename;

        public string Name;

        public Vector3 Position;

        public float Heading;

        public Vehicle Vehicle;

        public Vehicle HuskVehicle;

        public bool UsingHusk;

        public CarData(string filename, string name, Vector3 position, float heading, Vehicle vehicle, Vehicle huskVehicle, bool usingHusk)
        {
            Filename = filename;
            Name = name;
            Position = position;
            Heading = heading;
            Vehicle = vehicle;
            HuskVehicle = huskVehicle;
            UsingHusk = usingHusk;
        }

        public override string ToString() => $"{Filename} | {Name} | {Position} | {Heading} | {Vehicle} | {HuskVehicle} | {UsingHusk}";
    }

    internal class CarDataStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new CarData(Memory.ReadString(Address, Encoding.UTF8, 64), Memory.ReadString(Address + 64, Encoding.UTF8, 32), Memory.ReadStruct<Vector3>(Address + 64 + 32), Memory.ReadSingle(Address + 64 + 32 + Vector3.Size), Memory.ClassFactory.Create<Vehicle>(Address + 64 + 32 + Vector3.Size + sizeof(float)), Memory.ClassFactory.Create<Vehicle>(Address + 64 + 32 + Vector3.Size + sizeof(float) + sizeof(uint)), Memory.ReadBoolean(Address + 64 + 32 + Vector3.Size + sizeof(float) + sizeof(uint) + sizeof(uint)));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not CarData Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CarData)}'.", nameof(Value));

            Memory.WriteString(Address, Value2.Filename, Encoding.UTF8, 64);
            Memory.WriteString(Address + 64, Value2.Name, Encoding.UTF8, 32);
            Memory.WriteStruct(Address + 64 + 32, Value2.Position);
            Memory.WriteSingle(Address + 64 + 32 + Vector3.Size, Value2.Heading);
            Memory.WriteUInt32(Address + 64 + 32 + Vector3.Size + sizeof(float), Value2.Vehicle.Address);
            Memory.WriteUInt32(Address + 64 + 32 + Vector3.Size + sizeof(float) + sizeof(uint), Value2.Vehicle.Address);
            Memory.WriteBoolean(Address + 64 + 32 + Vector3.Size + sizeof(float) + sizeof(uint) + sizeof(uint), Value2.UsingHusk);
        }
    }
}
