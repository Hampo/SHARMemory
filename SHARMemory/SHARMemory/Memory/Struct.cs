using System;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Class <c>Memory.Struct</c> is an astract class to handle the reading/writing a struct.
    /// </summary>
    public abstract class Struct
    {
        /// <summary>
        /// The size of the object in bytes.
        /// </summary>
        public abstract int Size { get; }

        /// <summary>
        /// Converts a byte array to <c>object</c>.
        /// </summary>
        /// <param name="Memory">
        /// The <c>ProcessMemory</c> this is linked to.
        /// </param>
        /// <param name="Bytes">
        /// The byte array.
        /// </param>
        /// <param name="Offset">
        /// The start offset in the <paramref name="Bytes"/>. Defaults to <c>0</c>.
        /// </param>
        /// <returns>
        /// The <c>object</c> converted from <paramref name="Bytes"/>.
        /// </returns>
        public abstract object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0);

        /// <summary>
        /// Reads <paramref name="Memory"/> at <paramref name="Address"/>.
        /// </summary>
        /// <param name="Memory">
        /// The <c>ProcessMemory</c> to read.
        /// </param>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>object</c> at the given offset.
        /// </returns>
        public virtual object Read(ProcessMemory Memory, uint Address)
        {
            byte[] bytes = Memory.ReadBytes(Address, (uint)Size);
            return FromBytes(Memory, bytes, 0);
        }

        /// <summary>
        /// Converts <paramref name="Value"/> to a byte array.
        /// </summary>
        /// <param name="Memory">
        /// The <c>ProcessMemory</c> this is linked to.
        /// </param>
        /// <param name="Buffer">
        /// The byte array buffer to write to.
        /// </param>
        /// <param name="Value">
        /// The value to write.
        /// </param>
        /// <param name="Offset">
        /// The start offset in the <paramref name="Buffer"/>. Defaults to <c>0</c>.
        /// </param>
        public abstract void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0);

        /// <summary>
        /// Writes <paramref name="Value"/> to <paramref name="Memory"/> at <paramref name="Address"/>.
        /// </summary>
        /// <param name="Memory">
        /// The <c>ProcessMemory</c> to write to.
        /// </param>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The value to write.
        /// </param>
        public virtual void Write(ProcessMemory Memory, uint Address, object Value)
        {
            byte[] Buffer = new byte[Size];
            ToBytes(Memory, Value, Buffer, 0);
            Memory.WriteBytes(Address, Buffer);
        }
    }

    /// <summary>
    /// Class <c>Memory.StructAttribute</c> is an attribute to be given to structs to handle reading/writing.
    /// </summary>
    public class StructAttribute : Attribute
    {
        /// <summary>
        /// Gets an <see cref="Memory.Struct"/> link to a given <paramref name="Type"/> in <paramref name="Memory"/>.
        /// </summary>
        /// <param name="Memory">
        /// The <see cref="ProcessMemory"/> instance to search for an <see cref="Memory.Struct"/>.
        /// </param>
        /// <param name="Type">
        /// The <see cref="Type"/> to search for.
        /// </param>
        /// <returns>
        /// The <see cref="Memory.Struct"/> linked to the <paramref name="Type"/>.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public static Struct Get(ProcessMemory Memory, Type Type)
        {
            if (Memory.Structs.Known.TryGetValue(Type, out var Struct))
                return Struct;

            var StructAttributes = (StructAttribute[])Type.GetCustomAttributes(typeof(StructAttribute), false);
            if (StructAttributes.Length < 1)
                throw new ArgumentException($"'{nameof(Type)}' must have attribute '{nameof(SHARMemory.Memory.StructAttribute)}'.", nameof(Type));
            var StructAttribute = StructAttributes[0];

            return StructAttribute.Struct;
        }

        private readonly Struct Struct;

        /// <summary>
        /// The <c>Memory.StructAttribute</c> constructor.
        /// </summary>
        /// <param name="Type">
        /// The struct to manage.
        /// </param>
        public StructAttribute(Type Type)
        {
            Struct = (Struct)Activator.CreateInstance(Type);
        }
    }
}