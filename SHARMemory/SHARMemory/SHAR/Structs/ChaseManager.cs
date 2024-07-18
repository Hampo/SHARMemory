using SHARMemory.Memory;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(ChaseManagerStruct))]
public struct ChaseManager
{
    public const int Size = sizeof(uint) + 16 + 64;

    public Classes.ChaseManager ChaseManagerPtr;

    public string HostVehicle;

    public string HostVehicleFilename;

    public ChaseManager(Classes.ChaseManager chaseManagerPtr, string hostVehicle, string hostVehicleFilename)
    {
        ChaseManagerPtr = chaseManagerPtr;
        HostVehicle = hostVehicle;
        HostVehicleFilename = hostVehicleFilename;
    }

    public override readonly string ToString() => $"{ChaseManagerPtr} | {HostVehicle} | {HostVehicleFilename}";
}

internal class ChaseManagerStruct : Struct
{
    public override int Size => ChaseManager.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Classes.ChaseManager ChaseManagerPtr = Memory.ClassFactory.Create<Classes.ChaseManager>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        string HostVehicle = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
        Offset += 16;
        string HostVehicleFilename = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 64));
        return new ChaseManager(ChaseManagerPtr, HostVehicle, HostVehicleFilename);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not ChaseManager Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(ChaseManager)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.ChaseManagerPtr?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        Memory.GetStringBytes(Value2.HostVehicle, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
        Offset += 16;
        Memory.GetStringBytes(Value2.HostVehicleFilename, Encoding.UTF8, 64).CopyTo(Buffer, Offset);
    }
}
