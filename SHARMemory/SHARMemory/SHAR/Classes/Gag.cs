using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVGag@@")]
public class Gag : ButtonHandler
{
    public Gag(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint AnimationPlayerLoadDataCallBackVFTableOffset = ActionButtonOffset + sizeof(uint);

    internal const uint EventListenerVFTableOffset = AnimationPlayerLoadDataCallBackVFTableOffset + sizeof(uint);

    internal const uint NISSoundPlaybackCompleteCallbackVFTableOffset = EventListenerVFTableOffset + sizeof(uint);

    internal const uint DialogEventDataOffset = NISSoundPlaybackCompleteCallbackVFTableOffset + sizeof(uint);

}
