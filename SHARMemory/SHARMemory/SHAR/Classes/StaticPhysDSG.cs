using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVStaticPhysDSG@@")]
    public class StaticPhysDSG : CollisionEntityDSG
    {
        public StaticPhysDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Box3D BBox
        {
            get => ReadStruct<Box3D>(60);
            set => WriteStruct(60, value);
        }

        public Sphere Sphere
        {
            get => ReadStruct<Sphere>(84);
            set => WriteStruct(84, value);
        }

        public Vector3 Position
        {
            get => ReadStruct<Vector3>(100);
            set => WriteStruct(100, value);
        }

        public SimState SimState => Memory.ClassFactory.Create<SimState>(ReadUInt32(112));

        // TODO: Drawable Shadow (116)

        public Matrix4x4 ShadowMatrix
        {
            get => ReadStruct<Matrix4x4>(120);
            set => WriteStruct(120, value);
        }
    }
}
