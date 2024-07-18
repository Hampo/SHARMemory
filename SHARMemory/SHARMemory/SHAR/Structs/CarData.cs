using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

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

    public override readonly string ToString() => $"{Filename} | {Name} | {Position} | {Heading} | {Vehicle} | {HuskVehicle} | {UsingHusk}";
}

internal class CarDataStruct : Struct
{
    public override int Size => CarData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        string Filename = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 64));
        Offset += 64;
        string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 32));
        Offset += 32;
        Vector3 Position = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        float Heading = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        Vehicle Vehicle = Memory.ClassFactory.Create<Vehicle>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Vehicle HuskVehicle = Memory.ClassFactory.Create<Vehicle>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        bool UsingHusk = BitConverter.ToBoolean(Bytes, Offset);
        return new CarData(Filename, Name, Position, Heading, Vehicle, HuskVehicle, UsingHusk);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CarData Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CarData)}'.", nameof(Value));

        Memory.GetStringBytes(Value2.Filename, Encoding.UTF8, 64).CopyTo(Buffer, Offset);
        Offset += 64;
        Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 32).CopyTo(Buffer, Offset);
        Offset += 32;
        Memory.BytesFromStruct(Value2.Position, Buffer, Offset);
        Offset += Vector3.Size;
        BitConverter.GetBytes(Value2.Heading).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Vehicle?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.HuskVehicle?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.UsingHusk).CopyTo(Buffer, Offset);
    }
}
