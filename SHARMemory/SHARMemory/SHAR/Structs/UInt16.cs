namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(UInt16Struct))]
    public struct UInt16
    {
        public const int Size = sizeof(ushort);

        public ushort Value;

        public UInt16(ushort value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    internal class UInt16Struct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new UInt16(Memory.ReadUInt16(Address));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not UInt16 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt16)}'.", nameof(Value));

            Memory.WriteUInt16(Address, Value2.Value);
        }
    }
}