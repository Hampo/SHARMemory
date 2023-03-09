using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class SimVelocityState
    {
        public Vector3 Linear { get ; set; }

        public Vector3 Angular { get; set; }

        public SimVelocityState(Vector3 linear, Vector3 angular)
        {
            Linear = linear;
            Angular = angular;
        }
    }
}
