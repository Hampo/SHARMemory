namespace SHARMemory.SHAR.Classes
{
    public class TimeOutCondition : MissionCondition
    {
        public TimeOutCondition(Memory memory, uint address) : base(memory, address) { }

        public bool Done
        {
            get => ReadBoolean(10);
            set => WriteBoolean(10, value);
        }

        public static TimeOutCondition CreateNew(Memory memory, bool isViolated, bool leaveInterior, bool done)
        {
            uint address = memory.AllocateMemory();

            memory.WriteUInt32(address, 0x611288);
            memory.WriteInt32(address + 4, (int)ConditionTypes.TimeOut);
            memory.WriteBoolean(address + 8, isViolated);
            memory.WriteBoolean(address + 9, leaveInterior);
            memory.WriteBoolean(address + 10, done);

            return memory.CreateClass<TimeOutCondition>(address);
        }
    }
}
