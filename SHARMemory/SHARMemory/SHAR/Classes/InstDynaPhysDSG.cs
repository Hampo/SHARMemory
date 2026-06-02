using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVInstDynaPhysDSG@@")]
public class InstDynaPhysDSG : DynaPhysDSG
{
    public InstDynaPhysDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint PhysObjOffset = GroundPlaneRefsOffset + sizeof(int);
    public PhysicsObject PhysObj => Memory.ClassFactory.Create<PhysicsObject>(ReadUInt32(PhysObjOffset));

    internal const uint MatrixOffset = PhysObjOffset + sizeof(uint);
    public Matrix4x4 Matrix
    {
        get => ReadStruct<Matrix4x4>(MatrixOffset);
        set => WriteStruct(MatrixOffset, value);
    }

    internal const uint GeoOffset = MatrixOffset + Matrix4x4.Size;
    public tDrawable Geo => Memory.ClassFactory.Create<tDrawable>(ReadUInt32(GeoOffset));

    internal const uint HideOnHitIndexOffset = GeoOffset + sizeof(uint);
    public int HideOnHitIndex
    {
        get => ReadInt32(HideOnHitIndexOffset);
        set => WriteInt32(HideOnHitIndexOffset, value);
    }
}
