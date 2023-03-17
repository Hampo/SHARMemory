using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVRenderManager@@")]
    public class RenderManager : Class
    {
        public RenderManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        // TODO: Convert this offset to use a MoodLighting struct
        public LightGroup SunGroup => Memory.ClassFactory.Create<LightGroup>(ReadUInt32(112));
    }
}
