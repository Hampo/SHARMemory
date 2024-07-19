using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs.tMultiController;

[Struct(typeof(TrackInfoStruct))]
public struct TrackInfo
{
    public const int Size = sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float);

    public float StartTime;

    public float EndTime;

    public float Offset;

    public float Scale;

    public TrackInfo(float startTime, float endTime, float offset, float scale)
    {
        StartTime = startTime;
        EndTime = endTime;
        Offset = offset;
        Scale = scale;
    }

    public override readonly string ToString() => $"{StartTime} | {EndTime} | {Offset} | {Scale}";
}

internal class TrackInfoStruct : Struct
{
    public override int Size => TrackInfo.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        float StartTime = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float EndTime = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float OffsetVal = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float Scale = BitConverter.ToSingle(Bytes, Offset);
        return new TrackInfo(StartTime, EndTime, OffsetVal, Scale);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not TrackInfo Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(TrackInfo)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.StartTime).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.EndTime).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Offset).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.Scale).CopyTo(Buffer, Offset);
    }
}
