using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVInteriorManager@@")]
public class InteriorManager : Class
{
    public enum InteriorStates : uint
    {
        None,
        Enter,
        Exit,
        Inside
    }

    public InteriorManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public InteriorStates InteriorState => (InteriorStates)ReadUInt32(4);
}
