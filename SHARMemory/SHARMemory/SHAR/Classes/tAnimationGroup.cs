using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtAnimationGroup@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class tAnimationGroup : Class
{
    public tAnimationGroup(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint tAnimationGroupVFTableOffset = 0;

    internal const uint NameOffset = tAnimationGroupVFTableOffset + sizeof(uint);
    public long Name
    {
        get => ReadInt64(NameOffset);
        set => WriteInt64(NameOffset, value);
    }

    internal const uint GroupIdOffset = NameOffset + sizeof(long);
    public int GroupId
    {
        get => ReadInt32(GroupIdOffset);
        set => WriteInt32(GroupIdOffset, value);
    }

    internal const uint NumChannelsOffset = GroupIdOffset + sizeof(int);
    public int NumChannels
    {
        get => ReadInt32(NumChannelsOffset);
        set => WriteInt32(NumChannelsOffset, value);
    }

    internal const uint ChannelsOffset = NumChannelsOffset + sizeof(int);
    public PointerArray<tChannel> Channels => new(Memory, ReadUInt32(ChannelsOffset), NumChannels);

    internal const uint MemoryBlockOffset = ChannelsOffset + sizeof(uint);
    public tAnimationMemoryBlock MemoryBlock => Memory.ClassFactory.Create<tAnimationMemoryBlock>(ReadUInt32(MemoryBlockOffset));
}
