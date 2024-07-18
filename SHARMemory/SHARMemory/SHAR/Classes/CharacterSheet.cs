using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System.Text;

namespace SHARMemory.SHAR.Classes;

public class CharacterSheet : Class
{
    public const int MAX_LEVELS = 7;
    public const int MAX_MISSIONS = 8;
    public const int MAX_CARDS = 7;
    public const int MAX_STREETRACES = 3;
    public const int MAX_PURCHASED_ITEMS = 12;
    public const int MAX_LEVEL_GAGS = 32;
    public const int MAX_CARS_OWNED = 60;
    public const int NUM_BYTES_FOR_PERSISTENT_STATES = 1312;

    public CharacterSheet(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    private const uint PlayerNameOffset = 0;
    public string PlayerName => ReadString(PlayerNameOffset, Encoding.ASCII, 16u);

    private const uint LevelListOffset = PlayerNameOffset + 16;
    public StructArray<LevelRecord> LevelList => new(Memory, Address + LevelListOffset, LevelRecord.Size, MAX_LEVELS);

    private const uint CurrentMissionInfoOffset = LevelListOffset + LevelRecord.Size * MAX_LEVELS;
    public CurrentMission CurrentMissionInfo
    {
        get => ReadStruct<CurrentMission>(CurrentMissionInfoOffset);
        set => WriteStruct(CurrentMissionInfoOffset, value);
    }

    private const uint HighestMissionPlayedOffset = CurrentMissionInfoOffset + CurrentMission.Size;
    public CurrentMission HighestMissionPlayed
    {
        get => ReadStruct<CurrentMission>(HighestMissionPlayedOffset);
        set => WriteStruct(HighestMissionPlayedOffset, value);
    }

    private const uint IsNavSystemEnabledOffset = HighestMissionPlayedOffset + CurrentMission.Size;
    public bool IsNavSystemEnabled
    {
        get => ReadBoolean(IsNavSystemEnabledOffset);
        set => WriteBoolean(IsNavSystemEnabledOffset, value);
    }

    private const uint CoinsOffset = IsNavSystemEnabledOffset + 4;
    public int Coins
    {
        get => ReadInt32(CoinsOffset);
        set => WriteInt32(CoinsOffset, value);
    }

    private const uint CarInventoryOffset = CoinsOffset + sizeof(int);
    public CarInventory CarInventory
    {
        get => ReadStruct<CarInventory>(CarInventoryOffset);
        set => WriteStruct(CarInventoryOffset, value);
    }

    private const uint PersistentObjectStatesOffset = CarInventoryOffset + CarInventory.Size;
    public StructArray<byte> PersistentObjectStates => new(Memory, Address + PersistentObjectStatesOffset, sizeof(byte), NUM_BYTES_FOR_PERSISTENT_STATES);

    private const uint StateOffset = PersistentObjectStatesOffset + NUM_BYTES_FOR_PERSISTENT_STATES;
    public byte State
    {
        get => ReadByte(StateOffset);
        set => WriteByte(StateOffset, value);
    }
}
