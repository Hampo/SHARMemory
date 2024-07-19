using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtAnimation@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class tAnimation : tEntity
{
    public tAnimation(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint NumFramesOffset = NameOffset + sizeof(long);
    public float NumFrames
    {
        get => ReadSingle(NumFramesOffset);
        set => WriteSingle(NumFramesOffset, value);
    }

    internal const uint SpeedOffset = NumFramesOffset + sizeof(float);
    public float Speed
    {
        get => ReadSingle(SpeedOffset);
        set => WriteSingle(SpeedOffset, value);
    }

    internal const uint CyclicOffset = SpeedOffset + sizeof(float);
    public bool Cyclic
    {
        get => ReadBoolean(CyclicOffset);
        set => WriteBoolean(CyclicOffset, value);
    }

    internal const uint AnimTypeOffset = CyclicOffset + 4; // Padding
    public uint AnimType // TODO: Look into better FourCC handling
    {
        get => ReadUInt32(AnimTypeOffset);
        set => WriteUInt32(AnimTypeOffset, value);
    }

    internal const uint NumGroupsOffset = AnimTypeOffset + sizeof(uint);
    public int NumGroups
    {
        get => ReadInt32(NumGroupsOffset);
        set => WriteInt32(NumGroupsOffset, value);
    }

    internal const uint GroupsOffset = NumGroupsOffset + sizeof(int);
    public PointerArray<tAnimationGroup> Groups => new(Memory, ReadUInt32(GroupsOffset), NumGroups);

    internal const uint MemoryBlockOffset = GroupsOffset + sizeof(uint);

    internal const uint SortOrderOffset = MemoryBlockOffset + sizeof(uint);
}
