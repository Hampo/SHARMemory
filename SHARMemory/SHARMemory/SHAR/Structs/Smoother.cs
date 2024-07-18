using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(SmootherStruct))]
public struct Smoother
{
    public const int Size = sizeof(float) + sizeof(float);

    public float RollingAverage;

    public float Factor;

    public Smoother(float rollingAverage, float factor)
    {
        RollingAverage = rollingAverage;
        Factor = factor;
    }

    public override readonly string ToString() => $"{RollingAverage} | {Factor}";
}

internal class SmootherStruct : Struct
{
    public override int Size => Smoother.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        float RollingAverage = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Factor = BitConverter.ToSingle(Bytes, Offset);
        return new Smoother(RollingAverage, Factor);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not Smoother Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Smoother)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.RollingAverage).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Factor).CopyTo(Buffer, Offset);
    }
}
