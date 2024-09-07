using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System;
using System.Collections.Generic;

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
        List<byte> classBytes = new(sizeof(uint) + sizeof(int) + 4);

        classBytes.AddRange(BitConverter.GetBytes(memory.SelectAddress(0x611288, 0x611330, 0x6112F8, 0x611388)));
        classBytes.AddRange(BitConverter.GetBytes((int)ConditionTypes.TimeOut));
        classBytes.Add((byte)(isViolated ? 1 : 0));
        classBytes.Add((byte)(leaveInterior ? 1 : 0));
        classBytes.Add((byte)(done ? 1 : 0));
        classBytes.Add(0);

        uint address = memory.AllocateAndWriteMemory([.. classBytes]);
        return memory.ClassFactory.Create<TimeOutCondition>(address);
    }
}
