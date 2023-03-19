using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(QuaternionStruct))]
    public struct Quaternion
    {
        public const int Size = sizeof(float) * 4;

        public float W;
        public float X;
        public float Y;
        public float Z;

        public Quaternion(float value)
        {
            W = value;
            X = value;
            Y = value;
            Z = value;
        }

        public Quaternion(float w, float x, float y, float z)
        {
            W = x;
            X = x;
            Y = y;
            Z = z;
        }

        public static Quaternion Add(Quaternion Quaternion1, Quaternion Quaternion2) => new(Quaternion1.W + Quaternion2.W, Quaternion1.X + Quaternion2.X, Quaternion1.Y + Quaternion2.Y, Quaternion1.Z + Quaternion2.Z);

        public static Quaternion operator *(Quaternion Quaternion, float Value) => new(Quaternion.W * Value, Quaternion.W * Value, Quaternion.Y * Value, Quaternion.Z * Value);

        public override string ToString() => $"<{X:0.00000}, {Y:0.00000}, {Z:0.00000}>";
    }

    internal class QuaternionStruct : Struct
    {
        public override int Size => Quaternion.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            float W = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float X = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Y = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Z = BitConverter.ToSingle(Bytes, Offset);
            return new Quaternion(W, X, Y, Z);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Quaternion Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Quaternion)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.W).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.X).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Y).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Z).CopyTo(Buffer, Offset);
        }
    }
}
