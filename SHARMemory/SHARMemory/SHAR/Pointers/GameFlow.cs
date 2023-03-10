namespace SHARMemory.SHAR.Pointers
{
    public class GameFlow : Pointer
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

        public GameFlow(Memory memory) : base(memory, memory.SelectAddress(0x6C9014, 0x6C8FD4, 0x6C8FD4, 0x6C900C)) { }

        public GameState State => IsPointerValid ? (GameState)ReadUInt32(12) : GameState.PreLicence;
    }
}
