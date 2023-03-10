namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(SmootherStruct))]
    public struct Smoother
    {
        public const int Size = sizeof(float) + sizeof(float);

        public float RollingAverage;

        public float Factor;

        public Smoother(float rollingAverage, float factor)
        {
            RollingAverage = rollingAverage;
            Factor = factor;
        }
    }

    internal class SmootherStruct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new Smoother(Memory.ReadSingle(Address), Memory.ReadSingle(Address + sizeof(float)));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not Smoother Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Smoother)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2.RollingAverage);
            Memory.WriteSingle(Address + sizeof(float), Value2.Factor);
        }
    }
}
