namespace SHARMemory.SHAR.Classes
{
    public class CharacterSheetManager : Class
    {
        public CharacterSheetManager(Memory memory, uint address) : base(memory, address) { }

        public CharacterSheet CharacterSheet => new(Memory, Address + 4);
    }
}
