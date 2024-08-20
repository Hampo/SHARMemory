using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Text;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVMissionManager@@")]
public class MissionManager : GameplayManager
{
    public MissionManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public string LastFileName
    {
        get => ReadString(5848, Encoding.UTF8, 256);
        set => WriteString(5848, value, Encoding.UTF8, 256);
    }

    public bool IsSundayDrive
    {
        get => ReadBoolean(6104);
        set => WriteBoolean(6104, value);
    }

    public bool Resetting
    {
        get => ReadBoolean(6105);
        set => WriteBoolean(6105, value);
    }

    public bool HAHACK
    {
        get => ReadBoolean(6106);
        set => WriteBoolean(6106, value);
    }
}
