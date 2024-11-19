using SHARMemory.Memory;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(CarAttributeRecordStruct))]
public struct CarAttributeRecord
{
    public const int Size = 16 + sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float);

    public string Name;

    public float Speed;

    public float Acceleration;

    public float Toughness;

    public float Stability;

    public CarAttributeRecord(string name, float speed, float acceleration, float toughness, float stability)
    {
        Name = name;
        Speed = speed;
        Acceleration = acceleration;
        Toughness = toughness;
        Stability = stability;
    }

    public override readonly string ToString() => $"{Name} | {Speed} | {Acceleration} | {Toughness} | {Stability}";
}

internal class CarAttributeRecordStruct : Struct
{
    public override int Size => CarAttributeRecord.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
        Offset += 16;
        float Speed = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Acceleration = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Toughness = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Stability = BitConverter.ToSingle(Bytes, Offset);
        return new CarAttributeRecord(Name, Speed, Acceleration, Toughness, Stability);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CarAttributeRecord Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CarAttributeRecord)}'.", nameof(Value));

        Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
        Offset += 16;
        BitConverter.GetBytes(Value2.Speed).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Acceleration).CopyTo(Buffer, Offset);
        Offset += 16;
        BitConverter.GetBytes(Value2.Toughness).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Stability).CopyTo(Buffer, Offset);
    }
}
