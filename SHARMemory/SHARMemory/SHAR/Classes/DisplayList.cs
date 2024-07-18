using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

public class DisplayList : Class
{
    public DisplayList(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public uint NumDrawables
    {
        get => ReadUInt32(0);
        set => WriteUInt32(0, value);
    }

    public uint NumDrawablesEndPos
    {
        get => ReadUInt32(4);
        set => WriteUInt32(4, value);
    }

    // TODO: Drawable* drawables (8)

    // TODO: Drawable** toSort (12)

    public float SortRange
    {
        get => ReadSingle(16);
        set => WriteSingle(16, value);
    }

    public bool Sorted
    {
        get => ReadBoolean(20);
        set => WriteBoolean(20, value);
    }
}
