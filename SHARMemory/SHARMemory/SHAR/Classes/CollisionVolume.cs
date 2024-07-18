using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System.Drawing;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCollisionVolume@sim@@")]
public class CollisionVolume : Class
{
    public enum AxisOrientations
    {
        NotOriented,
        Oriented,
        XOriented,
        YOriented,
        ZOriented,
    }

    public enum Types
    {
        Collision,
        Sphere,
        Cylinder,
        OBBox,
        Wall,
        BBox,
        MaxTypes
    }

    public CollisionVolume(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public Vector3 Position
    {
        get => ReadStruct<Vector3>(8);
        set => WriteStruct(8, value);
    }

    public Vector3 BoxSize
    {
        get => ReadStruct<Vector3>(20);
        set => WriteStruct(20, value);
    }

    public float SphereRadius
    {
        get => ReadSingle(32);
        set => WriteSingle(32, value);
    }

    public Types Type
    {
        get => (Types)ReadInt32(36);
        set => WriteInt32(36, (int)value);
    }

    public int ObjRefIndex
    {
        get => ReadInt32(40);
        set => WriteInt32(40, value);
    }

    public int OwnerIndex
    {
        get => ReadInt32(44);
        set => WriteInt32(44, value);
    }

    public CollisionObject CollisionObject => Memory.ClassFactory.Create<CollisionObject>(ReadUInt32(48));

    public PointerArray<CollisionVolume> SubVolumeList => PointerArrayExtensions.FromTListPointer<CollisionVolume>(Memory, this, 52);

    public bool Visible
    {
        get => ReadBoolean(56);
        set => WriteBoolean(56, value);
    }

    public bool Updated
    {
        get => ReadBoolean(57);
        set => WriteBoolean(57, true);
    }

    public Vector3 DP
    {
        get => ReadStruct<Vector3>(60);
        set => WriteStruct(60, value);
    }

    public Color Colour
    {
        get => ReadStruct<Color>(72);
        set => WriteStruct(72, value);
    }

    public void UpdateAll()
    {
        UpdatePos();
        UpdateRot();
        UpdateBBox();
    }

    public void UpdatePos()
    {
        CollisionObject collisionObject = CollisionObject;
        if (collisionObject != null)
        {
            Updated = false;

            if (Type != Types.BBox && !collisionObject.ManualUpdate)
            {
                SimState simState = collisionObject.SimState;
                if (simState != null)
                {
                    Matrix4x4 transform = simState.Transform;
                    Vector3 position = Position;
                    transform.Transform(DP, ref position);
                    Position = position;
                    simState.Transform = transform;
                }
            }
        }
        else
        {
            Updated = true;
        }

        try
        {
            PointerArray<CollisionVolume> subVolumeList = SubVolumeList;
            if (subVolumeList != null)
                foreach (CollisionVolume subVolume in subVolumeList)
                    subVolume.UpdatePos();
        }
        catch { }
    }

    public void UpdateRot()
    {
        if (CollisionObject != null)
            SetRotation();

        try
        {
            PointerArray<CollisionVolume> subVolumeList = SubVolumeList;
            if (subVolumeList != null)
                foreach (CollisionVolume subVolume in subVolumeList)
                    subVolume.UpdateRot();
        }
        catch { }
    }

    public virtual void UpdateBBox() { }

    public virtual void SetRotation() { }
}
