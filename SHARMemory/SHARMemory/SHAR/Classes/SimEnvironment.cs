using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVSimEnvironment@sim@@")]
    public class SimEnvironment : Class
    {
        public SimEnvironment(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO
    }
}
