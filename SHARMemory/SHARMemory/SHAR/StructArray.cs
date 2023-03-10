using System;
using System.Collections;
using System.Collections.Generic;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.StructArray</c> is class to handle an array of SHAR structs.
    /// </summary>
    /// <typeparam name="T">
    /// The <c>struct</c> this is an array of.
    /// </typeparam>
    public class StructArray<T> : IEnumerable<T> where T : struct
    {
        private readonly Memory Memory;
        private readonly uint Address;
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

                return Memory.ReadStruct<T>(Address + Size * (uint)index);
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}");

                Memory.WriteStruct(Address + Size * (uint)index, value);
            }
        }

        /// <summary>
        /// The <c>SHAR.StructArray</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> to use.
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
        public StructArray(Memory memory, uint address, uint size, uint count)
        {
            Memory = memory;
            Address = address;
            Size = size;
            Count = count;
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
    }
}
