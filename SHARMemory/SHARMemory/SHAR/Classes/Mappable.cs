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

    internal const uint ButtonsOffset = ButtonMaskOffset + sizeof(int);
    public ClassArray<Button> Buttons => new(Memory, Address + ButtonsOffset, Button.Size, UserController.MAX_PHYSICAL_BUTTONS);

    internal const uint MappersOffset = ButtonsOffset + Button.Size * UserController.MAX_PHYSICAL_BUTTONS;
    public ClassArray<Mapper> Mappers => new(Memory, Address + MappersOffset, Mapper.Size, UserController.MAX_MAPPINGS);

    internal const uint ActiveMapperOffset = MappersOffset + Mapper.Size * UserController.MAX_MAPPINGS;
    public uint ActiveMapper
    {
        get => ReadUInt32(ActiveMapperOffset);
        set => WriteUInt32(ActiveMapperOffset, value);
    }

    internal const uint ActiveStateOffset = ActiveMapperOffset + sizeof(uint);
    public InputManager.ActiveState ActiveState
    {
        get => (InputManager.ActiveState)ReadUInt32(ActiveStateOffset);
        set => WriteUInt32(ActiveStateOffset, (uint)value);
    }

    internal const uint ActiveOffset = ActiveStateOffset + sizeof(uint);
    public bool Active
    {
        get => ReadBoolean(ActiveOffset);
        set => WriteBoolean(ActiveOffset, true);
    }

    internal const uint NeedResyncOffset = ActiveOffset + sizeof(bool);
    public bool NeedResync
    {
        get => ReadBoolean(NeedResyncOffset);
        set => WriteBoolean(NeedResyncOffset, true);
    }
}
