using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System.Text;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVGameplayManager@@")]
    public class GameplayManager : Class
    {
        public const int MAX_MISSIONS = 20;
        public const int MAX_BONUS_MISSIONS = 10;
        public const int MAX_VDU_CARS = 10;

        public enum CarSlots
        {
            DefaultCar,
            OtherCar,
            AICar
        }

        public enum GameTypes
        {
            Normal,
            SuperSprint, // Bonus Game
            Num_GameTypes
        }

        public enum Messages
        {
            None,
            NextMission,
            PrevMission
        }

        public enum Ratings
        {
            DNF,
            Bronze,
            Silver,
            Gold,
            Num_Ratins
        }

        public GameplayManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public bool IsDemo
        {
            get => ReadBoolean(8);
            set => WriteBoolean(8, value);
        }

        public PlayerAndCarInfo PlayerAndCarInfo
        {
            get => ReadStruct<PlayerAndCarInfo>(12);
            set => WriteStruct(12, value);
        }

        public int CharacterIndex
        {
            get => ReadInt32(40);
            set => WriteInt32(40, value);
        }

        public StructArray<CarData> VehicleSlots => new(Memory, ReadUInt32(44), CarData.Size, 3);

        public string DefaultLevelVehicleName
        {
            get => ReadString(416, Encoding.UTF8, 64);
            set => WriteString(416, value, Encoding.UTF8, 64);
        }

        public string DefaultLevelVehicleLocator
        {
            get => ReadString(480, Encoding.UTF8, 64);
            set => WriteString(480, value, Encoding.UTF8, 64);
        }

        public string DefaultLevelVehicleConfig
        {
            get => ReadString(544, Encoding.UTF8, 64);
            set => WriteString(544, value, Encoding.UTF8, 64);
        }

        public bool ShouldLoadDefaultVehicle
        {
            get => ReadBoolean(608);
            set => WriteBoolean(608, value);
        }

        public StructArray<MissionCarData> MissionVehicleSlots => new(Memory, ReadUInt32(612), MissionCarData.Size, 5);

        public StructArray<Structs.ChaseManager> ChaseManagers => new(Memory, ReadUInt32(832), Structs.ChaseManager.Size, 1);

        public int BlackScreenTimer
        {
            get => ReadInt32(916);
            set => WriteInt32(916, value);
        }

        public char SkipSunday
        {
            get => (char)ReadByte(920);
            set => WriteByte(920, (byte)value);
        }

        public int AIIndex
        {
            get => ReadInt32(924);
            set => WriteInt32(924, value);
        }

        public GameTypes GameType
        {
            get => (GameTypes)ReadUInt32(928);
            set => WriteUInt32(924, (uint)value);
        }

        public string PostLevelFMV
        {
            get => ReadString(932, Encoding.UTF8, 13);
            set => WriteString(932, value, Encoding.UTF8, 13);
        }

        private byte Bitfield_0x3B1
        {
            get => ReadByte(945);
            set => WriteByte(945, value);
        }

        public bool IrisClosed
        {
            get => (Bitfield_0x3B1 & 0b00000001) != 0;
            set
            {
                if (value)
                    Bitfield_0x3B1 |= 0b00000001;
                else
                    Bitfield_0x3B1 &= 0b11111110;
            }
        }

        public bool FadedToBlack
        {
            get => (Bitfield_0x3B1 & 0b00000010) != 0;
            set
            {
                if (value)
                    Bitfield_0x3B1 |= 0b00000010;
                else
                    Bitfield_0x3B1 &= 0b11111101;
            }
        }

        public bool WaitingOnFMV
        {
            get => (Bitfield_0x3B1 & 0b00000100) != 0;
            set
            {
                if (value)
                    Bitfield_0x3B1 |= 0b00000100;
                else
                    Bitfield_0x3B1 &= 0b11111011;
            }
        }

        public int CurrentMission
        {
            get => ReadInt32(948);
            set => WriteInt32(948, value);
        }

        public int NumPlayers
        {
            get => ReadInt32(952);
            set => WriteInt32(952, value);
        }

        public LevelData LevelData
        {
            get => ReadStruct<LevelData>(956);
            set => WriteStruct(956, value);
        }

        public int NumMissions
        {
            get => ReadInt32(1056);
            set => WriteInt32(1056, value);
        }

        public PointerArray<Mission> Missions => new(Memory, Address + 1060, MAX_MISSIONS + MAX_BONUS_MISSIONS);

        // GameMemoryAllocator CurrentMissionHeap (1080)

        private byte Bitfield_0x4A0
        {
            get => ReadByte(1184);
            set => WriteByte(1184, value);
        }

        public bool LevelComplete
        {
            get => (Bitfield_0x4A0 & 0b00000001) != 0;
            set
            {
                if (value)
                    Bitfield_0x4A0 |= 0b00000001;
                else
                    Bitfield_0x4A0 &= 0b11111110;
            }
        }

        public bool EnablePhoneBooths
        {
            get => (Bitfield_0x4A0 & 0b00000010) != 0;
            set
            {
                if (value)
                    Bitfield_0x4A0 |= 0b00000010;
                else
                    Bitfield_0x4A0 &= 0b11111101;
            }
        }

        public bool GameComplete
        {
            get => (Bitfield_0x4A0 & 0b00000100) != 0;
            set
            {
                if (value)
                    Bitfield_0x4A0 |= 0b00000100;
                else
                    Bitfield_0x4A0 &= 0b11111011;
            }
        }

        public Vehicle CurrentVehicle => Memory.ClassFactory.Create<Vehicle>(ReadUInt32(1188));

        public VDU VDU
        {
            get => ReadStruct<VDU>(1192);
            set => WriteStruct(119, value);
        }

        public int NumBonusMissions
        {
            get => ReadInt32(1236);
            set => WriteInt32(1236, value);
        }

        public int CurrentBonusMission
        {
            get => ReadInt32(1240);
            set => WriteInt32(1240, value);
        }

        public int DesiredBonusMission
        {
            get => ReadInt32(1244);
            set => WriteInt32(1244, value);
        }

        public bool IsInBonusMission
        {
            get => ReadBoolean(1248);
            set => WriteBoolean(1248, value);
        }

        public bool FireBonusMissionDialogue
        {
            get => ReadBoolean(1249);
            set => WriteBoolean(1249, value);
        }

        public bool JumpToBonusMission
        {
            get => ReadBoolean(1250);
            set => WriteBoolean(1250, value);
        }

        public bool UpdateBonusMission
        {
            get => ReadBoolean(1251);
            set => WriteBoolean(1251, value);
        }

        // BonusMissionInfo[MAX_BONUS_MISSIONS] BonusMissions (1252)

        public Messages CurrentMessage
        {
            get => (Messages)ReadInt32(6052);
            set => WriteInt32(6052, (int)value);
        }

        public RespawnManager RespawnManager => Memory.ClassFactory.Create<RespawnManager>(ReadUInt32(6056));

        public float IrisSpeed
        {
            get => ReadSingle(6060);
            set => WriteSingle(6060, value);
        }

        public bool PutPlayerInCae
        {
            get => ReadBoolean(6064);
            set => WriteBoolean(6064, value);
        }

        public bool ManualControlFade
        {
            get => ReadBoolean(6065);
            set => WriteBoolean(6065, value);
        }

        public int CurrentVehicleIconID
        {
            get => ReadInt32(6068);
            set => WriteInt32(6068, value);
        }

        public uint ElapsedIdleTime
        {
            get => ReadUInt32(6072);
            set => WriteUInt32(6072, value);
        }

        public Mission GetCurrentMission()
        {
            if (IsInBonusMission)
            {
                int currBonusMission = CurrentBonusMission;
                if (currBonusMission >= MAX_MISSIONS && currBonusMission < MAX_MISSIONS + MAX_BONUS_MISSIONS)
                    return Missions[currBonusMission];
            }
            else
            {
                int currMission = CurrentMission;
                if (currMission >= 0 && currMission < NumMissions)
                    return Missions[currMission];
            }

            return null;
        }

        public void RepairCurrentVehicle()
        {
            Vehicle currVehicle = CurrentVehicle;
            if (currVehicle == null)
                return;

            CarData testVehicleData = VehicleSlots[(int)CarSlots.DefaultCar];
            if (currVehicle == testVehicleData.Vehicle)
            {
                RepairVehicle(CarSlots.DefaultCar, testVehicleData);
            }
            else
            {
                testVehicleData = VehicleSlots[(int)CarSlots.OtherCar];
                if (currVehicle == testVehicleData.Vehicle)
                {
                    RepairVehicle(CarSlots.OtherCar, testVehicleData);

                    MissionCarData[] missionVehicleSlots = MissionVehicleSlots.ToArray();
                    for (int i = 0; i < missionVehicleSlots.Length; i++)
                    {
                        MissionCarData missionCarData = missionVehicleSlots[i];
                        if (missionCarData.Vehicle == testVehicleData.Vehicle)
                        {
                            //missionCarData.HuskVehicle->Release();
                            missionCarData.HuskVehicle = null;
                            missionCarData.UsingHusk = false;
                            missionVehicleSlots[i] = missionCarData;
                        }
                    }
                    MissionVehicleSlots.FromArray(missionVehicleSlots);
                }
            }
        }

        private void RepairVehicle(CarSlots carSlot, CarData carData)
        {
            if (carSlot == CarSlots.AICar)
                return;

            Vehicle veh = carData.Vehicle;
            if (veh == null)
                return;

            if (carData.UsingHusk)
            {
                Vehicle husk = carData.HuskVehicle;

                Vector3 carPos = husk.Position;
                float angle = (float)husk.GetFacingInRadians();

                Matrix4x4 m = new(0);
                m.Identity();
                m.FillRotateXYZ(0f, angle, 0f);
                m.FillTranslate(carPos);
                veh.SetTransform(m);
            }

            VehicleSlots[(int)carSlot] = carData;
        }
    }
}
