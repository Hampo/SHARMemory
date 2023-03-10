namespace SHARMemory.SHAR.Classes
{
    public class CharacterController : Class
    {
        public enum Intentions
        {
            None,
            LeftStickX,
            LeftStickY,
            DoAction,
            Jump,
            Dash,
            Attack,
            DPadUp,
            DPadDown,
            DPadLeft,
            DPadRight,
            GetOutCar,
            MouseLookLeft,
            MouseLookRight,
            NUM_INPUTS,
            Dodge,
            Cringe,
            TurnRight,
            TurnLeft,
            CelebrateSmall,
            CelebrateBig,
            WaveHello,
            WaveGoodbye
        }

        public CharacterController(Memory memory, uint address) : base(memory, address) { }

        public Intentions Intention
        {
            get => (Intentions)ReadUInt32(12);
            set => WriteUInt32(12, (uint)value);
        }

        public Intentions PreserveIntention
        {
            get => (Intentions)ReadUInt32(16);
            set => WriteUInt32(16, (uint)value);
        }

        public bool Active
        {
            get => ReadByte(20) != 0;
            set => WriteByte(20, (byte)(value ? 1 : 0));
        }
    }
}
