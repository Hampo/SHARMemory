namespace SHARMemory.SHAR.Classes
{
    public class GameFlow : Class
    {
        public enum GameState
        {
            PreLicence,
            Licence,
            MainMenu,
            DemoLoading,
            DemoInGame,
            BonusSetup,
            BonusLoading,
            BonusInGame,
            NormalLoading,
            NormalInGame,
            NormalPaused,
            Exit
        }

        public GameFlow(Memory memory, uint address) : base(memory, address) { }

        public GameState State => (GameState)ReadUInt32(12);
    }
}
