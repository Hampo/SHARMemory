using System;
using System.Text;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Class <c>Memory.Pointer</c> is an abstract class representing a single instance of a pointer.
    /// </summary>
    public abstract class Pointer
    {
        /// <summary>
        /// The <see cref="ProcessMemory"/> manager this pointer is linked to.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public ProcessMemory Memory { get; }
        /// <summary>
        /// The base address of this pointer in memory.
        /// </summary>
        public uint Address { get; }
        /// <summary>
        /// The current value of the pointer.
        /// </summary>
        public virtual uint Value => Memory.ReadUInt32(Address);
        /// <summary>
        /// A <c>bool</c> representing if the current value of the pointer in memory is valid.
        /// </summary>
        public bool IsPointerValid => Value != 0;

        /// <summary>
        /// The <c>Memory.Pointer</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="ProcessMemory"/> manager this pointer is linked to.
        /// </param>
        /// <param name="address">
        /// The base address of this pointer in memory.
        /// </param>
        public Pointer(ProcessMemory memory, uint address)
        {
            Memory = memory;
            Address = address;
        }

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>byte</c> at the given offset.
        /// </returns>
        public byte ReadByte(uint Offset) => Memory.ReadByte(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>bool</c> at the given offset.
        /// </returns>
        public bool ReadBoolean(uint Offset) => Memory.ReadBoolean(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <param name="Length">
        /// The number of bytes to read
        /// </param>
        /// <returns>
        /// The <c>byte[]</c> at the given offset.
        /// </returns>
        public byte[] ReadBytes(uint Offset, uint Length) => Memory.ReadBytes(Value + Offset, Length);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>double</c> at the given offset.
        /// </returns>
        public double ReadDouble(uint Offset) => Memory.ReadDouble(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>float</c> at the given offset.
        /// </returns>
        public float ReadSingle(uint Offset) => Memory.ReadSingle(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>short</c> at the given offset.
        /// </returns>
        public short ReadInt16(uint Offset) => Memory.ReadInt16(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>int</c> at the given offset.
        /// </returns>
        public int ReadInt32(uint Offset) => Memory.ReadInt32(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>long</c> at the given offset.
        /// </returns>
        public long ReadInt64(uint Offset) => Memory.ReadInt64(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>ushort</c> at the given offset.
        /// </returns>
        public ushort ReadUInt16(uint Offset) => Memory.ReadUInt16(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>uint</c> at the given offset.
        /// </returns>
        public uint ReadUInt32(uint Offset) => Memory.ReadUInt32(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>ulong</c> at the given offset.
        /// </returns>
        public ulong ReadUInt64(uint Offset) => Memory.ReadUInt64(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <param name="Encoding">
        /// The character encoding to use.
        /// </param>
        /// <param name="maxLength">
        /// The maximum length of the string. Default to <c>512</c>.
        /// </param>
        /// <returns>
        /// The <c>string</c> at the given offset.
        /// </returns>
        public string ReadString(uint Offset, Encoding Encoding, uint maxLength = 512) => Memory.ReadString(Value + Offset, Encoding, maxLength);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <param name="Encoding">
        /// The character encoding to use.
        /// </param>
        /// <returns>
        /// The <c>string</c> at the given offset.
        /// </returns>
        public string ReadNullString(uint Offset, Encoding Encoding) => Memory.ReadNullString(Value + Offset, Encoding);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <param name="Encoding">
        /// The character encoding to use.
        /// </param>
        /// <returns>
        /// The <c>string</c> pointer at the given offset.
        /// </returns>
        public string ReadNullStringPointer(uint Offset, Encoding Encoding) => Memory.ReadNullStringPointer(Value + Offset, Encoding);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Type">
        /// The type to read.
        /// </param>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <paramref name="Type"/> at the given offset.
        /// </returns>
        public object ReadStruct(Type Type, uint Offset) => Memory.ReadStruct(Type, Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>T</c> at the given offset.
        /// </returns>
        public T ReadStruct<T>(uint Offset) => (T)ReadStruct(typeof(T), Offset);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>byte</c> value to write.
        /// </param>
        public void WriteByte(uint Offset, byte Value) => Memory.WriteByte(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>bool</c> value to write.
        /// </param>
        public void WriteBoolean(uint Offset, bool Value) => Memory.WriteBoolean(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>byte[]</c> value to write.
        /// </param>
        public void WriteBytes(uint Offset, byte[] Value) => Memory.WriteBytes(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>double</c> value to write.
        /// </param>
        public void WriteDouble(uint Offset, double Value) => Memory.WriteDouble(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>float</c> value to write.
        /// </param>
        public void WriteSingle(uint Offset, float Value) => Memory.WriteSingle(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>short</c> value to write.
        /// </param>
        public void WriteInt16(uint Offset, short Value) => Memory.WriteInt16(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>int</c> value to write.
        /// </param>
        public void WriteInt32(uint Offset, int Value) => Memory.WriteInt32(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>long</c> value to write.
        /// </param>
        public void WriteInt64(uint Offset, long Value) => Memory.WriteInt64(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>ushort</c> value to write.
        /// </param>
        public void WriteUInt16(uint Offset, ushort Value) => Memory.WriteUInt16(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>uint</c> value to write.
        /// </param>
        public void WriteUInt32(uint Offset, uint Value) => Memory.WriteUInt32(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>ulong</c> value to write.
        /// </param>
        public void WriteUInt64(uint Offset, ulong Value) => Memory.WriteUInt64(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's base <see cref="Address"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>string</c> value to write.
        /// </param>
        /// <param name="Encoding">
        /// The character encoding to use.
        /// </param>
        /// <param name="Length">
        /// The number of bytes to write.
        /// </param>
        public void WriteString(uint Offset, string Value, Encoding Encoding, int Length) => Memory.WriteString(this.Value + Offset, Value, Encoding, Length);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Type">
        /// The type to write.
        /// </param>
        /// <param name="Offset">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <paramref name="Type"/> value to write.
        /// </param>
        public void WriteStruct(Type Type, uint Offset, object Value) => Memory.WriteStruct(Type, this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>T</c> value to write.
        /// </param>
        public void WriteStruct<T>(uint Offset, T Value) => WriteStruct(typeof(T), Offset, Value);

        /// <summary>
        /// Override <c>ToString</c> to provide a nicer string response.
        /// </summary>
        /// <returns>
        /// The hex value of <see cref="Address"/> and <see cref="Value"/>
        /// </returns>
        public override string ToString() => $"(0x{Address:X} | 0x{Value:X})";
    }
}
