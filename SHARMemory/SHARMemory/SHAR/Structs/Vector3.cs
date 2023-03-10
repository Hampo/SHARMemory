namespace SHARMemory.SHAR.Structs
{
    public struct Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

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

        public static Vector3 Add(Vector3 Vector1, Vector3 Vector2) => new Vector3(Vector1.X + Vector2.X, Vector1.Y + Vector2.Y, Vector1.Z + Vector2.Z);

        public static Vector3 operator *(Vector3 Vector, float Value) => new Vector3(Vector.X * Value, Vector.Y * Value, Vector.Z * Value);
    }
}
