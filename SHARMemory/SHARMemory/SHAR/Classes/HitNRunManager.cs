using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVHitnRunManager@@")]
public class HitNRunManager : Class
{
    public HitNRunManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public float CurrHitAndRun
    {
        get => ReadSingle(4);
        set => WriteSingle(4, value);
    }

    public float DecayRatePerSecond
    {
        get => ReadSingle(8);
        set => WriteSingle(8, value);
    }

    public float DecayRateWhileSpawning
    {
        get => ReadSingle(12);
        set => WriteSingle(12, value);
    }

    public float DecayRateInsidePerSecond
    {
        get => ReadSingle(16);
        set => WriteSingle(16, value);
    }

    public float VehicleDestroyedDelta
    {
        get => ReadSingle(20);
        set => WriteSingle(20, value);
    }

    public int VehicleDestroyedCoins
    {
        get => ReadInt32(24);
        set => WriteInt32(24, value);
    }

    public float VehicleHitDelta
    {
        get => ReadSingle(28);
        set => WriteSingle(28, value);
    }

    public float HitBreakableDelta
    {
        get => ReadSingle(32);
        set => WriteSingle(32, value);
    }

    public int HitBreakableCoins
    {
        get => ReadInt32(36);
        set => WriteInt32(36, value);
    }

    public float HitMoveableDelta
    {
        get => ReadSingle(40);
        set => WriteSingle(40, value);
    }

    public int HitMoveableCoins
    {
        get => ReadInt32(44);
        set => WriteInt32(44, value);
    }

    public float HitKrustyGlassDelta
    {
        get => ReadSingle(48);
        set => WriteSingle(48, value);
    }

    public int HitKrustyGlassCoins
    {
        get => ReadInt32(52);
        set => WriteInt32(52, value);
    }

    public float ColaPropDestroyedDelta
    {
        get => ReadSingle(56);
        set => WriteSingle(56, value);
    }

    public int ColaPropDestroyedCoins
    {
        get => ReadInt32(60);
        set => WriteInt32(60, value);
    }

    public float KickNPCDelta
    {
        get => ReadSingle(64);
        set => WriteSingle(64, value);
    }

    public int KickNPCCoins
    {
        get => ReadInt32(68);
        set => WriteInt32(68, value);
    }

    public float PlayerCarHitNPCDelta
    {
        get => ReadSingle(72);
        set => WriteSingle(72, value);
    }

    public int PlayerCarHitNPCCoins
    {
        get => ReadInt32(76);
        set => WriteInt32(76, value);
    }

    public int BustedCoins
    {
        get => ReadInt32(80);
        set => WriteInt32(80, value);
    }

    public float SwitchSkinDelta
    {
        get => ReadSingle(84);
        set => WriteSingle(84, value);
    }

    public float ChangeVehicleDelta
    {
        get => ReadSingle(88);
        set => WriteSingle(88, value);
    }

    public uint PrevVehicleID
    {
        get => ReadUInt32(92);
        set => WriteUInt32(92, value);
    }

    public PointerArray<SHARMemory.Memory.Class> PrevHits => new(Memory, Address + 96, 16);

    public int LeastRecentlyHit
    {
        get => ReadInt32(160);
        set => WriteInt32(160, value);
    }

    public bool ChaseOn
    {
        get => ReadBoolean(164);
        set => WriteBoolean(164, value);
    }

    public bool SpawnOn
    {
        get => ReadBoolean(165);
        set => WriteBoolean(165, value);
    }

    public float DecayDelayMS
    {
        get => ReadSingle(168);
        set => WriteSingle(168, value);
    }

    public float DecayDelay
    {
        get => ReadSingle(172);
        set => WriteSingle(172, value);
    }

    public float DecayDelayWhileSpawning
    {
        get => ReadSingle(176);
        set => WriteSingle(176, value);
    }

    public int NumChaseCars
    {
        get => ReadInt32(180);
        set => WriteInt32(180, value);
    }

    public bool HitnRunDisabled
    {
        get => ReadBoolean(184);
        set => WriteBoolean(184, value);
    }

    public bool DecayDisabled
    {
        get => ReadBoolean(185);
        set => WriteBoolean(185, value);
    }

    public float CopTicketDistance
    {
        get => ReadSingle(188);
        set => WriteSingle(188, value);
    }

    public float CopTicketDistanceOnFoot
    {
        get => ReadSingle(192);
        set => WriteSingle(192, value);
    }

    public float CopTicketSpeedThreshold
    {
        get => ReadSingle(196);
        set => WriteSingle(196, value);
    }

    public float CopTicketTimeThreshold
    {
        get => ReadSingle(200);
        set => WriteSingle(200, value);
    }

    public float TicketTimer
    {
        get => ReadSingle(204);
        set => WriteSingle(204, value);
    }

    public float LastUpdateValue
    {
        get => ReadSingle(208);
        set => WriteSingle(208, value);
    }

    public float LastHitNRunValue
    {
        get => ReadSingle(212);
        set => WriteSingle(212, value);
    }

    public bool VehicleReset
    {
        get => ReadBoolean(216);
        set => WriteBoolean(216, value);
    }

    public float VehicleResetTimer
    {
        get => ReadSingle(220);
        set => WriteSingle(220, value);
    }

    public float UnresponsiveTime
    {
        get => ReadSingle(224);
        set => WriteSingle(224, value);
    }

    public bool FadeDone
    {
        get => ReadBoolean(228);
        set => WriteBoolean(228, value);
    }

    public float FadeTimer
    {
        get => ReadSingle(232);
        set => WriteSingle(232, value);
    }
}
