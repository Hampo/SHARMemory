using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(LevelRewardRecordStruct))]
public struct LevelRewardRecord
{
    public const int Size = sizeof(uint) + sizeof(uint) + sizeof(uint) + sizeof(uint) + sizeof(uint) + sizeof(uint) + sizeof(int) + sizeof(int) + sizeof(int);

    public Reward StreetRace;

    public Reward BonusMission;

    public Reward Cards;

    public Reward DefaultCar;

    public Reward DefaultSkin;

    public Reward GoldCards;

    public int MaxTokensInLevel;

    public int TotalGagsInLevel;

    public int TotalWaspsInLevel;

    public LevelRewardRecord(Reward streetRace, Reward bonusMission, Reward cards, Reward defaultCar, Reward defaultSkin, Reward goldCards, int maxTokensInLevel, int totalGagsInLevel, int totalWaspsInLevel)
    {
        StreetRace = streetRace;
        BonusMission = bonusMission;
        Cards = cards;
        DefaultCar = defaultCar;
        DefaultSkin = defaultSkin;
        GoldCards = goldCards;
        MaxTokensInLevel = maxTokensInLevel;
        TotalGagsInLevel = totalGagsInLevel;
        TotalWaspsInLevel = totalWaspsInLevel;
    }

    public override readonly string ToString() => $"{StreetRace} | {BonusMission} | {Cards} | {DefaultCar} | {DefaultSkin} | {GoldCards} | {MaxTokensInLevel} | {TotalGagsInLevel} | {TotalWaspsInLevel}";
}

internal class LevelRewardRecordStruct : Struct
{
    public override int Size => LevelRewardRecord.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Reward StreetRace = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Reward BonusMission = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Reward Cards = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Reward DefaultCar = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Reward DefaultSkin = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        Reward GoldCards = Memory.ClassFactory.Create<Reward>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        int MaxTokensInLevel = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int TotalGagsInLevel = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int TotalWaspsInLevel = BitConverter.ToInt32(Bytes, Offset);
        return new LevelRewardRecord(StreetRace, BonusMission, Cards, DefaultCar, DefaultSkin, GoldCards, MaxTokensInLevel, TotalGagsInLevel, TotalWaspsInLevel);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not LevelRewardRecord Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelRewardRecord)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.StreetRace?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.BonusMission?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.Cards?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.DefaultCar?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.DefaultSkin?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.GoldCards?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.MaxTokensInLevel).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.TotalGagsInLevel).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.TotalWaspsInLevel).CopyTo(Buffer, Offset);
    }
}
