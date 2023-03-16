using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVMissionCondition@@")]
    public class MissionCondition : Class
    {
        public enum ConditionTypes
        {
            Invalid,
            VehicleDamage,
            PlayerHit,
            TimeOut,
            PlayerOutOfVehicle,
            FollowDistance,
            OutOfBounds,
            Race,
            LeaveInterior,
            Position,
            CarryingStatepropCollectible,
            NotAbducted,
            HitAndRunCaught,
            KeepBarrel,
            GetCollectibles,
            NumConditions
        }

        public MissionCondition(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public ConditionTypes Type
        {
            get => (ConditionTypes)ReadInt32(4);
            set => WriteInt32(4, (int)value);
        }

        public bool IsViolated
        {
            get => ReadBoolean(8);
            set => WriteBoolean(8, value);
        }

        public bool LeaveInterior
        {
            get => ReadBoolean(9);
            set => WriteBoolean(9, value);
        }
    }
}
