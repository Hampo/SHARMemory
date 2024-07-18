using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCharacterManager@@")]
public class CharacterManager : Class
{
    public CharacterManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public PointerArray<Character> Characters => new(Memory, Address + 192, 64);

    public Character Player => Characters[0];
}
