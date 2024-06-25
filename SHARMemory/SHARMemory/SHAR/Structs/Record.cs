using SHARMemory.Memory;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(RecordStruct))]
    public struct Record
    {
        public const int Size = 16 + 1;

        public string Name;
        public bool Completed;

        public Record(string name, bool completed)
        {
            Name = name;
            Completed = completed;
        }

        public override string ToString() => $"{Name} | {Completed}";
    }

    internal class RecordStruct : Struct
    {
        public override int Size => Record.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
            Offset += 16;
            bool Completed = BitConverter.ToBoolean(Bytes, Offset);
            return new Record(Name, Completed);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Record Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Record)}'.", nameof(Value));

            Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
            Offset += 16;
            BitConverter.GetBytes(Value2.Completed).CopyTo(Buffer, Offset);
        }
    }
}
