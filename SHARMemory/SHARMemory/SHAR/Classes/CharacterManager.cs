using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class CharacterManager : Class
    {
        public CharacterManager(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<Character> Characters => new(Memory, Address + 192, 64);

        public Character Player => Characters[0];
    }
}
