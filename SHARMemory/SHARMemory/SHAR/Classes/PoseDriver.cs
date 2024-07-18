using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVPoseDriver@poser@@")]
public class PoseDriver : Class
{
    public PoseDriver(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public bool Enabled
    {
        get => ReadBoolean(16);
        set => WriteBoolean(16, value);
    }
}
