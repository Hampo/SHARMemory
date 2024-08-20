using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVDialogPriorityQueue@@")]
public class DialogPriorityQueue : Class
{
    public DialogPriorityQueue(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint DialogLineCompleteVFTableOffset = 0;
    internal const uint DialogCompleteVFTableOffset = DialogLineCompleteVFTableOffset + sizeof(uint);

    internal const uint Player1Offset = DialogCompleteVFTableOffset + sizeof(uint);
    public Class Player1 => Memory.ClassFactory.Create<Class>(Player1Offset);

    internal const uint Player2Offset = Player1Offset + 16; // TODO: SimpsonsSoundPlayer.Size
    public Class Player2 => Memory.ClassFactory.Create<Class>(Player2Offset);

    internal const uint PositionalPlayer1Offset = Player2Offset + 16; // TODO: SimpsonsSoundPlayer.Size
    public Class PositionalPlayer1 => Memory.ClassFactory.Create<Class>(PositionalPlayer1Offset);

    internal const uint PositionalPlayer2Offset = PositionalPlayer1Offset + 48; // TODO: PositionalSoundPlayer.Size
    public Class PositionalPlayer2 => Memory.ClassFactory.Create<Class>(PositionalPlayer2Offset);

    internal const uint NowPlayingOffset = PositionalPlayer2Offset + 48; // TODO: PositionalSoundPlayer.Size
    public DialogQueueElement NowPlaying => Memory.ClassFactory.Create<DialogQueueElement>(ReadUInt32(NowPlayingOffset));
}
