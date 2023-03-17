using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Class <c>Memory.StructArray</c> is class to handle an array of structs.
    /// </summary>
    /// <typeparam name="T">
    /// The <c>struct</c> this is an array of.
    /// </typeparam>
    public class StructArray<T> : IEnumerable<T> where T : struct
    {
        private readonly ProcessMemory Memory;
        /// <summary>
        /// The base address of this array
        /// </summary>
        public readonly uint Address;
        private readonly uint Size;
        /// <summary>
        /// How many elements are in this array
        /// </summary>
        public readonly uint Count;

        /// <summary>
        /// Get an element of this array.
        /// </summary>
        /// <param name="index">
        /// The index to retrieve. Must be greater than or equal to <c>0</c>, and less than <see cref="Count"/>.
        /// </param>
        /// <returns>
        /// The <typeparamref name="T"/> at <paramref name="index"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if index is out of viable range.
        /// </exception>
        public T this[uint index]
        {
            get
            {
                if (index < 0)
                    throw new IndexOutOfRangeException($"Index {index} is not equal to or greater than 0.");
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}");

                return Memory.ReadStruct<T>(Address + Size * index);
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}");

                Memory.WriteStruct(Address + Size * index, value);
            }
        }

        /// <summary>
        /// The <c>Memory.StructArray</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="ProcessMemory"/> to use.
        /// </param>
        /// <param name="address">
        /// The base address for the first element in the array.
        /// </param>
        /// <param name="size">
        /// The size in bytes of <typeparamref name="T"/>.
        /// </param>
        /// <param name="count">
        /// How many elements are in this array.
        /// </param>
        public StructArray(ProcessMemory memory, uint address, uint size, uint count)
        {
            Memory = memory;
            Address = address;
            Size = size;
            Count = count;
        }

        /// <summary>
        /// Reads the whole array at once into a <see cref="Action{T}"/>.
        /// </summary>
        /// <returns>
        /// The entire array.
        /// </returns>
        public T[] ToArray()
        {
            byte[] bytes = Memory.ReadBytes(Address, Size * Count);

            T[] result = new T[Count];
            for (int i = 0; i < Count; i++)
                result[i] = Memory.StructFromBytes<T>(bytes, i * (int)Size);

            return result;
        }

        /// <summary>
        /// Writes the whole array at once into a <see cref="Action{T}"/>.
        /// </summary>
        /// <param name="array">
        /// The array to write.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws an <see cref="ArgumentException"/> if <paramref name="array"/> length doesn't match <see cref="Count"/>.
        /// </exception>
        public void FromArray(T[] array)
        {
            if (array.Length != Count)
                throw new ArgumentException($"{nameof(array)} must have a length of {Count}", nameof(array));

            byte[] bytes = new byte[Size * Count];
            for (int i = 0; i < Count; i++)
                Memory.BytesFromStruct<T>(array[i], bytes, i * (int)Size);

            Memory.WriteBytes(Address, bytes);
        }

        private class StructEnumerator : IEnumerator<T>
        {
            private readonly StructArray<T> array;
            private int position = -1;

            public T Current => array[(uint)position];
            object IEnumerator.Current => Current;

            public StructEnumerator(StructArray<T> array)
            {
                this.array = array;
            }

            public bool MoveNext()
            {
                position++;
                return position < array.Count;
            }

            public void Reset()
            {
                position = -1;
            }

            public void Dispose() { }
        }

        /// <summary>
        /// Gets the <see cref="StructEnumerator"/> for this array.
        /// </summary>
        /// <returns>
        /// A new enumerator for this array.
        /// </returns>
        public IEnumerator<T> GetEnumerator() => new StructEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Override <c>ToString</c> to provide a nicer string response.
        /// </summary>
        /// <returns>
        /// The array information
        /// </returns>
        public override string ToString() => $"{typeof(T)}[{Count}]";
    }
}
