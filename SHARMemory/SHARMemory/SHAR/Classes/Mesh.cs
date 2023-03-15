using SHARMemory.Memory;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    public class Mesh : Drawable
    {
        public Mesh(Memory memory, uint address) : base(memory, address) { }

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
