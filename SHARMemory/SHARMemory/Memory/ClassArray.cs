using System;
using System.Collections;
using System.Collections.Generic;

namespace SHARMemory.Memory;

/// <summary>
/// Class <c>Memory.ClassArray</c> is class to handle an array of classes.
/// </summary>
/// <typeparam name="T">
/// The <see cref="Class" /> this is an array of.
/// </typeparam>
public class ClassArray<T> : IEnumerable<T> where T : Class
{
    private readonly ProcessMemory Memory;
    /// <summary>
    /// The base address of the array.
    /// </summary>
    public readonly uint Address;
    /// <summary>
    /// The size of each class.
    /// </summary>
    public readonly uint Size;
    /// <summary>
    /// How many elements are in this array.
    /// </summary>
    public readonly int Count;

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

            return Memory.ClassFactory.Create<T>(Address + (uint)index * Size);
        }
    }

    /// <summary>
    /// The <c>Memory.ClassArray</c> constructor.
    /// </summary>
    /// <param name="memory">
    /// The <see cref="ProcessMemory"/> to use.
    /// </param>
    /// <param name="address">
    /// The base address for the first element in the array.
    /// </param>
    /// <param name="size">
    /// The size of each class.
    /// </param>
    /// <param name="count">
    /// How many elements are in this array.
    /// </param>
    public ClassArray(ProcessMemory memory, uint address, uint size, int count)
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
        T[] result = new T[Count];
        for (uint i = 0; i < Count; i++)
            result[i] = Memory.ClassFactory.Create<T>(Address + i * Size);

        return result;
    }

    private class ClassEnumerator : IEnumerator<T>
    {
        private readonly T[] array;
        private int position = -1;

        public T Current => array[position];
        object IEnumerator.Current => Current;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "Causes infinite loop")]
        public ClassEnumerator(ClassArray<T> array) => this.array = array.ToArray();

        public bool MoveNext()
        {
            do
            {
                position++;
                if (position >= array.Length)
                    return false;
            }
            while (Current == null);
            return true;
        }

        public void Reset() => position = -1;

        public void Dispose() { }
    }

    /// <summary>
    /// Gets the <see cref="ClassEnumerator"/> for this array.
    /// </summary>
    /// <returns>
    /// A new enumerator for this array.
    /// </returns>
    public IEnumerator<T> GetEnumerator() => new ClassEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Override <c>ToString</c> to provide a nicer string response.
    /// </summary>
    /// <returns>
    /// The array information
    /// </returns>
    public override string ToString() => $"{typeof(T)}[{Count}]";
}
