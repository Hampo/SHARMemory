using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Interface <c>SHAR.IStruct</c> is an interface to handle the reading/writing a SHAR struct.
    /// </summary>
    public sealed class Structs
    {
        /// <summary>
        /// A <c>Dictionary</c> to map <see cref="Type"/> to <see cref="IStruct"/>.
        /// </summary>
        public readonly Dictionary<Type, IStruct> Known = new()
        {
            { typeof(byte), new ByteStruct() },
            { typeof(bool), new BooleanStruct() },
            { typeof(double), new DoubleStruct() },
            { typeof(float), new SingleStruct() },
            { typeof(short), new Int16Struct() },
            { typeof(int), new Int32Struct() },
            { typeof(long), new Int64Struct() },
            { typeof(ushort), new UInt16Struct() },
            { typeof(uint), new UInt32Struct() },
            { typeof(ulong), new UInt64Struct() },
            { typeof(string), new NullStringPointerStruct() },
            { typeof(Color), new ColorStruct() },
        };

        internal Structs()
        {

        }

        private class ByteStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadByte(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not byte Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

                Memory.WriteByte(Address, Value2);
            }
        }

        private class BooleanStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadBoolean(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not bool Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Boolean)}'.", nameof(Value));

                Memory.WriteBoolean(Address, Value2);
            }
        }

        private class DoubleStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadDouble(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not double Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Double)}'.", nameof(Value));

                Memory.WriteDouble(Address, Value2);
            }
        }

        private class SingleStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadSingle(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not float Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Single)}'.", nameof(Value));

                Memory.WriteSingle(Address, Value2);
            }
        }

        private class Int16Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt16(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not short Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int16)}'.", nameof(Value));

                Memory.WriteInt16(Address, Value2);
            }
        }

        private class Int32Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt32(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not int Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int32)}'.", nameof(Value));

                Memory.WriteInt32(Address, Value2);
            }
        }

        private class Int64Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt64(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not long Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int64)}'.", nameof(Value));

                Memory.WriteInt64(Address, Value2);
            }
        }

        private class UInt16Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt16(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not ushort Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt16)}'.", nameof(Value));

                Memory.WriteUInt16(Address, Value2);
            }
        }

        private class UInt32Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt32(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not uint Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt32)}'.", nameof(Value));

                Memory.WriteUInt32(Address, Value2);
            }
        }

        private class UInt64Struct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt64(Address);

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not ulong Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt64)}'.", nameof(Value));

                Memory.WriteUInt64(Address, Value2);
            }
        }

        private class NullStringPointerStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Memory.ReadNullStringPointer(Address, Encoding.UTF8);
            public void Write(ProcessMemory Memory, uint Address, object Value) => throw new NotSupportedException();
        }

        private class ColorStruct : IStruct
        {
            public object Read(ProcessMemory Memory, uint Address) => Color.FromArgb(Memory.ReadInt32(Address));

            public void Write(ProcessMemory Memory, uint Address, object Value)
            {
                if (Value is not Color Value2)
                    throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Color)}'.", nameof(Value));

                Memory.WriteInt32(Address, Value2.ToArgb());
            }
        }
    }
}