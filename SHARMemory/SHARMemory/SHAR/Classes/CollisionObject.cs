using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCollisionObject@sim@@")]
public class CollisionObject : Class
{
    public CollisionObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public SimState SimState => Memory.ClassFactory.Create<SimState>(ReadUInt32(24));

    public PhysicsProperties PhysicsProperties => Memory.ClassFactory.Create<PhysicsProperties>(ReadUInt32(28));

    public CollisionVolume CollisionVolume => Memory.ClassFactory.Create<CollisionVolume>(ReadUInt32(32));

    public int NumJoint
    {
        get => ReadInt32(36);
        set => WriteInt32(36, value);
    }

    public PointerArray<SelfCollision> SelfCollisionList => PointerArrayExtensions.FromPtrArray<SelfCollision>(Memory, this, 40);

    public CollisionVolumeOwner CollisionVolumeOwner => Memory.ClassFactory.Create<CollisionVolumeOwner>(Address + 60);

    private byte Bitfield_0x58
    {
        get => ReadByte(88);
        set => WriteByte(88, value);
    }

    public bool IsStatic
    {
        get => (Bitfield_0x58 & 0b00000001) != 0;
        set
        {
            if (value)
                Bitfield_0x58 |= 0b00000001;
            else
                Bitfield_0x58 &= 0b11111110;
        }
    }

    public bool CollideWithStatic
    {
        get => (Bitfield_0x58 & 0b00000010) != 0;
        set
        {
            if (value)
                Bitfield_0x58 |= 0b00000010;
            else
                Bitfield_0x58 &= 0b11111101;
        }
    }

    public int DefaultArea
    {
        get => ReadInt32(92);
        set => WriteInt32(92, value);
    }

    private byte Bitfield_0x60
    {
        get => ReadByte(96);
        set => WriteByte(96, value);
    }

    public bool HasMoved
    {
        get => (Bitfield_0x60 & 0b00000001) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00000001;
            else
                Bitfield_0x60 &= 0b11111110;
        }
    }

    public bool HasRelocated
    {
        get => (Bitfield_0x60 & 0b00000010) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00000010;
            else
                Bitfield_0x60 &= 0b11111101;
        }
    }

    public bool AutoPair
    {
        get => (Bitfield_0x60 & 0b00000100) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00000100;
            else
                Bitfield_0x60 &= 0b11111011;
        }
    }

    public bool ManualUpdate
    {
        get => (Bitfield_0x60 & 0b00001000) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00001000;
            else
                Bitfield_0x60 &= 0b11110111;
        }
    }

    public bool CollisionEnabled
    {
        get => (Bitfield_0x60 & 0b00010000) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00010000;
            else
                Bitfield_0x60 &= 0b11101111;
        }
    }

    public bool SelfCollisionEnabled
    {
        get => (Bitfield_0x60 & 0b00100000) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b00100000;
            else
                Bitfield_0x60 &= 0b11011111;
        }
    }

    public bool RayCastingEnabled
    {
        get => (Bitfield_0x60 & 0b01000000) != 0;
        set
        {
            if (value)
                Bitfield_0x60 |= 0b01000000;
            else
                Bitfield_0x60 &= 0b10111111;
        }
    }

    public int PossibleCollisionEvents
    {
        get => ReadInt32(100);
        set => WriteInt32(100, value);
    }

    public void Relocated()
    {
        CollisionVolume collisionVolume = CollisionVolume;
        if (collisionVolume != null)
            collisionVolume.Updated = false;
        HasMoved = true;
        HasRelocated = true;
    }

    public void Update()
    {
        if (HasMoved || HasRelocated)
        {
            CollisionVolume?.UpdateAll();
        }
    }

    public void Moved(Vector3 dp)
    {
        HasMoved = true;
        CollisionVolume collisionVolume = CollisionVolume;
        if (collisionVolume != null)
        {
            Vector3 position = Vector3.Add(collisionVolume.Position, dp);
            collisionVolume.Position = position;
            collisionVolume.Updated = false;
        }
    }
}
