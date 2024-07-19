using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVradLoadObject@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical's naming")]
public class radLoadObject : IRefCount
{
    public radLoadObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint RefCountOffset = IRefCountVFTableOffset + sizeof(uint);
    public uint RefCount
    {
        get => ReadUInt32(RefCountOffset);
        set => WriteUInt32(RefCountOffset, value);
    }
}
