using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class IEntityDSG : Class
    {
        public IEntityDSG(Memory memory, uint address) : base(memory, address) { }

        public float Rank
        {
            get => ReadSingle(0);
            set => WriteSingle(0, value);
        }

        public bool Translucent
        {
            get => ReadByte(4) != 0;
            set => WriteByte(4, (byte)(value ? 1 : 0));
        }

        // TODO: Implement the name bullshit for shader name (8)

        // TODO: SpatialNode? (12)
    }
}
