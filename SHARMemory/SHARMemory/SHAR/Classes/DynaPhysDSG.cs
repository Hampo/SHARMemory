using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class DynaPhysDSG : StaticPhysDSG
    {
        public DynaPhysDSG(Memory memory, uint address) : base(memory, address) { }

        public bool IsHit
        {
            get => ReadByte(124) != 0;
            set => WriteByte(124, (byte)(value ? 1 : 0));
        }

        public Smoother PastLinear
        {
            get => ReadSmoother(128);
            set => WriteSmoother(128, value);
        }

        public Smoother PastAngular
        {
            get => ReadSmoother(136);
            set => WriteSmoother(136, value);
        }

        public int GroundPlaneIndex
        {
            get => ReadInt32(144);
            set => WriteInt32(144, value);
        }

        public int GroundPlaneRefs
        {
            get => ReadInt32(148);
            set => WriteInt32(148, value);
        }
    }
}
