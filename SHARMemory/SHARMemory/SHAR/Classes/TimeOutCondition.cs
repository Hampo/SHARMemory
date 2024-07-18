using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVTimeOutCondition@@")]
public class TimeOutCondition : MissionCondition
{
    public TimeOutCondition(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public bool Done
    {
        get => ReadBoolean(10);
        set => WriteBoolean(10, value);
    }

    public static TimeOutCondition CreateNew(Memory memory, bool isViolated, bool leaveInterior, bool done)
    {
        uint address = memory.AllocateMemory();

        memory.WriteUInt32(address, memory.SelectAddress(0x611288, 0x611330, 0x6112F8, 0x611388));
        memory.WriteInt32(address + 4, (int)ConditionTypes.TimeOut);
        memory.WriteBoolean(address + 8, isViolated);
        memory.WriteBoolean(address + 9, leaveInterior);
        memory.WriteBoolean(address + 10, done);

        return memory.ClassFactory.Create<TimeOutCondition>(address);
    }
}
