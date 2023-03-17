using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVRenderFlow@@")]
    public class RenderFlow : Class
    {
        public RenderFlow(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public RenderManager RenderManager => Memory.ClassFactory.Create<RenderManager>(ReadUInt32(16));
    }
}
