using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVPhysicsJoint@sim@@")]
public class PhysicsJoint : Class
{
    public enum DeformationDissipationMethods
    {
        Linear,
        Amortized
    }

    public PhysicsJoint(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public bool Visible
    {
        get => ReadBoolean(4);
        set => WriteBoolean(4, value);
    }

    public short NumDOF
    {
        get => ReadInt16(5);
        set => WriteInt16(5, value);
    }

    public int Index
    {
        get => ReadInt32(8);
        set => WriteInt32(8, value);
    }

    public int ParentIndex
    {
        get => ReadInt32(12);
        set => WriteInt32(12, value);
    }

    public float MinAngle
    {
        get => ReadSingle(16);
        set => WriteSingle(16, value);
    }

    public float MaxAngle
    {
        get => ReadSingle(20);
        set => WriteSingle(20, value);
    }

    public ArticulatedPhysicsObject ParentObject => Memory.ClassFactory.Create<ArticulatedPhysicsObject>(ReadUInt32(24));

    public PhysicsJoint ParentSimJoint => Memory.ClassFactory.Create<PhysicsJoint>(ReadUInt32(28));

    public PointerArray<PhysicsJoint> ChildrenJoints => PointerArrayExtensions.FromTList<PhysicsJoint>(Memory, this, 32);

    public float LocalVolume
    {
        get => ReadSingle(52);
        set => WriteSingle(52, value);
    }

    public float Volume
    {
        get => ReadSingle(56);
        set => WriteSingle(56, value);
    }

    public float LocalMass
    {
        get => ReadSingle(60);
        set => WriteSingle(60, value);
    }

    public float Mass
    {
        get => ReadSingle(64);
        set => WriteSingle(64, value);
    }

    public float InverseMass
    {
        get => ReadSingle(68);
        set => WriteSingle(68, value);
    }

    public float NormDCMPosition
    {
        get => ReadSingle(72);
        set => WriteSingle(72, value);
    }

    public SymMatrix InertiaMatrix => Memory.ClassFactory.Create<SymMatrix>(Address + 80);

    public SymMatrix LocalInertiaMatrix => Memory.ClassFactory.Create<SymMatrix>(Address + 104);

    public Vector3 DCMPosition
    {
        get => ReadStruct<Vector3>(132);
        set => WriteStruct(132, value);
    }

    public Vector3 DCMPositioni
    {
        get => ReadStruct<Vector3>(144);
        set => WriteStruct(144, value);
    }

    public Vector3 Axis
    {
        get => ReadStruct<Vector3>(156);
        set => WriteStruct(156, value);
    }

    public Matrix4x4 Transform
    {
        get => ReadStruct<Matrix4x4>(168);
        set => WriteStruct(168, value);
    }

    public float InverseStiffness
    {
        get => ReadSingle(232);
        set => WriteSingle(232, value);
    }

    public float AbsorptionFactor
    {
        get => ReadSingle(236);
        set => WriteSingle(236, value);
    }

    public bool OverFLowCache
    {
        get => ReadBoolean(240);
        set => WriteBoolean(240, value);
    }

    public bool CacheComplete
    {
        get => ReadBoolean(241);
        set => WriteBoolean(142, value);
    }

    public bool MaxSpeedReached
    {
        get => ReadBoolean(242);
        set => WriteBoolean(242, value);
    }

    public bool SimBranch
    {
        get => ReadBoolean(243);
        set => WriteBoolean(243, value);
    }

    public float ML2
    {
        get => ReadSingle(244);
        set => WriteSingle(244, value);
    }

    public DeformationDissipationMethods DeformationDissipationMethod
    {
        get => (DeformationDissipationMethods)ReadInt32(248);
        set => WriteInt32(248, (int)value);
    }

    public virtual void ResetDeformation() { }
}
