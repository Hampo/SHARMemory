using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Classes
{
    public class Smoother
    {
        public float RollingAverage { get ; set; }

        public float Factor { get; set; }

        public Smoother(float rollingAverage, float factor)
        {
            RollingAverage = rollingAverage;
            Factor = factor;
        }
    }
}
