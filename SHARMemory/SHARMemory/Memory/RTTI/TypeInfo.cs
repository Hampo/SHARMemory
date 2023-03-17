namespace SHARMemory.Memory.RTTI
{
    //[ClassFactory.TypeInfoName(".?AVtype_info@@")]
    public class TypeInfo : Class
    {
        //public readonly uint VFTable;
        public readonly uint Spare;
        public readonly string ClassName;

        public TypeInfo(ProcessMemory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator)
        {
            //VFTable = ReadUInt32(0);
            Spare = ReadUInt32(4);
            ClassName = ReadNullString(8, System.Text.Encoding.ASCII);
        }

        public override string ToString() => ClassName;
    }
}