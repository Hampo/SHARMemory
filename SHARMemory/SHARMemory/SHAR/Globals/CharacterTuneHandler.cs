using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR;

public partial class Globals
{
    public sealed class CharacterTuneHandler
    {
        private readonly Memory Memory;

        internal CharacterTuneHandler(Memory memory)
        {
            Memory = memory;
        }

        public float SlamForce
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C837C, 0x6C833C, 0x6C833C, 0x6C8374));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C837C, 0x6C833C, 0x6C833C, 0x6C8374), value);
        }

        public float KickingForce
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8380, 0x6C8340, 0x6C8340, 0x6C8378));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8380, 0x6C8340, 0x6C8340, 0x6C8378), value);
        }

        public float GetOutCloseSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8384, 0x6C8344, 0x6C8344, 0x6C837C));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8384, 0x6C8344, 0x6C8344, 0x6C837C), value);
        }

        public float GetOutCloseDelay
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8388, 0x6C8348, 0x6C8348, 0x6C8380));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8388, 0x6C8348, 0x6C8348, 0x6C8380), value);
        }

        public float GetOutOpenSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C838C, 0x6C834C, 0x6C834C, 0x6C8384));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C838C, 0x6C834C, 0x6C834C, 0x6C8384), value);
        }

        public float GetOutOpenDelay
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8390, 0x6C8350, 0x6C8350, 0x6C8388));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8390, 0x6C8350, 0x6C8350, 0x6C8388), value);
        }

        public float GetInCloseSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8394, 0x6C8354, 0x6C8354, 0x6C838C));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8394, 0x6C8354, 0x6C8354, 0x6C838C), value);
        }

        public float GetInCloseDelay
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C8398, 0x6C8358, 0x6C8358, 0x6C8390));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C8398, 0x6C8358, 0x6C8358, 0x6C8390), value);
        }

        public float GetInOpenSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C839C, 0x6C835C, 0x6C835C, 0x6C8394));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C839C, 0x6C835C, 0x6C835C, 0x6C8394), value);
        }

        public float GetInOpenDelay
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83A0, 0x6C8360, 0x6C8360, 0x6C8398));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83A0, 0x6C8360, 0x6C8360, 0x6C8398), value);
        }

        public float GetInHeightThreshold
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83A4, 0x6C8364, 0x6C8364, 0x6C839C));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83A4, 0x6C8364, 0x6C8364, 0x6C839C), value);
        }

        public float GetInOutOfCarAnimSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83A8, 0x6C8368, 0x6C8368, 0x6C83A0));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83A8, 0x6C8368, 0x6C8368, 0x6C83A0), value);
        }

        public float TurboRotateRate
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83AC, 0x6C836C, 0x6C836C, 0x6C83A4));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83AC, 0x6C836C, 0x6C836C, 0x6C83A4), value);
        }

        public float MaxSpeed
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83B0, 0x6C8370, 0x6C8370, 0x6C83A8));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83B0, 0x6C8370, 0x6C8370, 0x6C83A8), value);
        }

        public float HighJumpHeight
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83B4, 0x6C8374, 0x6C8374, 0x6C83AC));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83B4, 0x6C8374, 0x6C8374, 0x6C83AC), value);
        }

        public float DoubleJumpAllowDown
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83B8, 0x6C8378, 0x6C8378, 0x6C83B0));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83B8, 0x6C8378, 0x6C8378, 0x6C83B0), value);
        }

        public float DoubleJumpAllowUp
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83BC, 0x6C837C, 0x6C837C, 0x6C83B4));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83BC, 0x6C837C, 0x6C837C, 0x6C83B4), value);
        }

        public float DoubleJumpHeight
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83C0, 0x6C8380, 0x6C8380, 0x6C83B8));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83C0, 0x6C8380, 0x6C8380, 0x6C83B8), value);
        }

        public float DashDeceleration
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83C4, 0x6C8384, 0x6C8384, 0x6C83BC));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83C4, 0x6C8384, 0x6C8384, 0x6C83BC), value);
        }

        public float DashAcceleration
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83C8, 0x6C8388, 0x6C8388, 0x6C83C0));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83C8, 0x6C8388, 0x6C8388, 0x6C83C0), value);
        }

        public float DashBurstMax
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83CC, 0x6C838C, 0x6C838C, 0x6C83C4));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83CC, 0x6C838C, 0x6C838C, 0x6C83C4), value);
        }

        public float StompGravityScale
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83D0, 0x6C8390, 0x6C8390, 0x6C83C8));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83D0, 0x6C8390, 0x6C8390, 0x6C83C8), value);
        }

        public float AirGravity
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83D4, 0x6C8394, 0x6C8394, 0x6C83CC));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83D4, 0x6C8394, 0x6C8394, 0x6C83CC), value);
        }

        public float AirAccelScale
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83D8, 0x6C8398, 0x6C8398, 0x6C83D0));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83D8, 0x6C8398, 0x6C8398, 0x6C83D0), value);
        }

        public float AirRotateRate
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83DC, 0x6C839C, 0x6C839C, 0x6C83D4));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83DC, 0x6C839C, 0x6C839C, 0x6C83D4), value);
        }

        public bool LocoTest
        {
            get => Memory.ReadBoolean(Memory.SelectAddress(0x6C83E0, 0x6C83A0, 0x6C83A0, 0x6C83D8));
            set => Memory.WriteBoolean(Memory.SelectAddress(0x6C83E0, 0x6C83A0, 0x6C83A0, 0x6C83D8), value);
        }

        public float LocoDecceleration
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83E4, 0x6C83A4, 0x6C83A4, 0x6C83DC));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83E4, 0x6C83A4, 0x6C83A4, 0x6C83DC), value);
        }

        public float LocoAcceleration
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83E8, 0x6C83A8, 0x6C83A8, 0x6C83E0));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83E8, 0x6C83A8, 0x6C83A8, 0x6C83E0), value);
        }

        public float LocoRotateRate
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83EC, 0x6C83AC, 0x6C83AC, 0x6C83E4));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83EC, 0x6C83AC, 0x6C83AC, 0x6C83E4), value);
        }

        public float JumpHeight
        {
            get => Memory.ReadSingle(Memory.SelectAddress(0x6C83F0, 0x6C83B0, 0x6C83B0, 0x6C83E8));
            set => Memory.WriteSingle(Memory.SelectAddress(0x6C83F0, 0x6C83B0, 0x6C83B0, 0x6C83E8), value);
        }

        public Vector3 GetInPosition
        {
            get => Memory.ReadStruct<Vector3>(Memory.SelectAddress(0x6C921C, 0x6C91DC, 0x6C91DC, 0x6C9214));
            set => Memory.WriteStruct(Memory.SelectAddress(0x6C921C, 0x6C91DC, 0x6C91DC, 0x6C9214), value);
        }
    }
}
