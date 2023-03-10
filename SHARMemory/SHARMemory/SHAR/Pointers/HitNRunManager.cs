namespace SHARMemory.SHAR.Pointers
{
    public class HitNRunManager : Pointer
    {
        public HitNRunManager(Memory memory) : base(memory, memory.SelectAddress(0x6C84E0, 0x6C84A0, 0x6C84A0, 0x6C84D8)) { }

        public float HitAndRun
        {
            get => ReadSingle(4);
            set => WriteSingle(4, value);
        }

        public float DecayPerSecond
        {
            get => ReadSingle(8);
            set => WriteSingle(8, value);
        }

        public float DecayWhileSpawning
        {
            get => ReadSingle(12);
            set => WriteSingle(12, value);
        }

        public float DecayInsidePerSecond
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

        public float BreakableDelta
        {
            get => ReadSingle(32);
            set => WriteSingle(32, value);
        }

        public int BreakableCoins
        {
            get => ReadInt32(36);
            set => WriteInt32(36, value);
        }

        public float MoveableDelta
        {
            get => ReadSingle(40);
            set => WriteSingle(40, value);
        }

        public int MoveableCoins
        {
            get => ReadInt32(44);
            set => WriteInt32(44, value);
        }

        public float KrustyGlassDelta
        {
            get => ReadSingle(48);
            set => WriteSingle(48, value);
        }

        public int KrustyGlassCoins
        {
            get => ReadInt32(52);
            set => WriteInt32(52, value);
        }

        public float ColaPropDelta
        {
            get => ReadSingle(56);
            set => WriteSingle(56, value);
        }

        public int ColaPropCoins
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

        public float HitNPCDelta
        {
            get => ReadSingle(72);
            set => WriteSingle(72, value);
        }

        public int HitNPCCoins
        {
            get => ReadInt32(76);
            set => WriteInt32(76, value);
        }

        public int BustedCoins
        {
            get => ReadInt32(80);
            set => WriteInt32(80, value);
        }

        public float HitSwitchSkinDelta
        {
            get => ReadSingle(84);
            set => WriteSingle(84, value);
        }

        public float HitChangeVehicleDelta
        {
            get => ReadSingle(88);
            set => WriteSingle(88, value);
        }

        public uint PrevVehicleID
        {
            get => ReadUInt32(92);
            set => WriteUInt32(92, value);
        }

        // 96 = PrevHits[16]

        public int LeastRecentlyHit
        {
            get => ReadInt32(160);
            set => WriteInt32(160, value);
        }

        public bool ChaseOn
        {
            get => ReadByte(164) != 0;
            set => WriteByte(164, (byte)(value ? 1 : 0));
        }

        public bool SpawnOn
        {
            get => ReadByte(165) != 0;
            set => WriteByte(165, (byte)(value ? 1 : 0));
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

        public bool Disabled
        {
            get => ReadByte(184) != 0;
            set => WriteByte(184, (byte)(value ? 1 : 0));
        }

        public bool DecayDisabled
        {
            get => ReadByte(185) != 0;
            set => WriteByte(185, (byte)(value ? 1 : 0));
        }

        public float TicketDistance
        {
            get => ReadSingle(188);
            set => WriteSingle(188, value);
        }

        public float TicketDistanceOnFoot
        {
            get => ReadSingle(192);
            set => WriteSingle(192, value);
        }

        public float TicketSpeedThreshold
        {
            get => ReadSingle(196);
            set => WriteSingle(196, value);
        }

        public float TicketTimeThreshold
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
            get => ReadByte(216) != 0;
            set => WriteByte(216, (byte)(value ? 1 : 0));
        }

        public float VehicleResetTimer
        {
            get => ReadSingle(220);
            set => WriteSingle(220, value);
        }

        public float UnresponsiveTimer
        {
            get => ReadSingle(224);
            set => WriteSingle(224, value);
        }

        public bool FadeDone
        {
            get => ReadByte(228) != 0;
            set => WriteByte(228, (byte)(value ? 1 : 0));
        }

        public float FadeTimer
        {
            get => ReadSingle(232);
            set => WriteSingle(232, value);
        }
    }
}
