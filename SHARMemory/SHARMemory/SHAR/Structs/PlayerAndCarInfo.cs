using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(PlayerAndCarInfoStruct))]
public struct PlayerAndCarInfo
{
    public const int Size = Vector3.Size + Vector3.Size + 4; // sizeof(bool) returns 1, not sure best way to handle padding

    public Vector3 PlayerPosition;

    public Vector3 ForceLocation;

    public bool DirtyFlag;

    public PlayerAndCarInfo(Vector3 playerPosition, Vector3 forceLocation, bool dirtyFlag)
    {
        PlayerPosition = playerPosition;
        ForceLocation = forceLocation;
        DirtyFlag = dirtyFlag;
    }

    public override readonly string ToString() => $"{PlayerPosition} | {ForceLocation} | {DirtyFlag}";
}

internal class PlayerAndCarInfoStruct : Struct
{
    public override int Size => PlayerAndCarInfo.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Vector3 PlayerPosition = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        Vector3 ForceLocation = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        bool DirtyFlag = BitConverter.ToBoolean(Bytes, Offset);
        return new PlayerAndCarInfo(PlayerPosition, ForceLocation, DirtyFlag);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not PlayerAndCarInfo Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(PlayerAndCarInfo)}'.", nameof(Value));

        Memory.BytesFromStruct(Value2.PlayerPosition, Buffer, Offset);
        Offset += Vector3.Size;
        Memory.BytesFromStruct(Value2.ForceLocation, Buffer, Offset);
        Offset += Vector3.Size;
        BitConverter.GetBytes(Value2.DirtyFlag).CopyTo(Buffer, Offset);
    }
}
