using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    public class SimState : Class
    {
        public enum SimControlEnum
        {
            AICtrl = 0,
            SimulationCtrl
        }

        public SimState(Memory memory, uint address) : base(memory, address) { }

        public int AIRefIndex
        {
            get => ReadInt32(12);
            set => WriteInt32(12, value);
        }

        public Matrix4x4 Transform
        {
            get => ReadMatrix4x4(16);
            set => WriteMatrix4x4(16, value);
        }

        public SimVelocityState VelocityState
        {
            get => ReadSimVelocityState(80);
            set => WriteSimVelocityState(80, value);
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

        // TODO: SimulatedObject (112)

        // TODO: CollisionObject (116)

        // TODO: VirtualCM (120)

        public bool ObjectMoving
        {
            get => ReadByte(132) != 0;
            set => WriteByte(132, (byte)(value ? 1 : 0));
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
            get => ReadByte(144) != 0;
            set => WriteByte(144, (byte)(value ? 1 : 0));
        }
    }
}
