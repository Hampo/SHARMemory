using System.Text;

namespace SHARMemory.SHAR.Classes
{
    public class CharacterSheet : Class
    {
        public CharacterSheet(Memory memory, uint address) : base(memory, address) { }

        public string PlayerName => ReadString(0, Encoding.ASCII, 16u);

        public string CardName(uint Level, uint Card) => ReadString(16 + 620 * Level + 17 * Card, Encoding.ASCII, 16u);

        public bool CardCollected(uint Level, uint Card) => ReadBoolean(16 + 620 * Level + 17 * Card + 16);

        public string MissionName(uint Level, uint Mission) => ReadString(16 + 620 * Level + 120 + 32 * Mission, Encoding.ASCII, 16u);

        public bool MissionCompleted(uint Level, uint Mission) => ReadBoolean(16 + 620 * Level + 120 + 32 * (Level == 0 ? Mission + 1 : Mission) + 16);

        public string StreetRaceName(uint Level, uint StreetRace) => ReadString(16 + 620 * Level + 120 + 256 + 32 * StreetRace, Encoding.ASCII, 16u);

        public bool StreetRaceCompleted(uint Level, uint StreetRace) => ReadBoolean(16 + 620 * Level + 120 + 256 + 32 * StreetRace + 16);

        public string BonusMissionName(uint Level) => ReadString(16 + 620 * Level + 120 + 256 + 96, Encoding.ASCII, 16u);

        public bool BonusMissionCompleted(uint Level) => ReadBoolean(16 + 620 * Level + 120 + 256 + 96 + 16);

        public string GambleRaceName(uint Level) => ReadString(16 + 620 * Level + 120 + 256 + 96 + 32, Encoding.ASCII, 16u);

        public bool GambleRaceCompleted(uint Level) => ReadBoolean(16 + 620 * Level + 120 + 256 + 96 + 32 + 16);

        public bool FMVUnlocked(uint Level) => ReadBoolean(16 + 620 * Level + 120 + 256 + 96 + 32 + 32);

        public uint VehiclesCount(uint Level) => ReadUInt32(16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4);

        public uint CharacterClothingCount(uint Level) => ReadUInt32(16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4);

        public uint WaspCamerasCount(uint Level) => ReadUInt32(16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4);

        public string CurrentSkinName(uint Level) => ReadString(16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4, Encoding.ASCII, 16u);

        public uint GagsCount(uint Level) => ReadUInt32(16 + 620 * Level + 120 + 256 + 96 + 32 + 32 + 4 + 4 + 4 + 4 + 16);

        public RenderEnums.LevelEnum CurrentLevel => (RenderEnums.LevelEnum)ReadInt32((uint)(16 + 620 * Memory.LevelCount));

        public int CurrentMission => ReadInt32((uint)(16 + 620 * Memory.LevelCount + 4));

        public RenderEnums.LevelEnum HighestLevel => (RenderEnums.LevelEnum)ReadInt32((uint)(16 + 620 * Memory.LevelCount + 8));

        public int HighestMission => ReadInt32((uint)(16 + 620 * Memory.LevelCount + 12));

        public bool IsNavSystemEnabled => ReadBoolean((uint)(16 + 620 * Memory.LevelCount + 16));

        public int Coins
        {
            get => ReadInt32((uint)(16 + 620 * Memory.LevelCount + 20));
            set => WriteInt32((uint)(16 + 620 * Memory.LevelCount + 20), value);
        }
    }
}
