namespace SHARMemory.Memory.RTTI
{
    public class ClassHierarchyDescriptor : Class
    {
        [System.Flags]
        public enum Attributes2
        {
            MultipleInheritance = 1,
            VirtualInheritance = 2,
        }

        public uint Signature => ReadUInt32(0);
        public Attributes2 Attributes => (Attributes2)ReadUInt32(4);
        public uint NumBaseClasses => ReadUInt32(8);
        public PointerArray<BaseClassDescriptor> BaseClassArray => new(Memory, ReadUInt32(12), NumBaseClasses);

        public ClassHierarchyDescriptor(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }
    }
}