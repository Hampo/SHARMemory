using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(Matrix4x4Struct))]
    public struct Matrix4x4
    {
        public const int Size = sizeof(float) * 4 * 4;

        public float M11;
        public float M12;
        public float M13;
        public float M14;

        public float M21;
        public float M22;
        public float M23;
        public float M24;

        public float M31;
        public float M32;
        public float M33;
        public float M34;

        public float M41;
        public float M42;
        public float M43;
        public float M44;

        public Matrix4x4(float M11, float M12, float M13, float M14,  float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;

            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;

            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;

            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
        }

        public override string ToString() => $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";
    }

    internal class Matrix4x4Struct : Struct
    {
        public override int Size => Matrix4x4.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            float M11 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M12 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M13 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M14 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M21 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M22 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M23 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M24 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M31 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M32 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M33 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M34 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M41 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M42 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M43 = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float M44 = BitConverter.ToSingle(Bytes, Offset);
            return new Matrix4x4(M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Matrix4x4 Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Matrix4x4)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.M11).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M12).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M13).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M14).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M21).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M22).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M23).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M24).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M31).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M32).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M33).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M34).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M41).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M42).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M43).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.M44).CopyTo(Buffer, Offset);
        }
    }
}