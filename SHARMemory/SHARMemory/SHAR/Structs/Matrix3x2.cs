using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(Matrix3x2Struct))]
public struct Matrix3x2
{
    public const int Size = sizeof(float) * 2 * 3;

    public float M11;
    public float M12;

    public float M21;
    public float M22;

    public float M31;
    public float M32;

    public Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32)
    {
        this.M11 = M11;
        this.M12 = M12;

        this.M21 = M21;
        this.M22 = M22;

        this.M31 = M31;
        this.M32 = M32;
    }

    public override readonly string ToString() => $"{{ {{M11:{M11} M12:{M12}}} {{M21:{M21} M22:{M22}}} {{M31:{M31} M32:{M32}}} }}";
}

internal class Matrix3x2Struct : Struct
{
    public override int Size => Matrix3x2.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        float M11 = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float M12 = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float M21 = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float M22 = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float M31 = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float M32 = BitConverter.ToSingle(Bytes, Offset);
        return new Matrix3x2(M11, M12, M21, M22, M31, M32);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not Matrix3x2 Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Matrix3x2)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.M11).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.M12).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.M21).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.M22).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.M31).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.M32).CopyTo(Buffer, Offset);
    }
}