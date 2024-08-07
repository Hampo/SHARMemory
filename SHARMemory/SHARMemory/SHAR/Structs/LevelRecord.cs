﻿using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(LevelRecordStruct))]
public struct LevelRecord
{
    public const int Size = CharCardList.Size + 1 + MissionList.Size + StreetRaceList.Size + MissionRecord.Size + MissionRecord.Size + 4 + sizeof(int) + sizeof(int) + sizeof(int) + 16 + sizeof(int) + sizeof(uint) + CharacterSheet.MAX_LEVEL_GAGS + CharacterSheet.MAX_PURCHASED_ITEMS;

    public CharCardList Cards;

    public MissionList Missions;
    public StreetRaceList StreetRaces;
    public MissionRecord BonusMission;
    public MissionRecord GambleRace;

    public bool FMVUnlocked;
    public int NumCarsPurchased;
    public int NumSkinsPurchased;
    public int WaspsDestroyed;
    public string CurrentSkin;

    public int GagsViewed;
    public uint GagMask;

    public bool[] Gags;

    public bool[] PurchasedRewards;

    public LevelRecord(CharCardList cards, MissionList missions, StreetRaceList streetRaces, MissionRecord bonusMission, MissionRecord gambleRace, bool fmvUnlocked, int numCarsPurchased, int numSkinsPurchased, int waspsDestroyed, string currentSkin, int gagsViewed, uint gagMask, bool[] gags, bool[] purchasedRewards)
    {
        Cards = cards;

        Missions = missions;
        StreetRaces = streetRaces;
        BonusMission = bonusMission;
        GambleRace = gambleRace;

        FMVUnlocked = fmvUnlocked;
        NumCarsPurchased = numCarsPurchased;
        NumSkinsPurchased = numSkinsPurchased;
        WaspsDestroyed = waspsDestroyed;
        CurrentSkin = currentSkin;

        GagsViewed = gagsViewed;
        GagMask = gagMask;

        Gags = gags;

        PurchasedRewards = purchasedRewards;
    }

    public override readonly string ToString() => $"{Cards} | {Missions} | {StreetRaces} | {BonusMission} | {GambleRace} | {FMVUnlocked} | {NumCarsPurchased} | {NumSkinsPurchased} | {WaspsDestroyed} | {CurrentSkin} | {GagsViewed} | {GagMask} | {Gags} | {PurchasedRewards}";
}

internal class LevelRecordStruct : Struct
{
    public override int Size => LevelRecord.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        CharCardList Cards = Memory.StructFromBytes<CharCardList>(Bytes, Offset);
        Offset += CharCardList.Size;
        Offset++; // Padding

        MissionList Missions = Memory.StructFromBytes<MissionList>(Bytes, Offset);
        Offset += MissionList.Size;
        StreetRaceList StreetRaces = Memory.StructFromBytes<StreetRaceList>(Bytes, Offset);
        Offset += StreetRaceList.Size;
        MissionRecord BonusMission = Memory.StructFromBytes<MissionRecord>(Bytes, Offset);
        Offset += MissionRecord.Size;
        MissionRecord GambleRace = Memory.StructFromBytes<MissionRecord>(Bytes, Offset);
        Offset += MissionRecord.Size;

        bool FMVUnlocked = BitConverter.ToBoolean(Bytes, Offset);
        Offset += 4;
        int NumCarsPurchased = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int NumSkinsPurchased = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int WaspsDestroyed = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        string CurrentSkin = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
        Offset += 16;

        int GagsViewed = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        uint GagMask = BitConverter.ToUInt32(Bytes, Offset);
        Offset += sizeof(uint);

        bool[] Gags = new bool[CharacterSheet.MAX_LEVEL_GAGS];
        for (int i = 0; i < CharacterSheet.MAX_LEVEL_GAGS; i++)
        {
            Gags[i] = BitConverter.ToBoolean(Bytes, Offset);
            Offset += 1;
        }

        bool[] PurchasedRewards = new bool[CharacterSheet.MAX_PURCHASED_ITEMS];
        for (int i = 0; i < CharacterSheet.MAX_PURCHASED_ITEMS; i++)
        {
            PurchasedRewards[i] = BitConverter.ToBoolean(Bytes, Offset);
            Offset += 1;
        }
        return new LevelRecord(Cards, Missions, StreetRaces, BonusMission, GambleRace, FMVUnlocked, NumCarsPurchased, NumSkinsPurchased, WaspsDestroyed, CurrentSkin, GagsViewed, GagMask, Gags, PurchasedRewards);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not LevelRecord Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelRecord)}'.", nameof(Value));

        Memory.BytesFromStruct(Value2.Cards, Buffer, Offset);
        Offset += CharCardList.Size;
        BitConverter.GetBytes((byte)0).CopyTo(Buffer, Offset); // padding
        Offset += sizeof(byte);

        Memory.BytesFromStruct(Value2.Missions, Buffer, Offset);
        Offset += MissionList.Size;
        Memory.BytesFromStruct(Value2.StreetRaces, Buffer, Offset);
        Offset += StreetRaceList.Size;
        Memory.BytesFromStruct(Value2.BonusMission, Buffer, Offset);
        Offset += MissionRecord.Size;
        Memory.BytesFromStruct(Value2.GambleRace, Buffer, Offset);
        Offset += MissionRecord.Size;

        BitConverter.GetBytes(Value2.FMVUnlocked).CopyTo(Buffer, Offset);
        Offset += 4;
        BitConverter.GetBytes(Value2.NumCarsPurchased).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.NumSkinsPurchased).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.WaspsDestroyed).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        Memory.GetStringBytes(Value2.CurrentSkin, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
        Offset += 16;

        BitConverter.GetBytes(Value2.GagsViewed).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.GagMask).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);

        for (int i = 0; i < CharacterSheet.MAX_LEVEL_GAGS; i++)
        {
            BitConverter.GetBytes(Value2.Gags[i]).CopyTo(Buffer, Offset);
            Offset += 1;
        }

        for (int i = 0; i < CharacterSheet.MAX_PURCHASED_ITEMS; i++)
        {
            BitConverter.GetBytes(Value2.PurchasedRewards[i]).CopyTo(Buffer, Offset);
            Offset += 1;
        }
    }
}
