using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCharacterController@@")]
public class CharacterController : Class
{
    public enum Intentions
    {
        None,
        LeftStickX,
        LeftStickY,
        DoAction,
        Jump,
        Dash,
        Attack,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        GetOutCar,
        MouseLookLeft,
        MouseLookRight,
        NUM_INPUTS,
        Dodge,
        Cringe,
        TurnRight,
        TurnLeft,
        CelebrateSmall,
        CelebrateBig,
        WaveHello,
        WaveGoodbye
    }

    public CharacterController(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public Intentions Intention
    {
        get => (Intentions)ReadUInt32(12);
        set => WriteUInt32(12, (uint)value);
    }

    public Intentions PreserveIntention
    {
        get => (Intentions)ReadUInt32(16);
        set => WriteUInt32(16, (uint)value);
    }

    public bool Active
    {
        get => ReadBoolean(20);
        set => WriteBoolean(20, value);
    }
}
