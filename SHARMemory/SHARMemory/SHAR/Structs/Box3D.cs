namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(Box3DStruct))]
    public struct Box3D
    {
        public const int Size = Vector3.Size + Vector3.Size;

        public Vector3 Low;

        public Vector3 High;

        public Vector3 Mid
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

        public override string ToString() => $"{Low} | {High}";
    }

    internal class Box3DStruct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new Box3D(Memory.ReadStruct<Vector3>(Address), Memory.ReadStruct<Vector3>(Address + Vector3.Size));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not Box3D Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Box3D)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.Low);
            Memory.WriteStruct(Address + Vector3.Size, Value2.High);
        }
    }
}
