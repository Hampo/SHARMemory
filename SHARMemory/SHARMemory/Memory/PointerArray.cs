using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace SHARMemory.Memory
{
    /// <summary>
    /// Class <c>Memory.PointerArray</c> is class to handle an array of pointers.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Class" /> this is an array of.
    /// </typeparam>
    public class PointerArray<T> : IEnumerable<T> where T : Class
    {
        private readonly ProcessMemory Memory;
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
        public T this[uint index]
        {
            get
            {
                if (index < 0)
                    throw new IndexOutOfRangeException($"Index {index} is not equal to or greater than 0.");
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}.");

                return Memory.ClassFactory.Create<T>(Memory.ReadUInt32(Address + index * sizeof(uint)));
            }
            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException($"Index {index} is not equal to or greater than 0.");
                if (index >= Count)
                    throw new IndexOutOfRangeException($"Index {index} is outside range {Count}.");

                Memory.WriteUInt32(Address + index * sizeof(uint), value?.Address ?? 0);
            }
        }

        /// <summary>
        /// The <c>Memory.PointerArray</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="ProcessMemory"/> to use.
        /// </param>
        /// <param name="address">
        /// The base address for the first element in the array.
        /// </param>
        /// <param name="count">
        /// How many elements are in this array.
        /// </param>
        public PointerArray(ProcessMemory memory, uint address, uint count)
        {
            Memory = memory;
            Address = address;
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
            byte[] bytes = Memory.ReadBytes(Address, sizeof(uint) * Count);

            T[] result = new T[Count];
            for (int i = 0; i < Count; i++)
                result[i] = Memory.ClassFactory.Create<T>(BitConverter.ToUInt32(bytes, i * sizeof(uint)));

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

            byte[] bytes = new byte[sizeof(uint) * Count];
            for (int i = 0; i < Count; i++)
                BitConverter.GetBytes(array[i]?.Address ?? 0).CopyTo(bytes, i * sizeof(uint));

            Memory.WriteBytes(Address, bytes);
        }

        private class PointerEnumerator : IEnumerator<T>
        {
            private readonly PointerArray<T> array;
            private int position = -1;

            public T Current => array[(uint)position];
            object IEnumerator.Current => Current;

            public PointerEnumerator(PointerArray<T> array)
            {
                this.array = array;
            }

            public bool MoveNext()
            {
                do
                {
                    position++;
                    if (position >= array.Count)
                        return false;
                }
                while (Current == null);
                return true;
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

        /// <summary>
        /// Override <c>ToString</c> to provide a nicer string response.
        /// </summary>
        /// <returns>
        /// The array information
        /// </returns>
        public override string ToString() => $"{typeof(T)}[{Count}]";
    }
}
