using SHARMemory.Memory;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// A static class to add SHAR specific functions to <see cref="PointerArray{T}"/>.
    /// </summary>
    public static class PointerArrayExtensions
    {

        /// <summary>
        /// Creates a <see cref="PointerArray{T}"/> from an <c>IPtrArray</c> in SHAR.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="ProcessMemory"/> to use.
        /// </param>
        /// <param name="class">
        /// The <see cref="Class"/> the array is in.
        /// </param>
        /// <param name="offset">
        /// The address offset in the class.
        /// </param>
        /// <returns>
        /// A new <see cref="PointerArray{T}"/> at the <paramref name="offset"/> in <paramref name="class"/>.
        /// </returns>
        public static PointerArray<T> FromPtrArray<T>(ProcessMemory memory, Class @class, uint offset) where T : Class => new(memory, @class.ReadUInt32(offset + 8), @class.ReadUInt32(offset + 4));

        /// <summary>
        /// Creates a <see cref="PointerArray{T}"/> from an <c>rVector</c> in SHAR.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> to use.
        /// </param>
        /// <param name="class">
        /// The <see cref="Class"/> the array is in.
        /// </param>
        /// <param name="offset">
        /// The address offset in the class.
        /// </param>
        /// <returns>
        /// A new <see cref="PointerArray{T}"/> at the <paramref name="offset"/> in <paramref name="class"/>.
        /// </returns>
        public static PointerArray<T> FromVector<T>(ProcessMemory memory, Class @class, uint offset) where T : Class => new(memory, @class.ReadUInt32(offset + 4), @class.ReadUInt32(offset + 8));

        /// <summary>
        /// Creates a <see cref="PointerArray{T}"/> from an <c>IPtrArray</c> in SHAR.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> to use.
        /// </param>
        /// <param name="pointer">
        /// The <see cref="Pointer"/> the array is in.
        /// </param>
        /// <param name="offset">
        /// The address offset in the class.
        /// </param>
        /// <returns>
        /// A new <see cref="PointerArray{T}"/> at the <paramref name="offset"/> in <paramref name="pointer"/>.
        /// </returns>
        public static PointerArray<T> FromPtrArray<T>(ProcessMemory memory, Pointer pointer, uint offset) where T : Class => new(memory, pointer.ReadUInt32(offset + 8), pointer.ReadUInt32(offset + 4));

        /// <summary>
        /// Creates a <see cref="PointerArray{T}"/> from an <c>rVector</c> in SHAR.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> to use.
        /// </param>
        /// <param name="pointer">
        /// The <see cref="Pointer"/> the array is in.
        /// </param>
        /// <param name="offset">
        /// The address offset in the class.
        /// </param>
        /// <returns>
        /// A new <see cref="PointerArray{T}"/> at the <paramref name="offset"/> in <paramref name="pointer"/>.
        /// </returns>
        public static PointerArray<T> FromVector<T>(ProcessMemory memory, Pointer pointer, uint offset) where T : Class => new(memory, pointer.ReadUInt32(offset + 4), pointer.ReadUInt32(offset + 8));
    }
}
