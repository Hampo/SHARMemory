using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class Sphere
    {
        public Vector3 Centre { get ; set; }

        public float Radius { get; set; }

        public Sphere(Vector3 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }
    }
}
