using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtEntity@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class tEntity : tRefCounted
{
    public tEntity(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint NameOffset = RefCountOffset + sizeof(uint);
    public long Name
    {
        get => ReadInt64(NameOffset);
        set => WriteInt64(NameOffset, value);
    }
}
