using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        public CollisionAttributes CollisionAttributes => new CollisionAttributes(Memory, ReadUInt32(48));

        public bool WasParticleEffectTriggered
        {
            get => ReadByte(52) != 0;
            set => WriteByte(52, (byte)(value ? 1 : 0));
        }

        public RenderEnums.LayerEnum RenderLayer
        {
            get => (RenderEnums.LayerEnum)ReadUInt32(56);
            set => WriteUInt32(56, (uint)value);
        }
    }
}
