using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class CharacterManager : Pointer
    {
        public CharacterManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8470, 0x6C8430, 0x6C8430, 0x6C8468)) { }

        public PointerArray<Character> Characters => new(Memory, Value + 192, 64);

        public Character Player => Characters[0];
    }
}
