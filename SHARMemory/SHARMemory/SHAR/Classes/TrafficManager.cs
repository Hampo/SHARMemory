using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVTrafficManager@@")]
public class TrafficManager : Class
{
    public TrafficManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public bool TrafficEnabled
    {
        get => ReadBoolean(100);
        set => WriteBoolean(100, value);
    }
}
