using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDialogSelectionGroup@@")]
public class DialogSelectionGroup : SelectableDialog
{
    public DialogSelectionGroup(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint UnknownOffset = NextListObjectOffset + sizeof(uint); // TODO: Check if just part of the Vector

    internal const uint DialogVectorOffset = UnknownOffset + sizeof(uint);
    public PointerArray<SelectableDialog> DialogVector => PointerArrayExtensions.FromVector<SelectableDialog>(Memory, this, DialogVectorOffset);
    public SelectableDialog FirstDialog => DialogVector[0];

    internal const uint CurrentlyPlayingDialogOffset = DialogVectorOffset + sizeof(uint) + sizeof(uint) + sizeof(uint);
    public short CurrentlyPlayingDialog
    {
        get => ReadInt16(CurrentlyPlayingDialogOffset);
        set => WriteInt16(CurrentlyPlayingDialogOffset, value);
    }

    internal const uint LastSelectionOffset = CurrentlyPlayingDialogOffset + sizeof(short);
    public short LastSelection
    {
        get => ReadInt16(LastSelectionOffset);
        set => WriteInt16(LastSelectionOffset, value);
    }
}
