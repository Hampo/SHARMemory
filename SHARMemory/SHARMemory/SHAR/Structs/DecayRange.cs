using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(DecayRangeStruct))]
    public struct DecayRange
    {
        public const int Size = sizeof(int) + Vector3.Size  + Vector3.Size + sizeof(float) + sizeof(float);

        public enum Types
        {
            NoDecayRange,
            SphereDecayRange,
            CuboidDecayRange,
            EllipsoidDecayRange,
        }

        public Types Type;

        public Vector3 Inner;

        public Vector3 Outer;

        public float SinRotationY;

        public float CosRotationY;

        public DecayRange(Types type, Vector3 inner, Vector3 outer, float sinRotationY, float cosRotationY)
        {
            Type = type;
            Inner = inner;
            Outer = outer;
            SinRotationY = sinRotationY;
            CosRotationY = cosRotationY;
        }

        public override string ToString() => $"{Type} | {Inner} | {Outer} | {SinRotationY} | {CosRotationY}";
    }

    internal class DecayRangeStruct : Struct
    {
        public override int Size => DecayRange.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            DecayRange.Types Type = (DecayRange.Types)BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(int);
            Vector3 Inner = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            Offset += Vector3.Size;
            Vector3 Outer = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            Offset += Vector3.Size;
            float SinRotationY = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float CosRotationY = BitConverter.ToSingle(Bytes, Offset);
            return new DecayRange(Type, Inner, Outer, SinRotationY, CosRotationY);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not DecayRange Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(DecayRange)}'.", nameof(Value));

            BitConverter.GetBytes((int)Value2.Type).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
            Memory.BytesFromStruct(Value2.Inner, Buffer, Offset);
            Offset += Vector3.Size;
            Memory.BytesFromStruct(Value2.Outer, Buffer, Offset);
            Offset += Vector3.Size;
            BitConverter.GetBytes(Value2.SinRotationY).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.CosRotationY).CopyTo(Buffer, Offset);
        }
    }
}
