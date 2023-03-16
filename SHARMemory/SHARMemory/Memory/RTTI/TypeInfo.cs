namespace SHARMemory.Memory.RTTI
{
    //[ClassFactory.TypeInfoName(".?AVtype_info@@")]
    public class TypeInfo : Class
    {
        //public uint VFTable => ReadUInt32(0);
        public uint Spare => ReadUInt32(4);
        public string ClassName => ReadNullString(8, System.Text.Encoding.ASCII);

        public override string ToString() => ClassName;

        public TypeInfo(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator)
        {

        }
    }
}