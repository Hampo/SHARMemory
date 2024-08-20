using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDialogCoordinator@@")]
public class DialogCoordinator : Class
{
    public DialogCoordinator(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint EventListenerVFTableOffset = 0;

    internal const uint DialogNamespaceOffset = EventListenerVFTableOffset + sizeof(uint);

    internal const uint DialogListOffset = DialogNamespaceOffset + sizeof(uint);
    public Class DialogList => Memory.ClassFactory.Create<Class>(ReadUInt32(DialogListOffset));

    internal const uint PlaybackQueueOffset = DialogListOffset + sizeof(uint);
    public DialogPriorityQueue PlaybackQueue => Memory.ClassFactory.Create<DialogPriorityQueue>(ReadUInt32(PlaybackQueueOffset));

    internal const uint DialogOnOffset = PlaybackQueueOffset + sizeof(uint);
    public bool DialogOn
    {
        get => ReadBoolean(DialogOnOffset);
        set => WriteBoolean(DialogOnOffset, value);
    }

    internal const uint PhoneBoothRequestMadeOffset = DialogOnOffset + 1;
    public bool PhoneBoothRequestMade
    {
        get => ReadBoolean(PhoneBoothRequestMadeOffset);
        set => WriteBoolean(PhoneBoothRequestMadeOffset, value);
    }
}
