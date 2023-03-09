using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class StaticPhysDSG : CollisionEntityDSG
    {
        public StaticPhysDSG(Memory memory, uint address) : base(memory, address) { }

        public Box3D BBox
        {
            get => ReadBox3D(60);
            set => WriteBox3D(60, value);
        }

        public Sphere Sphere
        {
            get => ReadSphere(84);
            set => WriteSphere(84, value);
        }

        public Vector3 Position
        {
            get => ReadVector3(100);
            set => WriteVector3(100, value);
        }

        public SimState SimState => new SimState(Memory, ReadUInt32(112));

        // TODO: Drawable Shadow (116)

        public Matrix4x4 ShadowMatrix
        {
            get => ReadMatrix4x4(120);
            set => WriteMatrix4x4(120, value);
        }
    }
}
