using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(SimVelocityStateStruct))]
    public struct SimVelocityState
    {
        public const int Size = Vector3.Size + Vector3.Size;

        public Vector3 Linear;

        public Vector3 Angular;

        public SimVelocityState(Vector3 linear, Vector3 angular)
        {
            Linear = linear;
            Angular = angular;
        }

        public override string ToString() => $"{Linear} | {Angular}";
    }

    internal class SimVelocityStateStruct : Struct
    {
        public override int Size => SimVelocityState.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            Vector3 Linear = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            Offset += Vector3.Size;
            Vector3 Angular = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            return new SimVelocityState(Linear, Angular);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not SimVelocityState Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(SimVelocityState)}'.", nameof(Value));

            Memory.BytesFromStruct(Value2.Linear, Buffer, Offset);
            Offset += Vector3.Size;
            Memory.BytesFromStruct(Value2.Angular, Buffer, Offset);
        }
    }
}
