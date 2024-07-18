using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVPresentationManager@@")]
public class PresentationManager : Class // : EventListener
{
    public PresentationManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public PresentationEvent Current => Memory.ClassFactory.Create<PresentationEvent>(ReadUInt32(80));

    public FMVPlayer FMVPlayer => Memory.ClassFactory.Create<FMVPlayer>(ReadUInt32(84));

    //public NISPlayer NISPlayer => Memory.ClassFactory.Create<NISPlayer>(ReadUInt32(88));

    //public CameraPlayer CameraPlayer => Memory.ClassFactory.Create<CameraPlayer>(ReadUInt32(92));

    //public TransitionPlayer TransitionPlayer => Memory.ClassFactory.Create<TransitionPlayer>(ReadUInt32(96));

    //public PlayerDrawable PlayerDrawable => Memory.ClassFactory.Create<PlayerDrawable>(ReadUInt32(100));

    //public PresentationAnimator PCAnimator => Memory.ClassFactory.Create<PresentationAnimator>(ReadUInt32(104));

    //public PresentationAnimator NPCAnimator => Memory.ClassFactory.Create<PresentationAnimator>(ReadUInt32(108));

    public int DialogLineNumber
    {
        get => ReadInt32(112);
        set => WriteInt32(112, value);
    }

    // TNAMEVECTOR mCameraForLineOfDialog - 116 -> 131

    //public PresentationEventCallBack PlayCallback => Memory.ClassFactory.Create<PresentationEventCallBack>(ReadUInt32(132));

    private byte Bitfield_0x88
    {
        get => ReadByte(136);
        set => WriteByte(136, value);
    }

    public bool InConversation
    {
        get => (Bitfield_0x88 & 0b00000001) != 0;
        set
        {
            if (value)
                Bitfield_0x88 |= 0b00000001;
            else
                Bitfield_0x88 &= 0b11111110;
        }
    }

    public bool WaitingOnFade
    {
        get => (Bitfield_0x88 & 0b00000010) != 0;
        set
        {
            if (value)
                Bitfield_0x88 |= 0b00000010;
            else
                Bitfield_0x88 &= 0b11111101;
        }
    }

    //public PresentationOverlay Overlay => Memory.ClassFactory.Create<PresentationOverlay>(ReadUInt32(140));
}
