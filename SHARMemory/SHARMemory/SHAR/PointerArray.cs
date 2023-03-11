using System;
using System.Collections;
using System.Collections.Generic;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.PointerArray</c> is class to handle an array of SHAR pointers.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Class" /> this is an array of.
    /// </typeparam>
    public class PointerArray<T> : IEnumerable<T> where T : Class
    {
        private readonly Memory Memory;
        private readonly uint Address;
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
        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new IndexOutOfRangeException($"Index {index} is not equal to or greater than 0.");
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}.");

                return Memory.CreateClass<T>(Memory.ReadUInt32(Address + (uint)index * 4));
            }
        }

        /// <summary>
        /// The <c>SHAR.PointerArray</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> to use.
        /// </param>
        /// <param name="address">
        /// The base address for the first element in the array.
        /// </param>
        /// <param name="count">
        /// How many elements are in this array.
        /// </param>
        public PointerArray(Memory memory, uint address, uint count)
        {
            Memory = memory;
            Address = address;
            Count = count;
        }

        private class PointerEnumerator : IEnumerator<T>
        {
            private readonly PointerArray<T> array;
            private int position = -1;

            public T Current => array[position];
            object IEnumerator.Current => Current;

            public PointerEnumerator(PointerArray<T> array)
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
        /// Gets the <see cref="PointerEnumerator"/> for this array.
        /// </summary>
        /// <returns>
        /// A new enumerator for this array.
        /// </returns>
        public IEnumerator<T> GetEnumerator() => new PointerEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
