using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    public class PhysicsObject : SimulatedObject
    {
        public PhysicsObject(Memory memory, uint address) : base(memory, address) { }

        public Vector3 AngularMomentum
        {
            get => ReadVector3(188);
            set => WriteVector3(188, value);
        }

        public Matrix3x2 RelativeInertiaMatrix
        {
            get => ReadMatrix3x2(220);
            set => WriteMatrix3x2(220, value);
        }

        public Matrix3x2 AbsoluteInertiaMatrix
        {
            get => ReadMatrix3x2(248);
            set => WriteMatrix3x2(248, value);
        }
    }
}
