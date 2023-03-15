using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(BoolStruct))]
    public struct Bool
    {
        public const int Size = sizeof(bool);

        public bool Value;

        public Bool(bool value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

    internal class BoolStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new Bool(Memory.ReadBoolean(Address));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not Bool Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Bool)}'.", nameof(Value));

            Memory.WriteBoolean(Address, Value2.Value);
        }
    }
}