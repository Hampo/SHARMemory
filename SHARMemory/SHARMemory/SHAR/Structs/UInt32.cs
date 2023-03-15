using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(UInt32Struct))]
    public struct UInt32
    {
        public const int Size = sizeof(uint);

        public uint Value;

        public UInt32(uint value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    internal class UInt32Struct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new UInt32(Memory.ReadUInt32(Address));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not UInt32 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt32)}'.", nameof(Value));

            Memory.WriteUInt32(Address, Value2.Value);
        }
    }
}