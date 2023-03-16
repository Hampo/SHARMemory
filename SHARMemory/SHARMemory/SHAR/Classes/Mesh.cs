using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtGeometry@@")]
    public class Mesh : Drawable
    {
        public Mesh(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Box3D Box
        {
            get => ReadStruct<Box3D>(16);
            set => WriteStruct(16, value);
        }

        public Sphere Sphere
        {
            get => ReadStruct<Sphere>(40);
            set => WriteStruct(40, value);
        }

        public int CastsShadow
        {
            get => ReadInt32(56);
            set => WriteInt32(56, value);
        }

        public PointerArray<PrimGroup> PrimGroups => PointerArrayExtensions.FromPtrArray<PrimGroup>(Memory, this, 60);
    }
}
