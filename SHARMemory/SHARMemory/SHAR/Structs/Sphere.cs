using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(SphereStruct))]
    public struct Sphere
    {
        public const int Size = Vector3.Size + sizeof(float);

        public Vector3 Centre;

        public float Radius;

        public Sphere(Vector3 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }

        public override string ToString() => $"{Centre} | {Radius}";
    }

    internal class SphereStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new Sphere(Memory.ReadStruct<Vector3>(Address), Memory.ReadSingle(Address + Vector3.Size));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not Sphere Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Sphere)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.Centre);
            Memory.WriteSingle(Address + Vector3.Size, Value2.Radius);
        }
    }
}
