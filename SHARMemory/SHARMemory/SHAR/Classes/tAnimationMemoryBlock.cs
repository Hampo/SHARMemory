using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtAnimationMemoryBlock@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class tAnimationMemoryBlock : tRefCounted
{
    public tAnimationMemoryBlock(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint BlockSizeOffset = RefCountOffset + sizeof(uint);
    public uint BlockSize
    {
        get => ReadUInt32(BlockSizeOffset);
        set => WriteUInt32(BlockSizeOffset, value);
    }

    internal const uint UsedBlocksOffset = BlockSizeOffset + sizeof(uint);
    public uint UsedBlocks
    {
        get => ReadUInt32(UsedBlocksOffset);
        set => WriteUInt32(UsedBlocksOffset, value);
    }

    internal const uint BlockOffset = UsedBlocksOffset + sizeof(uint);
    public StructArray<byte> Block => new(Memory, ReadUInt32(BlockOffset), sizeof(byte), (int)BlockSize);
}
