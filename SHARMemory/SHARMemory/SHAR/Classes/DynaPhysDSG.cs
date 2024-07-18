using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDynaPhysDSG@@")]
public class DynaPhysDSG : StaticPhysDSG
{
    public DynaPhysDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint IsHitOffset = ShadowMatrixOffset + sizeof(uint);
    public bool IsHit
    {
        get => ReadBoolean(IsHitOffset);
        set => WriteBoolean(IsHitOffset, value);
    }

    internal const uint PastLinearOffset = IsHitOffset + 4; // Bool padding
    public Smoother PastLinear
    {
        get => ReadStruct<Smoother>(PastLinearOffset);
        set => WriteStruct(PastLinearOffset, value);
    }

    internal const uint PastAngularOffset = PastLinearOffset + Smoother.Size;
    public Smoother PastAngular
    {
        get => ReadStruct<Smoother>(PastAngularOffset);
        set => WriteStruct(PastAngularOffset, value);
    }

    internal const uint GroundPlaneIndexOffset = PastAngularOffset + Smoother.Size;
    public int GroundPlaneIndex
    {
        get => ReadInt32(GroundPlaneIndexOffset);
        set => WriteInt32(GroundPlaneIndexOffset, value);
    }

    internal const uint GroundPlaneRefsOffset = GroundPlaneIndexOffset + sizeof(int);
    public int GroundPlaneRefs
    {
        get => ReadInt32(GroundPlaneRefsOffset);
        set => WriteInt32(GroundPlaneRefsOffset, value);
    }
}
