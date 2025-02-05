using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Text;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVMissionManager@@")]
public class MissionManager : GameplayManager
{
    public MissionManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint LoadingManagerProcessRequetsCallbackVFTableOffset = ElapsedIdleTimeOffset + sizeof(uint);

    internal const uint LastFileNameOffset = LoadingManagerProcessRequetsCallbackVFTableOffset + sizeof(uint) + 8; // TF is the 8???
    public string LastFileName
    {
        get => ReadString(LastFileNameOffset, Encoding.UTF8, 256);
        set => WriteString(LastFileNameOffset, value, Encoding.UTF8, 256);
    }

    internal const uint IsSundayDriveOffset = LastFileNameOffset + 256;
    public bool IsSundayDrive
    {
        get => ReadBoolean(IsSundayDriveOffset);
        set => WriteBoolean(IsSundayDriveOffset, value);
    }

    internal const uint ResettingOffset = IsSundayDriveOffset + 1;
    public bool Resetting
    {
        get => ReadBoolean(ResettingOffset);
        set => WriteBoolean(ResettingOffset, value);
    }

    internal const uint HAHACKOffset = ResettingOffset + 1;
    public bool HAHACK
    {
        get => ReadBoolean(HAHACKOffset);
        set => WriteBoolean(HAHACKOffset, value);
    }

    internal const uint CollectionEffectOffset = HAHACKOffset + 2;
    public AnimatedIcon CollectionEffect => Memory.ClassFactory.Create<AnimatedIcon>(ReadUInt32(CollectionEffectOffset));
}
