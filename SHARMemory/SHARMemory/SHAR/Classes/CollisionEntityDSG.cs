namespace SHARMemory.SHAR.Classes
{
    public class CollisionEntityDSG : IEntityDSG
    {
        public CollisionEntityDSG(Memory memory, uint address) : base(memory, address) { }

        public short PersistentObjectID
        {
            get => ReadInt16(40);
            set => WriteInt16(40, value);
        }

        public uint LastUpdate
        {
            get => ReadUInt32(44);
            set => WriteUInt32(44, value);
        }

        public CollisionAttributes CollisionAttributes => Memory.CreateClass<CollisionAttributes>(ReadUInt32(48));

        public bool WasParticleEffectTriggered
        {
            get => ReadBoolean(52);
            set => WriteBoolean(52, value);
        }

        public RenderEnums.LayerEnum RenderLayer
        {
            get => (RenderEnums.LayerEnum)ReadUInt32(56);
            set => WriteUInt32(56, (uint)value);
        }
    }
}
