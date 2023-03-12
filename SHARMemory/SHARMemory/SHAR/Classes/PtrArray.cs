namespace SHARMemory.SHAR.Classes
{
    public class PtrArray<T> : Class where T : Class
    {
        public PtrArray(Memory memory, uint address) : base(memory, address) { }

        public uint Size => ReadUInt32(4);

        public PointerArray<T> Values => new(Memory, ReadUInt32(8), Size);
    }
}
