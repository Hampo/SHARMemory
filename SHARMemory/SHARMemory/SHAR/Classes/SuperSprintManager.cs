using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSuperSprintManager@@")]
public class SuperSprintManager : Class
{
    public enum State
    {
        Idle,
        Countdown,
        Racing,
        DNFTimeout,
        WinnerCircle,
        Paused
    }

    public SuperSprintManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint CurrentStateOffset = 6792;
    public State CurrentState
    {
        get => (State)ReadUInt32(CurrentStateOffset);
        set => WriteUInt32(CurrentStateOffset, (uint)value);
    }

    internal const uint VehicleSlotsOffset = CurrentStateOffset + sizeof(uint);

    internal const uint PlayersOffset = VehicleSlotsOffset + 144; // SuperSprintData.CarData.Size * 4;

    internal const uint DrawableOffset = PlayersOffset + 288; // SuperSprintData.PlayerData.Size * 4;

    internal const uint CountDownTimeOffset = DrawableOffset + sizeof(uint);
    public int CountDownTime
    {
        get => ReadInt32(CountDownTimeOffset);
        set => WriteInt32(CountDownTimeOffset, value);
    }

    internal const uint TimeOffset = CountDownTimeOffset + sizeof(int);
    public string Time
    {
        get => ReadString(TimeOffset, System.Text.Encoding.ASCII, 32);
        set => WriteString(TimeOffset, value, System.Text.Encoding.ASCII, 32);
    }

    internal const uint CurrentPositionOffset = TimeOffset + 32;
    public int CurrentPosition
    {
        get => ReadInt32(CurrentPositionOffset);
        set => WriteInt32(CurrentPositionOffset, value);
    }

    internal const uint StartTimeOffset = CurrentPositionOffset + sizeof(int);
    public uint StartTime
    {
        get => ReadUInt32(StartTimeOffset);
        set => WriteUInt32(StartTimeOffset, value);
    }

    internal const uint NumActivePlayersOffset = StartTimeOffset + sizeof(uint);
    public int NumActivePlayers
    {
        get => ReadInt32(NumActivePlayersOffset);
        set => WriteInt32(NumActivePlayersOffset, value);
    }

    internal const uint NumHumanPlayersOffset = NumActivePlayersOffset + sizeof(int);
    public int NumHumanPlayers
    {
        get => ReadInt32(NumHumanPlayersOffset);
        set => WriteInt32(NumHumanPlayersOffset, value);
    }

    internal const uint NumHumansFinishedOffset = NumHumanPlayersOffset + sizeof(int);
    public int NumHumansFinished
    {
        get => ReadInt32(NumHumansFinishedOffset);
        set => WriteInt32(NumHumansFinishedOffset, value);
    }
}
