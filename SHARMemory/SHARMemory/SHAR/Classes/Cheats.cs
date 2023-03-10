using System;

namespace SHARMemory.SHAR.Classes
{
    public class Cheats
    {
        [Flags]
        public enum Cheat
        {
            MotherOfAllCheats = 1 << 0,
            UnlockAllCards = 1 << 1,
            UnlockAllOutfits = 1 << 2,
            UnlockAllStoryMissions = 1 << 3,
            UnlockAllMovies = 1 << 4,
            UnlockAllRewardVehicles = 1 << 5,
            UnlockEverything = 1 << 6,
            NoTopSpeed = 1 << 7,
            HighAcceleration = 1 << 8,
            CarJumpOnHorn = 1 << 9,
            FlamingCar = 1 << 10,
            OneTapTrafficDeath = 1 << 11,
            x5StageTime = 1 << 12,
            ShowAvatarPosition = 1 << 13,
            KickSwapsCharacterModel = 1 << 14,
            ExtraCoins = 1 << 15,
            UnlockAllCameras = 1 << 16,
            DemoTest = 1 << 17,
            PlayCreditsDialogue = 1 << 18,
            ShowSpeedometer = 1 << 19,
            RedBrick = 1 << 20,
            InvincibleCar = 1 << 21,
            ShowTree = 1 << 22,
            Trippy = 1 << 23
        }

        public Memory Memory { get; }

        public Cheats(Memory memory)
        {
            Memory = memory;
        }

        public uint EnabledCheats
        {
            get => Memory.ReadUInt32(Memory.SelectAddress(0x6C8420, 0x6C83E0, 0x6C83E0, 0x6C8418));
            set => Memory.WriteUInt32(Memory.SelectAddress(0x6C8420, 0x6C83E0, 0x6C83E0, 0x6C8418), value);
        }

        public bool IsCheatEnabled(Cheat cheat) => (EnabledCheats & (uint)cheat) != 0;

        public void SetCheatEnabled(Cheat cheat, bool enabled)
        {
            if (enabled)
                EnabledCheats |= (uint)cheat;
            else
                EnabledCheats &= ~(uint)cheat;
        }
    }
}
