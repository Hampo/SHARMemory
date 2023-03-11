namespace SHARMemory.SHAR.Classes
{
    public class Character : DynaPhysDSG
    {
        public Character(Memory memory, uint address) : base(memory, address) { }

        public float Rotation => ReadSingle(272);

        public Vehicle Car => Memory.CreateClass<Vehicle>(ReadUInt32(348));

        public CharacterController Controller => Memory.CreateClass<CharacterController>(ReadUInt32(256u));
    }
}
