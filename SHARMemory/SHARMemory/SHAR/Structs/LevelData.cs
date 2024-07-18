using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(LevelDataStruct))]
public struct LevelData
{
    public const int Size = sizeof(Globals.RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int) + sizeof(int) + sizeof(Globals.RenderEnums.MissionEnum);

    public Globals.RenderEnums.LevelEnum Level;

    public int NumMissions;

    public GameplayManager.Ratings[] Ratings;

    public int NumBonusCollectibles;

    public int NumBonusCollected;

    public Globals.RenderEnums.MissionEnum Mission;

    public LevelData(Globals.RenderEnums.LevelEnum level, int numMissions, GameplayManager.Ratings[] ratings, int numBonusCollectibles, int numBonusCollected, Globals.RenderEnums.MissionEnum mission)
    {
        Level = level;
        NumMissions = numMissions;
        Ratings = ratings;
        NumBonusCollectibles = numBonusCollectibles;
        NumBonusCollected = numBonusCollected;
        Mission = mission;
    }

    public override readonly string ToString() => $"{Level} | {NumMissions} | {Ratings} | {NumBonusCollectibles} | {NumBonusCollected} | {Mission}";
}

internal class LevelDataStruct : Struct
{
    public override int Size => LevelData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Globals.RenderEnums.LevelEnum Level = (Globals.RenderEnums.LevelEnum)BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(Globals.RenderEnums.LevelEnum);
        int NumMissions = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        GameplayManager.Ratings[] Ratings = new GameplayManager.Ratings[GameplayManager.MAX_MISSIONS];
        for (int i = 0; i < GameplayManager.MAX_MISSIONS; i++)
        {
            Ratings[i] = (GameplayManager.Ratings)BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(GameplayManager.Ratings);
        }
        int NumBonusCollectibles = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int NumBonusCollected = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        Globals.RenderEnums.MissionEnum Mission = (Globals.RenderEnums.MissionEnum)BitConverter.ToInt32(Bytes, Offset);
        return new LevelData(Level, NumMissions, Ratings, NumBonusCollectibles, NumBonusCollected, Mission);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not LevelData Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelData)}'.", nameof(Value));

        BitConverter.GetBytes((int)Value2.Level).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.NumMissions).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        for (int i = 0; i < GameplayManager.MAX_MISSIONS; i++)
        {
            BitConverter.GetBytes((int)Value2.Ratings[i]).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
        }
        BitConverter.GetBytes(Value2.NumBonusCollectibles).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.NumBonusCollected).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes((int)Value2.Mission).CopyTo(Buffer, Offset);
    }
}