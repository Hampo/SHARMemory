namespace SHARMemory.SHAR.Pointers
{
    public class MissionManager : Pointer
    {
        public MissionManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8994, 0x6C8954, 0x6C8954, 0x6C898C)) { }
    }
}
