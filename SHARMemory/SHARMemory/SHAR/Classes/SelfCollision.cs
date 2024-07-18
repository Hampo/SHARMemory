using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

public class SelfCollision : Class
{
    public SelfCollision(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public int Index1
    {
        get => ReadInt32(0);
        set => WriteInt32(0, value);
    }

    public int Index2
    {
        get => ReadInt32(4);
        set => WriteInt32(4, value);
    }

    public bool Self1
    {
        get => ReadBoolean(8);
        set => WriteBoolean(8, value);
    }

    public bool Self2
    {
        get => ReadBoolean(9);
        set => WriteBoolean(9, value);
    }

    public CollisionVolume CollisionVolume1 => Memory.ClassFactory.Create<CollisionVolume>(ReadUInt32(12));

    public CollisionVolume CollisionVolume2 => Memory.ClassFactory.Create<CollisionVolume>(ReadUInt32(16));
}
