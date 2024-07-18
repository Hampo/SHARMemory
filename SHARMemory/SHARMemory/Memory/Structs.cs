using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;

namespace SHARMemory.Memory;

/// <summary>
/// Interface <c>SHAR.IStruct</c> is an interface to handle the reading/writing a SHAR struct.
/// </summary>
public sealed class Structs
{
    /// <summary>
    /// A <c>Dictionary</c> to map <see cref="Type"/> to <see cref="Struct"/>.
    /// </summary>
    public readonly Dictionary<Type, Struct> Known = new()
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

    private class ByteStruct : Struct
    {
        public override int Size => sizeof(byte);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => Bytes[Offset];

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadByte(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not byte Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Buffer[Offset] = Value2;
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not byte Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteByte(Address, Value2);
        }
    }

    private class BooleanStruct : Struct
    {
        public override int Size => sizeof(bool);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToBoolean(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadBoolean(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not bool Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Boolean)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not bool Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteBoolean(Address, Value2);
        }
    }

    private class DoubleStruct : Struct
    {
        public override int Size => sizeof(double);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToDouble(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadDouble(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not double Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Double)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not double Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteDouble(Address, Value2);
        }
    }

    private class SingleStruct : Struct
    {
        public override int Size => sizeof(float);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToSingle(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadSingle(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not float Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Single)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not float Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2);
        }
    }

    private class Int16Struct : Struct
    {
        public override int Size => sizeof(short);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToInt16(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt16(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not short Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int16)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not short Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteInt16(Address, Value2);
        }
    }

    private class Int32Struct : Struct
    {
        public override int Size => sizeof(int);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToInt32(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt32(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not int Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int32)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not int Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2);
        }
    }

    private class Int64Struct : Struct
    {
        public override int Size => sizeof(long);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToInt64(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadInt64(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not long Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Int64)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not long Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteInt64(Address, Value2);
        }
    }

    private class UInt16Struct : Struct
    {
        public override int Size => sizeof(ushort);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToUInt16(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt16(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not ushort Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt16)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not ushort Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteUInt16(Address, Value2);
        }
    }

    private class UInt32Struct : Struct
    {
        public override int Size => sizeof(uint);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToUInt32(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt32(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not uint Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt32)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not uint Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteUInt32(Address, Value2);
        }
    }

    private class UInt64Struct : Struct
    {
        public override int Size => sizeof(ulong);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => BitConverter.ToUInt64(Bytes, Offset);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadUInt64(Address);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not ulong Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(UInt64)}'.", nameof(Value));

            BitConverter.GetBytes(Value2).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not ulong Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteUInt64(Address, Value2);
        }
    }

    private class NullStringPointerStruct : Struct
    {
        public override int Size => sizeof(uint);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => Memory.ReadNullString(BitConverter.ToUInt32(Bytes, Offset), Encoding.UTF8);

        public override object Read(ProcessMemory Memory, uint Address) => Memory.ReadNullStringPointer(Address, Encoding.UTF8);

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0) => throw new NotSupportedException();

        public override void Write(ProcessMemory Memory, uint Address, object Value) => throw new NotSupportedException();
    }

    private class ColorStruct : Struct
    {
        public override int Size => sizeof(int);

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0) => Color.FromArgb(BitConverter.ToInt32(Bytes, Offset));

        public override object Read(ProcessMemory Memory, uint Address) => Color.FromArgb(Memory.ReadInt32(Address));

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not Color Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Color)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.ToArgb()).CopyTo(Buffer, Offset);
        }

        public override void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not Color Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Byte)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.ToArgb());
        }
    }
}