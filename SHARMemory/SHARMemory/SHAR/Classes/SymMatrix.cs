using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSymMatrix@sim@@")]
public class SymMatrix : Class
{
    public SymMatrix(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public float XX
    {
        get => ReadSingle(4);
        set => WriteSingle(4, value);
    }

    public float XY
    {
        get => ReadSingle(8);
        set => WriteSingle(8, value);
    }

    public float XZ
    {
        get => ReadSingle(12);
        set => WriteSingle(12, value);
    }

    public float YY
    {
        get => ReadSingle(16);
        set => WriteSingle(16, value);
    }

    public float YZ
    {
        get => ReadSingle(20);
        set => WriteSingle(20, value);
    }

    public float ZZ
    {
        get => ReadSingle(24);
        set => WriteSingle(24, value);
    }

    /// <summary>
    /// Sets all values of the Matrix to <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The value to set.
    /// </param>
    public void Set(float value)
    {
        byte[] bytes = new byte[sizeof(float) * 6];
        byte[] valueBytes = BitConverter.GetBytes(value);
        for (int i = 0; i < 6; i++)
            valueBytes.CopyTo(bytes, i * sizeof(float));
        WriteBytes(4, bytes);
    }

    public void Scale(float scale)
    {
        byte[] bytes = ReadBytes(4, sizeof(float) * 6);
        float[] values = new float[6];

        for (int i = 0; i < 6; i++)
            values[i] = BitConverter.ToSingle(bytes, i * sizeof(float));

        for (int i = 0; i < 6; i++)
            values[i] *= scale;

        for (int i = 0; i < 6; i++)
            BitConverter.GetBytes(values[i]).CopyTo(bytes, i * sizeof(float));

        WriteBytes(4, bytes);
    }

    public void Identity()
    {
        byte[] bytes = new byte[sizeof(float) * 6];
        byte[] zeroBytes = BitConverter.GetBytes(0f);
        byte[] oneBytes = BitConverter.GetBytes(1f);

        oneBytes.CopyTo(bytes, 0);
        zeroBytes.CopyTo(bytes, sizeof(float));
        zeroBytes.CopyTo(bytes, sizeof(float) * 2);
        oneBytes.CopyTo(bytes, sizeof(float) * 3);
        zeroBytes.CopyTo(bytes, sizeof(float) * 4);
        oneBytes.CopyTo(bytes, sizeof(float) * 5);

        WriteBytes(4, bytes);
    }

    public void Zero()
    {
        byte[] bytes = new byte[sizeof(float) * 6];
        byte[] valueBytes = BitConverter.GetBytes(0f);
        for (int i = 0; i < 6; i++)
            valueBytes.CopyTo(bytes, i * sizeof(float));
        WriteBytes(4, bytes);
    }
}
