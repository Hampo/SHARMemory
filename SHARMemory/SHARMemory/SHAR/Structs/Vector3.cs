namespace SHARMemory.SHAR.Structs
{
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

        public static Vector3 Add(Vector3 Vector1, Vector3 Vector2) => new(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z);

        public static Vector3 operator *(Vector3 Vector, float Value) => new(Vector.X * Value, Vector.Y * Value, Vector.Z * Value);
    }

    internal class Vector3Struct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new Vector3(Memory.ReadSingle(Address), Memory.ReadSingle(Address + sizeof(float)), Memory.ReadSingle(Address + sizeof(float) + sizeof(float)));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not Vector3 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Vector3)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2.X);
            Memory.WriteSingle(Address + sizeof(float), Value2.Y);
            Memory.WriteSingle(Address + sizeof(float) + sizeof(float), Value2.Z);
        }
    }
}
