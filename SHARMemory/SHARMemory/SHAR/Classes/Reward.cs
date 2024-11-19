using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Text;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVReward@@")]
public class Reward : Class
{
    public enum RewardTypes
    {
        Null,
        SkinOther,
        SkinGood,
        PlayerCar,
        Toy
    }

    public enum QuestTypes
    {
        Blank,
        DefaultCar,
        DefaultSkin,
        Cards,
        StreetRace,
        BonusMission,
        GoldCards
    }

    public Reward(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint VFTableOffset = 0;

    internal const uint UIDOffset = VFTableOffset + sizeof(uint);
    public long UID
    {
        get => ReadInt64(UIDOffset);
        set => WriteInt64(UIDOffset, value);
    }

    internal const uint LevelOffset = UIDOffset + sizeof(long);
    public int Level
    {
        get => ReadInt32(LevelOffset);
        set => WriteInt32(LevelOffset, value);
    }

    internal const uint NameOffset = LevelOffset + sizeof(int);
    public string Name
    {
        get => ReadString(NameOffset, Encoding.UTF8, 16);
        set => WriteString(NameOffset, value, Encoding.UTF8, 16);
    }

    internal const uint FileOffset = NameOffset + 16;
    public string File
    {
        get => ReadString(FileOffset, Encoding.UTF8, 64);
        set => WriteString(FileOffset, value, Encoding.UTF8, 64);
    }

    internal const uint EarnedOffset = FileOffset + 64;
    public bool Earned
    {
        get => ReadBoolean(EarnedOffset);
        set => WriteBoolean(EarnedOffset, value);
    }

    internal const uint QuestTypeOffset = EarnedOffset + 4; // Padding
    public QuestTypes QuestType
    {
        get => (QuestTypes)ReadUInt32(QuestTypeOffset);
        set => WriteUInt32(QuestTypeOffset, (uint)value);
    }

    internal const uint RewardTypeOffset = QuestTypeOffset + sizeof(uint);
    public RewardTypes RewardType
    {
        get => (RewardTypes)ReadUInt32(RewardTypeOffset);
        set => WriteUInt32(RewardTypeOffset, (uint)value);
    }

    internal const uint RepairCostOffset = RewardTypeOffset + sizeof(uint);
    public int RepairCost
    {
        get => ReadInt32(RepairCostOffset);
        set => WriteInt32(RepairCostOffset, value);
    }
}
