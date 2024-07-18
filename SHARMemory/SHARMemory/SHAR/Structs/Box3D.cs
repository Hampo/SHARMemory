using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(Box3DStruct))]
public struct Box3D
{
    public const int Size = Vector3.Size + Vector3.Size;

    public Vector3 Low;

    public Vector3 High;

    public readonly Vector3 Mid
    {
        get
        {
            Vector3 Mid = Vector3.Add(Low, High);
            Mid *= .5f;
            return Mid;
        }
    }

    public Box3D(Vector3 low, Vector3 high)
    {
        Low = low;
        High = high;
    }

    public override readonly string ToString() => $"{Low} | {High}";
}

internal class Box3DStruct : Struct
{
    public override int Size => Box3D.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Vector3 Low = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        Vector3 High = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        return new Box3D(Low, High);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not Box3D Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Box3D)}'.", nameof(Value));

        Memory.BytesFromStruct(Value2.Low, Buffer, Offset);
        Offset += Vector3.Size;
        Memory.BytesFromStruct(Value2.High, Buffer, Offset);
    }
}
