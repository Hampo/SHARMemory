using System.Text;

namespace SHARMemory.SHAR.Classes
{
    public class Mission : Class
    {
        public enum MissionStates
        {
            Waiting,
            InProgress,
            Failed,
            Success,
            NumStates
        }

        public Mission(Memory memory, uint address) : base(memory, address)
        {
            if (memory.ModLauncherOrdinals.TryGetValue(3360, out uint MaxStagesAddress) && memory.ModLauncherOrdinals.TryGetValue(3364, out uint StagesOffsetAddress))
            {
                MaxStages = memory.ReadUInt32(MaxStagesAddress);
                StagesOffset = memory.ReadUInt32(StagesOffsetAddress);
            }
            else
            {
                MaxStages = 25;
                StagesOffset = 12;
            }
        }

        public bool IsStreetRace1Or2
        {
            get => ReadBoolean(4);
            set => WriteBoolean(4, value);
        }

        public int NumMissionStages
        {
            get => ReadInt32(8);
            set => WriteInt32(8, value);
        }

        private readonly uint MaxStages;
        private readonly uint StagesOffset;
        public PointerArray<MissionStage> MissionStages => new(Memory, Address + StagesOffset, MaxStages);

        public int CurrentStage
        {
            get => ReadInt32(112);
            set => WriteInt32(112, value);
        }

        public int ResetMission
        {
            get => ReadInt32(116);
            set => WriteInt32(116, value);
        }

        public string Name
        {
            get => ReadString(120, Encoding.UTF8, 16);
            set => WriteString(120, value, Encoding.UTF8, 16);
        }

        // GameMemoryAllocator CurrentMissionHeap (136)

        public bool Complete
        {
            get => ReadBoolean(140);
            set => WriteBoolean(140, value);
        }

        public bool IsLastStage
        {
            get => ReadBoolean(141);
            set => WriteBoolean(141, value);
        }

        public int MissionTimer
        {
            get => ReadInt32(144);
            set => WriteInt32(144, value);
        }

        public int ElapsedTimeMS
        {
            get => ReadInt32(148);
            set => WriteInt32(148, value);
        }

        public MissionStates State
        {
            get => (MissionStates)ReadInt32(152);
            set => WriteInt32(152, (int)value);
        }

        public MissionStage.MissionStageStates LastStageState
        {
            get => (MissionStage.MissionStageStates)ReadInt32(156);
            set => WriteInt32(156, (int)value);
        }

        public MissionStage GetCurrentStage()
        {
            int currStage = CurrentStage;
            if (currStage >= 0 && currStage < NumMissionStages)
                return MissionStages[(uint)currStage];
            return null;
        }
    }
}
