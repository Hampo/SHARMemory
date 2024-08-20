using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSoundManager@@")]
public class SoundManager : Class
{
    public SoundManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint EventListenerVFTableOffset = 0;
    internal const uint GameConfigHandlerVFTableOffset = EventListenerVFTableOffset + sizeof(uint);
    internal const uint GameDataHandlerVFTableOffset = GameConfigHandlerVFTableOffset + sizeof(uint);

    internal const uint DialogCoordinatorOffset = GameDataHandlerVFTableOffset + sizeof(uint);
    public DialogCoordinator DialogCoordinator => Memory.ClassFactory.Create<DialogCoordinator>(ReadUInt32(DialogCoordinatorOffset));
}
