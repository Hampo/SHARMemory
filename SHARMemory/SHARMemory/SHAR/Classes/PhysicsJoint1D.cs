using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVPhysicsJoint1D@sim@@")]
    public class PhysicsJoint1D : PhysicsJoint
    {
        public PhysicsJoint1D(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public bool ParentRelative
        {
            get => ReadBoolean(252);
            set => WriteBoolean(252, value);
        }

        public Vector3 RotAxis
        {
            get => ReadStruct<Vector3>(256);
            set => WriteStruct(256, value);
        }

        public float DeformationSpeedCache
        {
            get => ReadSingle(268);
            set => WriteSingle(268, value);
        }

        public float DeformationSpeed
        {
            get => ReadSingle(272);
            set => WriteSingle(272, value);
        }

        public float DeformationSpeed0
        {
            get => ReadSingle(276);
            set => WriteSingle(276, value);
        }

        public float Deformation
        {
            get => ReadSingle(280);
            set => WriteSingle(280, value);
        }

        public float Deformation0
        {
            get => ReadSingle(284);
            set => WriteSingle(284, value);
        }

        public float CurrentAngle
        {
            get => ReadSingle(288);
            set => WriteSingle(288, value);
        }


        public override void ResetDeformation()
        {
            Deformation = 0;
            Deformation0 = 0;
            DeformationSpeed = 0;
            DeformationSpeed0 = 0;
        }
    }
}
