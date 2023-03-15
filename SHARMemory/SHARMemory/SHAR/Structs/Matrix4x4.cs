using SHARMemory.Memory;

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

    internal class Matrix4x4Struct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new Matrix4x4(Memory.ReadSingle(Address), Memory.ReadSingle(Address + sizeof(float)), Memory.ReadSingle(Address + sizeof(float) * 2), Memory.ReadSingle(Address + sizeof(float) * 3), Memory.ReadSingle(Address + sizeof(float) * 4), Memory.ReadSingle(Address + sizeof(float) * 5), Memory.ReadSingle(Address + sizeof(float) * 6), Memory.ReadSingle(Address + sizeof(float) * 7), Memory.ReadSingle(Address + sizeof(float) * 8), Memory.ReadSingle(Address + sizeof(float) * 9), Memory.ReadSingle(Address + sizeof(float) * 10), Memory.ReadSingle(Address + sizeof(float) * 11), Memory.ReadSingle(Address + sizeof(float) * 12), Memory.ReadSingle(Address + sizeof(float) * 13), Memory.ReadSingle(Address + sizeof(float) * 14), Memory.ReadSingle(Address + sizeof(float) * 15));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not Matrix4x4 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Matrix4x4)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2.M11);
            Memory.WriteSingle(Address + sizeof(float), Value2.M12);
            Memory.WriteSingle(Address + sizeof(float) * 2, Value2.M13);
            Memory.WriteSingle(Address + sizeof(float) * 3, Value2.M14);
            Memory.WriteSingle(Address + sizeof(float) * 4, Value2.M21);
            Memory.WriteSingle(Address + sizeof(float) * 5, Value2.M22);
            Memory.WriteSingle(Address + sizeof(float) * 6, Value2.M23);
            Memory.WriteSingle(Address + sizeof(float) * 7, Value2.M24);
            Memory.WriteSingle(Address + sizeof(float) * 8, Value2.M31);
            Memory.WriteSingle(Address + sizeof(float) * 9, Value2.M32);
            Memory.WriteSingle(Address + sizeof(float) * 10, Value2.M33);
            Memory.WriteSingle(Address + sizeof(float) * 11, Value2.M34);
            Memory.WriteSingle(Address + sizeof(float) * 12, Value2.M41);
            Memory.WriteSingle(Address + sizeof(float) * 13, Value2.M42);
            Memory.WriteSingle(Address + sizeof(float) * 14, Value2.M43);
            Memory.WriteSingle(Address + sizeof(float) * 15, Value2.M44);
        }
    }
}