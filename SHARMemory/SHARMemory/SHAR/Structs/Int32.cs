using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(Int32Struct))]
    public struct Int32
    {
        public const int Size = sizeof(int);

        public int Value;

        public Int32(int value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    internal class Int32Struct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new Int32(Memory.ReadInt32(Address));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not Int32 Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int32)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.Value);
        }
    }
}