namespace SHARMemory.SHAR.Classes
{
    public class Character : DynaPhysDSG
    {
        public Character(Memory memory, uint address) : base(memory, address) { }

        public CharacterController Controller => Memory.CreateClass<CharacterController>(ReadUInt32(256));

        public CharacterRenderable CharacterRenderable => Memory.CreateClass<CharacterRenderable>(ReadUInt32(260));

        public float Rotation => ReadSingle(272);

        public Vehicle Car
        {
            get => Memory.CreateClass<Vehicle>(ReadUInt32(348));
            set => WriteUInt32(348, value == null ? 0 : value.Address);
        }
        
    }
}
