using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVTriggerVolume@@")]
    public class TriggerVolume : IEntityDSG
    {
        public TriggerVolume(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO
    }
}
