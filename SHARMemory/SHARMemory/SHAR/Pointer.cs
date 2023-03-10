using SHARMemory.SHAR.Structs;
using System.Text;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.Pointer</c> is an abstract class representing a single instance of a SHAR pointer.
    /// </summary>
    public abstract class Pointer
    {
        /// <summary>
        /// The <see cref="SHAR.Memory"/> manager this pointer is linked to.
        /// </summary>
        public Memory Memory { get; }
        /// <summary>
        /// The base address of this pointer in memory.
        /// </summary>
        public uint Address { get; }
        /// <summary>
        /// The current value of the pointer.
        /// </summary>
        public uint Value => Memory.ReadUInt32(Address);
        /// <summary>
        /// A <c>bool</c> representing if the current value of the pointer in memory is valid.
        /// </summary>
        /// <example>
        /// Checks to see if <see cref="Pointers.HitNRunManager"/> is valid, and if it is triggers <c>HitAndRun</c>.
        /// <code>
        /// SHAR.Pointers.HitNRunManager HitNRunManager = memory.HitNRunManager;
        /// if (player.IsPointerValid)
        ///     HitNRunManager.HitAndRun = 100f;
        /// </code>
        /// </example>
        public bool IsPointerValid => Value != 0;

        /// <summary>
        /// The <c>SHAR.Pointer</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> manager this pointer is linked to.
        /// </param>
        /// <param name="address">
        /// The base address of this pointer in memory.
        /// </param>
        public Pointer(Memory memory, uint address)
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
        /// <returns>
        /// The <c>Vector3</c> at the given offset.
        /// </returns>
        public Vector3 ReadVector3(uint Offset) => Memory.ReadVector3(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>Matrix3x2</c> at the given offset.
        /// </returns>
        public Matrix3x2 ReadMatrix3x2(uint Offset) => Memory.ReadMatrix3x2(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>Matrix4x4</c> at the given offset.
        /// </returns>
        public Matrix4x4 ReadMatrix4x4(uint Offset) => Memory.ReadMatrix4x4(Value + Offset);

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
        /// <returns>
        /// The <c>Box3D</c> at the given offset.
        /// </returns>
        public Box3D ReadBox3D(uint Offset) => Memory.ReadBox3D(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>Sphere</c> at the given offset.
        /// </returns>
        public Sphere ReadSphere(uint Offset) => Memory.ReadSphere(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>Smoother</c> at the given offset.
        /// </returns>
        public Smoother ReadSmoother(uint Offset) => Memory.ReadSmoother(Value + Offset);

        /// <summary>
        /// Reads <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to read.
        /// </param>
        /// <returns>
        /// The <c>SimVelocityState</c> at the given offset.
        /// </returns>
        public SimVelocityState ReadSimVelocityState(uint Offset) => Memory.ReadSimVelocityState(Value + Offset);

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
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Vector3</c> value to write.
        /// </param>
        public void WriteVector3(uint Offset, Vector3 Value) => Memory.WriteVector3(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Matrix3x2</c> value to write.
        /// </param>
        public void WriteMatrix3x2(uint Offset, Matrix3x2 Value) => Memory.WriteMatrix3x2(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Matrix4x4</c> value to write.
        /// </param>
        public void WriteMatrix4x4(uint Offset, Matrix4x4 Value) => Memory.WriteMatrix4x4(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Box3D</c> value to write.
        /// </param>
        public void WriteBox3D(uint Offset, Box3D Value) => Memory.WriteBox3D(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Sphere</c> value to write.
        /// </param>
        public void WriteSphere(uint Offset, Sphere Value) => Memory.WriteSphere(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Smoother</c> value to write.
        /// </param>
        public void WriteSmoother(uint Offset, Smoother Value) => Memory.WriteSmoother(this.Value + Offset, Value);

        /// <summary>
        /// Writes the given value to <see cref="Memory"/> at the class's pointer <see cref="Value"/> + <paramref name="Offset"/>.
        /// </summary>
        /// <param name="Offset">
        /// The offset to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>SimVelocityState</c> value to write.
        /// </param>
        public void WriteSimVelocityState(uint Offset, SimVelocityState Value) => Memory.WriteSimVelocityState(this.Value + Offset, Value);
    }
}
