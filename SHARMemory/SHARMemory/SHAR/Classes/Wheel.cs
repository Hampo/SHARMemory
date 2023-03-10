namespace SHARMemory.SHAR.Classes
{
    public class Wheel : Class
    {
        public Wheel(Memory memory, uint address) : base(memory, address) { }

        public Vehicle Car => new Vehicle(Memory, ReadUInt32(0));

        public bool IsSteerWheel
        {
            get => ReadByte(4) != 0;
            set => WriteByte(4, (byte)(value ? 1 : 0));
        }

        public bool IsDriveWheel
        {
            get => ReadByte(5) != 0;
            set => WriteByte(5, (byte)(value ? 1 : 0));
        }

        public int Number
        {
            get => ReadInt32(8);
            set => WriteInt32(8, value);
        }

        public float Radius
        {
            get => ReadSingle(12);
            set => WriteSingle(12, value);
        }

        public float ObjectSpaceYOffsetFromCurrentPosition
        {
            get => ReadSingle(16);
            set => WriteSingle(16, value);
        }

        public float YOffset
        {
            get => ReadSingle(20);
            set => WriteSingle(20, value);
        }

        public float RotAngle
        {
            get => ReadSingle(28);
            set => WriteSingle(28, value);
        }

        public float CumulativeRot
        {
            get => ReadSingle(32);
            set => WriteSingle(32, value);
        }

        public float TotalTime
        {
            get => ReadSingle(36);
            set => WriteSingle(36, value);
        }

        public float Limit
        {
            get => ReadSingle(40);
            set => WriteSingle(40, value);
        }

        public float SpringConstant
        {
            get => ReadSingle(44);
            set => WriteSingle(44, value);
        }

        public float DamperConstant
        {
            get => ReadSingle(48);
            set => WriteSingle(48, value);
        }

        public bool InCollision
        {
            get => ReadByte(52) != 0;
            set => WriteByte(52, (byte)(value ? 1 : 0));
        }

        public bool BottomedOutThisFrame
        {
            get => ReadByte(53) != 0;
            set => WriteByte(53, (byte)(value ? 1 : 0));
        }

        public float SpringRelaxRate
        {
            get => ReadSingle(56);
            set => WriteSingle(56, value);
        }

        public float TurnAngle
        {
            get => ReadSingle(60);
            set => WriteSingle(60, value);
        }
    }
}
