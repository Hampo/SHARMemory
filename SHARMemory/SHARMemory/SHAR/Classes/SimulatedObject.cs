using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSimulatedObject@sim@@")]
public class SimulatedObject : Class
{
    public enum Types
    {
        RigidObject,
        ArticulatedObject,
        FlexibleObject,
        MaxPhObjEnum
    }

    public SimulatedObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public SimState SimState => Memory.ClassFactory.Create<SimState>(ReadUInt32(16));

    public float ProgressTime
    {
        get => ReadSingle(20);
        set => WriteSingle(20, value);
    }

    public PhysicsProperties PhysicsProperties => Memory.ClassFactory.Create<PhysicsProperties>(ReadUInt32(24));

    public SimEnvironment SimEnvironment => Memory.ClassFactory.Create<SimEnvironment>(ReadUInt32(28));

    public bool UseRestingDetector
    {
        get => ReadBoolean(32);
        set => WriteBoolean(32, value);
    }

    public Types Type
    {
        get => (Types)ReadInt32(36);
        set => WriteInt32(36, (int)value);
    }

    public int RefIndex
    {
        get => ReadInt32(40);
        set => WriteInt32(40, value);
    }
}
