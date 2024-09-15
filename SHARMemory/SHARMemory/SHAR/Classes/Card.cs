using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCard@@")]
public class Card : Class
{
    public const int MAX_NUM_QUOTES = 3;

    [Struct(typeof(SHARMemory.Memory.Structs.Int32Struct))]
    public enum QuoteID : int
    {
        Empty = -1,

        Announcer,
        Apu,
        Bart,
        Brockman,
        Burns,
        Carl,
        Child,
        Dr_Wolff,
        Gil,
        Homer,
        Jasper,
        Jimbo,
        Kang,
        Krusty,
        Lenny,
        Lisa,
        Maggie,
        Manjula,
        Marge,
        Meyers,
        Milhouse,
        Mother,
        Mr_Sparkle,
        Otto,
        Photographer,
        Ralph,
        Skinner,
        Smithers,
        Stacy,
        Wiggum,
        Willie,
    };

    public Card(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint CardVFTableOffset = 0;

    internal const uint IDOffset = CardVFTableOffset + sizeof(uint);
    public uint ID
    {
        get => ReadUInt32(IDOffset);
        set => WriteUInt32(IDOffset, value);
    }

    internal const uint LevelOffset = IDOffset + sizeof(uint);
    public uint Level
    {
        get => ReadUInt32(LevelOffset);
        set => WriteUInt32(LevelOffset, value);
    }

    internal const uint LevelIDOffset = LevelOffset + sizeof(uint);
    public uint LevelID
    {
        get => ReadUInt32(LevelIDOffset);
        set => WriteUInt32(LevelIDOffset, value);
    }

    internal const uint CardNameOffset = LevelIDOffset + sizeof(uint);
    public ulong CardName
    {
        get => ReadUInt64(CardNameOffset);
        set => WriteUInt64(CardNameOffset, value);
    }

    internal const uint QuotesOffset = CardNameOffset + sizeof(ulong);
    public StructArray<QuoteID> Quotes => new(Memory, Address + QuotesOffset, sizeof(int), MAX_NUM_QUOTES);

    internal const uint NumQuotesOffset = QuotesOffset + sizeof(int) * MAX_NUM_QUOTES;
    public int NumQuotes
    {
        get => ReadInt32(NumQuotesOffset);
        set => WriteInt32(NumQuotesOffset, value);
    }
}
