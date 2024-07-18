using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

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

    public override readonly string ToString() => $"{Vehicle} | {Name} | {HuskVehicle} | {UsingHusk}";
}

internal class MissionCarDataStruct : Struct
{
    public override int Size => MissionCarData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Vehicle Vehicle = Memory.ClassFactory.Create<Vehicle>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 32));
        Offset += 32;
        Vehicle HuskVehicle = Memory.ClassFactory.Create<Vehicle>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        bool UsingHusk = BitConverter.ToBoolean(Bytes, Offset);
        return new MissionCarData(Vehicle, Name, HuskVehicle, UsingHusk);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not MissionCarData Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(MissionCarData)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.Vehicle?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 32).CopyTo(Buffer, Offset);
        Offset += 32;
        BitConverter.GetBytes(Value2.HuskVehicle?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.UsingHusk).CopyTo(Buffer, Offset);
    }
}
