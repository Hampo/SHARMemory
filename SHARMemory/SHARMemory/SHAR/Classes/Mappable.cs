using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVMappable@@")]
public class Mappable : radLoadObject
{
    public Mappable(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint ButtonMaskOffset = RefCountOffset + sizeof(uint);
    public uint ButtonMask
    {
        get => ReadUInt32(ButtonMaskOffset);
        set => WriteUInt32(ButtonMaskOffset, value);
    }

    public const uint ButtonArrayOffset = ButtonMaskOffset + sizeof(int);
    public ClassArray<Button> ButtonArray => new(Memory, Address + ButtonArrayOffset, Button.Size, UserController.MAX_PHYSICAL_BUTTONS);
}
