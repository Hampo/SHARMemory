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
    }

    internal class SimVelocityStateStruct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new SimVelocityState(Memory.ReadStruct<Vector3>(Address), Memory.ReadStruct<Vector3>(Address + Vector3.Size));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not SimVelocityState Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(SimVelocityState)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.Linear);
            Memory.WriteStruct(Address + Vector3.Size, Value2.Angular);
        }
    }
}
