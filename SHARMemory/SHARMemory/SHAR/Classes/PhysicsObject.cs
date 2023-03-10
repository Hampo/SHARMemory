using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    public class PhysicsObject : SimulatedObject
    {
        public PhysicsObject(Memory memory, uint address) : base(memory, address) { }

        public Vector3 AngularMomentum
        {
            get => ReadStruct<Vector3>(188);
            set => WriteStruct(188, value);
        }

        public Matrix3x2 RelativeInertiaMatrix
        {
            get => ReadStruct<Matrix3x2>(220);
            set => WriteStruct(220, value);
        }

        public Matrix3x2 AbsoluteInertiaMatrix
        {
            get => ReadStruct<Matrix3x2>(248);
            set => WriteStruct(248, value);
        }
    }
}
