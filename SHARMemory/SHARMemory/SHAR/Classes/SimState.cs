using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVSimState@sim@@")]
    public class SimState : Class
    {
        public enum SimControlEnum
        {
            AICtrl = 0,
            SimulationCtrl
        }

        public SimState(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public int AIRefIndex
        {
            get => ReadInt32(12);
            set => WriteInt32(12, value);
        }

        public Matrix4x4 Transform
        {
            get => ReadStruct<Matrix4x4>(16);
            set => WriteStruct(16, value);
        }

        public SimVelocityState VelocityState
        {
            get => ReadStruct<SimVelocityState>(80);
            set => WriteStruct(80, value);
        }

        public float Scale
        {
            get => ReadSingle(104);
            set => WriteSingle(104, value);
        }

        public SimControlEnum Control
        {
            get => (SimControlEnum)ReadUInt32(108);
            set => WriteUInt32(108, (uint)value);
        }

        public SimulatedObject SimulatedObject => Memory.ClassFactory.Create<SimulatedObject>(ReadUInt32(112));

        public PhysicsObject PhysicsObject => Memory.ClassFactory.Create<PhysicsObject>(ReadUInt32(112));

        // TODO: CollisionObject (116)

        // TODO: VirtualCM (120)

        public bool ObjectMoving
        {
            get => ReadBoolean(132);
            set => WriteBoolean(132, value);
        }

        public float SafeTimeBeforeCollision
        {
            get => ReadSingle(136);
            set => WriteSingle(136, value);
        }

        public float ApproxSpeedMagnitude
        {
            get => ReadSingle(140);
            set => WriteSingle(140, value);
        }

        public bool Articulated
        {
            get => ReadBoolean(144);
            set => WriteBoolean(144, value);
        }
    }
}
