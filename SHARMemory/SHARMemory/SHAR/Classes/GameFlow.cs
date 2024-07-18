using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVGameFlow@@")]
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

    public GameFlow(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public GameState State => (GameState)ReadUInt32(12);
}
