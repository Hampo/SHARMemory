using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(CurrentMissionStruct))]
public struct CurrentMission
{
    public const int Size = sizeof(Globals.RenderEnums.LevelEnum) + sizeof(Globals.RenderEnums.MissionEnum);

    public Globals.RenderEnums.LevelEnum Level;

    public Globals.RenderEnums.MissionEnum MissionNumber;

    public CurrentMission(Globals.RenderEnums.LevelEnum level, Globals.RenderEnums.MissionEnum missionNumber)
    {
        Level = level;
        MissionNumber = missionNumber;
    }

    public override readonly string ToString() => $"{Level} | {MissionNumber}";
}

internal class CurrentMissionStruct : Struct
{
    public override int Size => LevelData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Globals.RenderEnums.LevelEnum Level = (Globals.RenderEnums.LevelEnum)BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(Globals.RenderEnums.LevelEnum);
        Globals.RenderEnums.MissionEnum MissionNumber = (Globals.RenderEnums.MissionEnum)BitConverter.ToInt32(Bytes, Offset);
        return new CurrentMission(Level, MissionNumber);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CurrentMission Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CurrentMission)}'.", nameof(Value));

        BitConverter.GetBytes((int)Value2.Level).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes((int)Value2.MissionNumber).CopyTo(Buffer, Offset);
    }
}