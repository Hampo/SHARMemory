using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVPhysicsJointInertialEffector@sim@@")]
    public class PhysicsJointInertialEffector : PoseDriver
    {
        public PhysicsJointInertialEffector(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public PhysicsJoint PhysicsJoint => Memory.ClassFactory.Create<PhysicsJoint>(ReadUInt32(20));

        public Vector3 PreviousSpeed
        {
            get => ReadStruct<Vector3>(24);
            set => WriteStruct(24, value);
        }

        public float DT
        {
            get => ReadSingle(36);
            set => WriteSingle(36, value);
        }

        public float InertialSpeedRate
        {
            get => ReadSingle(40);
            set => WriteSingle(40, value);
        }

        public float InertialAccelRate
        {
            get => ReadSingle(44);
            set => WriteSingle(44, value);
        }

        public float GravityRate
        {
            get => ReadSingle(48);
            set => WriteSingle(48, value);
        }

        public float CentrifugalRate
        {
            get => ReadSingle(52);
            set => WriteSingle(52, value);
        }
    }
}
