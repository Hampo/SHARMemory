using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDialogQueueElement@@")]
public class DialogQueueElement : Class
{
    public DialogQueueElement(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint IRadTimerCallbackVFTableOffset = 0;
    internal const uint SimpsonsSoundPlayerCallbackVFTableOffset = IRadTimerCallbackVFTableOffset + sizeof(uint);
    internal const uint radBaseObjectVFTableOffset = SimpsonsSoundPlayerCallbackVFTableOffset + sizeof(uint);

    internal const uint radMemoryAllocatorThisAllocatorOffset = radBaseObjectVFTableOffset + sizeof(uint);
    internal const uint radRefCountRefCountOffset = radMemoryAllocatorThisAllocatorOffset + sizeof(uint);

    internal const uint DialogOffset = radRefCountRefCountOffset + sizeof(uint);
    public SelectableDialog Dialog => Memory.ClassFactory.Create<SelectableDialog>(ReadUInt32(DialogOffset));

    // TODO
}
