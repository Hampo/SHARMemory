using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVIEntityDSG@@")]
    public class IEntityDSG : Drawable
    {
        public IEntityDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public float Rank
        {
            get => ReadSingle(0);
            set => WriteSingle(0, value);
        }

        public bool Translucent
        {
            get => ReadBoolean(4);
            set => WriteBoolean(4, value);
        }

        // TODO: Implement the name bullshit for shader name (8)

        // TODO: SpatialNode? (12)
    }
}
