using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class ArticulatedPhysicsObject : PhysicsObject
    {
        public ArticulatedPhysicsObject(Memory memory, uint address) : base(memory, address) { }

        public float DissipationDeformationRate
        {
            get => ReadSingle(484);
            set => WriteSingle(484, value);
        }

        public float DissipationDeformationSpeedRate
        {
            get => ReadSingle(488);
            set => WriteSingle(488, value);
        }

        public float DissipationInternalRate
        {
            get => ReadSingle(492);
            set => WriteSingle(492, value);
        }
    }
}
