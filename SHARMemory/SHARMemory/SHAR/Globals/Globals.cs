using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR
{
    public sealed partial class Globals
    {
        private readonly Memory Memory;

        public GameplayManager GameplayManager => Memory.CreateClass<GameplayManager>(Memory.SelectAddress(0x6C8998, 0x6C8958, 0x6C8958, 0x6C8990));
        public FeTextBible TextBible => Memory.CreateClass<FeTextBible>(Memory.SelectAddress(0x6C8944, 0x6C8904, 0x6C8904, 0x6C893C));

        /// <summary>
        /// A <c>byte</c> containing how many levels in the game. Usually 7, but can differ when using <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" />.
        /// </summary>
        public byte LevelCount => Memory.ReadByte(Memory.SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3);

        public CharacterTuneHandler CharacterTune { get; }
        public CheatsHandler Cheats { get; }
        public FeTextBibleHandler FeTextBible { get; }
        public TrafficManagerHandler TrafficManager { get; }

        internal Globals(Memory memory)
        {
            Memory = memory;

            CharacterTune = new(Memory);
            Cheats = new(Memory);
            FeTextBible = new(Memory);
            TrafficManager = new(Memory);
        }
    }
}