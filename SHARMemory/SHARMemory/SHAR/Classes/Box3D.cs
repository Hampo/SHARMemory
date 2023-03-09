using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class Box3D
    {
        public Vector3 Low { get ; set; }

        public Vector3 High { get; set; }

        public Vector3 Mid
        {
            get
            {
                Vector3 Mid = Vector3.Add(Low, High);
                Mid *= .5f;
                return Mid;
            }
        }

        public Box3D(Vector3 low, Vector3 high)
        {
            Low = low;
            High = high;
        }
    }
}
