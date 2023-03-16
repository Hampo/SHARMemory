namespace SHARMemory.Memory.RTTI
{
    public class BaseClassDescriptor : Class
    {
        public TypeInfo TypeInfo => Memory.ClassFactory.Create<TypeInfo>(ReadUInt32(0));
        public uint NumContainedBases => ReadUInt32(4);
        public PMD Where => ReadStruct<PMD>(8);
        public uint Attributes => ReadUInt32(8 + PMD.Size);

        public BaseClassDescriptor(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public override string ToString() => TypeInfo.ToString();
    }
}