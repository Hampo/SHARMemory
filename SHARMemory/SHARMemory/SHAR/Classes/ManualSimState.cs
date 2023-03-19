using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVManualSimState@sim@@")]
    public class ManualSimState : SimState
    {
        public ManualSimState(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO
    }
}
