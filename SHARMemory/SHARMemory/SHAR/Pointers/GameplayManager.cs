namespace SHARMemory.SHAR.Pointers
{
    public class GameplayManager : Pointer
    {
        public GameplayManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8998, 0x6C8958, 0x6C8958, 0x6C8990)) { }

        // TODO: Actually populate this
    }
}
