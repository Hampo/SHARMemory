namespace SHARMemory.SHAR.Classes
{
    public class MissionStage : Class
    {
        public enum MissionStageStates
        {
            Idle,
            InProgress,
            Compelte,
            Failed,
            Backup,
            AllComplete,
            NumStates
        }

        public MissionStage(Memory memory, uint address) : base(memory, address) { }

        public bool StayBlackForStage
        {
            get => ReadBoolean(96);
            set => WriteBoolean(96, value);
        }

        public bool DisablePlayerControlForCountDown
        {
            get => ReadBoolean(97);
            set => WriteBoolean(97, value);
        }

        public MissionStageStates State
        {
            get => (MissionStageStates)ReadInt32(100);
            set => WriteInt32(100, (int)value);
        }

        public MissionObjective Objective => Memory.CreateClass<MissionObjective>(ReadUInt32(104));

        public int NumConditions
        {
            get => ReadInt32(108);
            set => WriteInt32(108, value);
        }

        public PointerArray<MissionCondition> Conditions => new(Memory, Address + 112, 8);
    }
}
