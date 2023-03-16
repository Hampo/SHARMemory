namespace SHARMemory.Memory.RTTI
{
    public class CompleteObjectLocator : Class
    {
        public uint Signature => ReadUInt32(0);
        public uint Offset => ReadUInt32(4);
        public uint ConstructorDisplacementOffset => ReadUInt32(8);
        public TypeInfo TypeInfo => Memory.ClassFactory.Create<TypeInfo>(ReadUInt32(12));
        public ClassHierarchyDescriptor ClassDescriptor => Memory.ClassFactory.Create<ClassHierarchyDescriptor>(ReadUInt32(16));

        public CompleteObjectLocator(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public override string ToString() => TypeInfo.ToString();
    }
}