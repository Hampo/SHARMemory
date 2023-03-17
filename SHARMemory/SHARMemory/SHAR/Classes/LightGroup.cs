using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtLightGroup@@")]
    public class LightGroup : Class
    {
        public LightGroup(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public int NumLights
        {
            get => ReadInt32(16);
            set => WriteInt32(16, value);
        }

        public int CurNumLights
        {
            get => ReadInt32(20);
            set => WriteInt32(20, value);
        }

        public PointerArray<Light> Lights => PointerArrayExtensions.FromPtrArray<Light>(Memory, this, 24);
    }
}
