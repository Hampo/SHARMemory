using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVPhysicsObject@sim@@")]
    public class PhysicsObject : SimulatedObject
    {
        public enum AxisOfRevolutionEnum
        {
            AboutX,
            AboutY,
            AboutZ,
            One,
            Three,
            None
        }

        public PhysicsObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public float InitialVolume
        {
            get => ReadSingle(52);
            set => WriteSingle(52, value);
        }

        public float Volume
        {
            get => ReadSingle(56);
            set => WriteSingle(56, value);
        }

        public float Mass
        {
            get => ReadSingle(60);
            set => WriteSingle(60, value);
        }

        public float InverseMass
        {
            get => ReadSingle(64);
            set => WriteSingle(64, value);
        }

        public AxisOfRevolutionEnum AxisOfRevolution
        {
            get => (AxisOfRevolutionEnum)ReadInt32(68);
            set => WriteInt32(68, (int)value);
        }

        public Vector3 GeometryRotationAxis
        {
            get => ReadStruct<Vector3>(72);
            set => WriteStruct(72, value);
        }

        public Matrix4x4 RotationMatrix
        {
            get => ReadStruct<Matrix4x4>(84);
            set => WriteStruct(84, value);
        }

        public Vector3 CMPosition
        {
            get => ReadStruct<Vector3>(148);
            set => WriteStruct(148, value);
        }

        public bool CMOffsetEmpty
        {
            get => ReadBoolean(160);
            set => WriteBoolean(160, value);
        }

        public Vector3 CMOffset
        {
            get => ReadStruct<Vector3>(164);
            set => WriteStruct(164, value);
        }

        public Vector3 ExternalCMOffset
        {
            get => ReadStruct<Vector3>(176);
            set => WriteStruct(176, value);
        }

        public Vector3 AngularMomentum
        {
            get => ReadStruct<Vector3>(188);
            set => WriteStruct(188, value);
        }

        public Quaternion Q
        {
            get => ReadStruct<Quaternion>(200);
            set => WriteStruct(200, value);
        }

        public SymMatrix InitialInertiaMatrix => Memory.ClassFactory.Create<SymMatrix>(Address + 216);

        public SymMatrix InertiaMatrix => Memory.ClassFactory.Create<SymMatrix>(Address + 244);

        public SymMatrix InverseInertiaMatrix => Memory.ClassFactory.Create<SymMatrix>(Address + 252);
    }
}
