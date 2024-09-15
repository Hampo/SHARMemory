using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

public class Mapper : Class
{
    public Mapper(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint ButtonMapOffset = 0;
    public StructArray<int> ButtonMap => new(Memory, Address + ButtonMapOffset, sizeof(int), UserController.MAX_PHYSICAL_BUTTONS);

    public const uint Size = ButtonMapOffset + sizeof(int) * UserController.MAX_PHYSICAL_BUTTONS;
}
