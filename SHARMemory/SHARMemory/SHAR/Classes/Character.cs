namespace SHARMemory.SHAR.Classes
{
    public class Character : DynaPhysDSG
    {
        public Character(Memory memory, uint address) : base(memory, address) { }

        public float Rotation => ReadSingle(272);

        public Vehicle Car => new Vehicle(Memory, ReadUInt32(348));

        public CharacterController Controller => new CharacterController(Memory, ReadUInt32(256u));
    }
}
