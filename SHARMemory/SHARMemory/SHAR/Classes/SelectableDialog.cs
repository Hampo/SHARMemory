using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSelectableDialog@@")]
public class SelectableDialog : Class
{
    public SelectableDialog(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint SelectableDialogVFTableOffset = 0;

    internal const uint MissionNumOffset = SelectableDialogVFTableOffset + sizeof(uint);
    public int MissionNum
    {
        get => ReadInt32(MissionNumOffset);
        set => WriteInt32(MissionNumOffset, value);
    }

    internal const uint LevelNumOffset = MissionNumOffset + sizeof(int);
    public int LevelNum
    {
        get => ReadInt32(LevelNumOffset);
        set => WriteInt32(LevelNumOffset, value);
    }

    internal const uint EventOffset = LevelNumOffset + sizeof(int);
    public Globals.Events Event
    {
        get => (Globals.Events)ReadUInt32(EventOffset);
        set => WriteUInt32(EventOffset, (uint)value);
    }

    internal const uint NextListObjectOffset = EventOffset + sizeof(uint);
    public SelectableDialog NextListObject => Memory.ClassFactory.Create<SelectableDialog>(ReadUInt32(NextListObjectOffset));
}
