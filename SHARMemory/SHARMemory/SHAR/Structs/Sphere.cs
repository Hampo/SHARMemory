using SHARMemory.Memory;
using System;

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

    internal class SphereStruct : Struct
    {
        public override int Size => Sphere.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            Vector3 Centre = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            Offset += Vector3.Size;
            float Radius = BitConverter.ToSingle(Bytes, Offset);
            return new Sphere(Centre, Radius);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Sphere Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Sphere)}'.", nameof(Value));

            Memory.BytesFromStruct(Value2.Centre, Buffer, Offset);
            Offset += Vector3.Size;
            BitConverter.GetBytes(Value2.Radius).CopyTo(Buffer, Offset);
        }
    }
}
