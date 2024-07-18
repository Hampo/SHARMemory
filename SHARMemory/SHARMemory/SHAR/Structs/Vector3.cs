using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(Vector3Struct))]
public struct Vector3
{
    public const int Size = sizeof(float) * 3;

    public float X;
    public float Y;
    public float Z;

    public Vector3(float value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void Set(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3 Add(Vector3 Vector1, Vector3 Vector2) => new(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z);

    public void Add(Vector3 vector)
    {
        X += vector.X;
        Y += vector.Y;
        Z += vector.Z;
    }

    public static Vector3 Sub(Vector3 Vector1, Vector3 Vector2) => new(Vector1.X - Vector2.X, Vector1.Y - Vector2.Y, Vector1.Z - Vector2.Z);

    public void Sub(Vector3 vector)
    {
        X -= vector.X;
        Y -= vector.Y;
        Z -= vector.Z;
    }

    public readonly double Magnitude() => Math.Sqrt(X * X + Y * Y + Z * Z);

    public void Normalize()
    {
        double mag = 1f / Magnitude();
        X = (float)(X * mag);
        Y = (float)(Y * mag);
        Z = (float)(Z * mag);
    }
    
    public readonly float DotProduct(Vector3 vector) => X * vector.X + Y * vector.Y + Z * vector.Z;

    public static Vector3 operator *(Vector3 Vector, float Value) => new(Vector.X * Value, Vector.Y * Value, Vector.Z * Value);

    public override readonly string ToString() => $"<{X:0.00000}, {Y:0.00000}, {Z:0.00000}>";
}

internal class Vector3Struct : Struct
{
    public override int Size => Vector3.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        float X = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Y = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Z = BitConverter.ToSingle(Bytes, Offset);
        return new Vector3(X, Y, Z);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not Vector3 Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Vector3)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.X).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Y).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Z).CopyTo(Buffer, Offset);
    }
}
