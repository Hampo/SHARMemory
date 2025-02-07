using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVRealController@@")]
public class RealController : Class
{
    public const int NUM_CONTROLLER_TYPES = 4;

    public enum ControllerTypes
    {
        Gamepad,
        Keyboard,
        Mouse,
        SteeringWheel,
    }

    public RealController(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint RadControllerOffset = 0;

    internal const uint ConnectedOffset = RadControllerOffset + 4;
    public bool Connected
    {
        get => ReadBoolean(ConnectedOffset);
        set => WriteBoolean(ConnectedOffset, value);
    }

    internal const uint ControllerTypeOffset = ConnectedOffset + 4;
    public ControllerTypes ControllerType
    {
        get => (ControllerTypes)ReadUInt32(ControllerTypeOffset);
        set => WriteUInt32(ControllerTypeOffset, (uint)value);
    }
}
