using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVRenderManager@@")]
    public class RenderManager : Class
    {
        public RenderManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public MoodLighting Mood
        {
            get => ReadStruct<MoodLighting>(112);
            set => WriteStruct(112, value);
        }
    }
}
