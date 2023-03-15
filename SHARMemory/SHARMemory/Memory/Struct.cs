using System;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Interface <c>SHAR.IStruct</c> is an interface to handle the reading/writing a SHAR struct.
    /// </summary>
    public interface IStruct
    {
        /// <summary>
        /// Reads <paramref name="Memory"/> at <paramref name="Address"/>.
        /// </summary>
        /// <param name="Memory">
        /// The <c>SHAR.Memory</c> to read.
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
        /// The <c>SHAR.Memory</c> to write to.
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
    /// Class <c>SHAR.StructAttribute</c> is an attribute to be given to SHAR structs to handle reading/writing.
    /// </summary>
    public class StructAttribute : Attribute
    {
        /// <summary>
        /// Gets the <see cref="IStruct"/> related to the given SHAR struct.
        /// </summary>
        /// <param name="Type">
        /// The struct to get.
        /// </param>
        /// <returns>
        /// The <see cref="IStruct"/> interface for the given SHAR struct.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throws if the given <paramref name="Type"/> does not have a <see cref="StructAttribute"/>.
        /// </exception>
        public static IStruct Get(Type Type)
        {
            var StructAttributes = (StructAttribute[])Type.GetCustomAttributes(typeof(StructAttribute), false);
            if (StructAttributes.Length < 1)
                throw new ArgumentException($"'{nameof(Type)}' must have attribute '{nameof(Memory.StructAttribute)}'.", nameof(Type));
            var StructAttribute = StructAttributes[0];

            return StructAttribute.Struct;
        }

        private readonly IStruct Struct;

        /// <summary>
        /// The <c>SHAR.StructAttribute</c> constructor.
        /// </summary>
        /// <param name="Type">
        /// The SHAR struct to manage.
        /// </param>
        public StructAttribute(Type Type)
        {
            Struct = (IStruct)Activator.CreateInstance(Type);
        }
    }
}