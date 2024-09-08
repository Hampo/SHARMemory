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

    internal const uint IRadTimerCallbackVFTableOffset = 0;

    internal const uint TimerOffset = IRadTimerCallbackVFTableOffset + sizeof(uint);
    public SHARMemory.Memory.Class Timer => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(TimerOffset));

    internal const uint CurrentContextOffset = TimerOffset + sizeof(uint);
    public GameState CurrentContext
    {
        get => (GameState)ReadUInt32(CurrentContextOffset);
        set => WriteUInt32(CurrentContextOffset, (uint)value);
    }

    internal const uint NextContextOffset = CurrentContextOffset + sizeof(uint);
    public GameState NextContext
    {
        get => (GameState)ReadUInt32(NextContextOffset);
        set => WriteUInt32(NextContextOffset, (uint)value);
    }
}
