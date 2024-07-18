using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVCollisionEntityDSG@@")]
    public class CollisionEntityDSG : IEntityDSG
    {
        public CollisionEntityDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        internal const uint PersistentObjectIDOffset = SpatialNodeOffset + sizeof(uint);
        public short PersistentObjectID
        {
            get => ReadInt16(PersistentObjectIDOffset);
            set => WriteInt16(PersistentObjectIDOffset, value);
        }

        internal const uint LastUpdateOffset = PersistentObjectIDOffset + sizeof(short) + 2; // Padding
        public uint LastUpdate
        {
            get => ReadUInt32(LastUpdateOffset);
            set => WriteUInt32(LastUpdateOffset, value);
        }

        internal const uint CollisionAttributesOffset = LastUpdateOffset + sizeof(uint);
        public CollisionAttributes CollisionAttributes => Memory.ClassFactory.Create<CollisionAttributes>(ReadUInt32(CollisionAttributesOffset));

        internal const uint WasParticleEffectTriggeredOffset = CollisionAttributesOffset + sizeof(uint);
        public bool WasParticleEffectTriggered
        {
            get => ReadBoolean(WasParticleEffectTriggeredOffset);
            set => WriteBoolean(WasParticleEffectTriggeredOffset, value);
        }

        internal const uint RenderLayerOffset = WasParticleEffectTriggeredOffset + 4; // Padding
        public Globals.RenderEnums.LayerEnum RenderLayer
        {
            get => (Globals.RenderEnums.LayerEnum)ReadUInt32(RenderLayerOffset);
            set => WriteUInt32(RenderLayerOffset, (uint)value);
        }
    }
}
