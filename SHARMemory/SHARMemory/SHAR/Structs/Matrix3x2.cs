namespace SHARMemory.SHAR.Structs
{
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

        public override string ToString() => $"{{ {{M11:{M11} M12:{M12}}} {{M21:{M21} M22:{M22}}} {{M31:{M31} M32:{M32}}} }}";
    }

    internal class Matrix3x2Struct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new Matrix3x2(Memory.ReadSingle(Address), Memory.ReadSingle(Address + sizeof(float)), Memory.ReadSingle(Address + sizeof(float) * 2), Memory.ReadSingle(Address + sizeof(float) * 3), Memory.ReadSingle(Address + sizeof(float) * 4), Memory.ReadSingle(Address + sizeof(float) * 5));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not Matrix3x2 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Matrix3x2)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2.M11);
            Memory.WriteSingle(Address + sizeof(float), Value2.M12);
            Memory.WriteSingle(Address + sizeof(float) * 2, Value2.M21);
            Memory.WriteSingle(Address + sizeof(float) * 3, Value2.M22);
            Memory.WriteSingle(Address + sizeof(float) * 4, Value2.M31);
            Memory.WriteSingle(Address + sizeof(float) * 5, Value2.M32);
        }
    }
}