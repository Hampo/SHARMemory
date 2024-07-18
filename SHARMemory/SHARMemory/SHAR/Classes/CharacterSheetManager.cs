using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCharacterSheetManager@@")]
public class CharacterSheetManager : Class
{
    public CharacterSheetManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public CharacterSheet CharacterSheet => Memory.ClassFactory.Create<CharacterSheet>(Address + 4);
}
