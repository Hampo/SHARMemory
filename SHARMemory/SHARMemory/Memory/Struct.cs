using System;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Interface <c>Memory.IStruct</c> is an interface to handle the reading/writing a struct.
    /// </summary>
    public interface IStruct
    {
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
        object Read(ProcessMemory Memory, uint Address);

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
        void Write(ProcessMemory Memory, uint Address, object Value);
    }

    /// <summary>
    /// Class <c>Memory.StructAttribute</c> is an attribute to be given to structs to handle reading/writing.
    /// </summary>
    public class StructAttribute : Attribute
    {
        /// <summary>
        /// Gets an <see cref="IStruct"/> link to a given <paramref name="Type"/> in <paramref name="Memory"/>.
        /// </summary>
        /// <param name="Memory">
        /// The <see cref="ProcessMemory"/> instance to search for an <see cref="IStruct"/>.
        /// </param>
        /// <param name="Type">
        /// The <see cref="Type"/> to search for.
        /// </param>
        /// <returns>
        /// The <see cref="IStruct"/> linked to the <paramref name="Type"/>.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public static IStruct Get(ProcessMemory Memory, Type Type)
        {
            if (Memory.Structs.Known.TryGetValue(Type, out var Struct))
                return Struct;

            var StructAttributes = (StructAttribute[])Type.GetCustomAttributes(typeof(StructAttribute), false);
            if (StructAttributes.Length < 1)
                throw new ArgumentException($"'{nameof(Type)}' must have attribute '{nameof(SHARMemory.Memory.StructAttribute)}'.", nameof(Type));
            var StructAttribute = StructAttributes[0];

            return StructAttribute.Struct;
        }

        private readonly IStruct Struct;

        /// <summary>
        /// The <c>Memory.StructAttribute</c> constructor.
        /// </summary>
        /// <param name="Type">
        /// The struct to manage.
        /// </param>
        public StructAttribute(Type Type)
        {
            Struct = (IStruct)Activator.CreateInstance(Type);
        }
    }
}