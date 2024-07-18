namespace SHARMemory.Memory.RTTI;

public class BaseClassDescriptor : Class
{
    public readonly TypeInfo TypeInfo;
    public readonly uint NumContainedBases;
    public readonly PMD Where;
    public readonly uint Attributes;

    public BaseClassDescriptor(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator)
    {
        TypeInfo = Memory.ClassFactory.Create<TypeInfo>(ReadUInt32(0));
        NumContainedBases = ReadUInt32(4);
        Where = ReadStruct<PMD>(8);
        Attributes = ReadUInt32(8 + PMD.Size);
    }

    public override string ToString() => TypeInfo.ToString();
}