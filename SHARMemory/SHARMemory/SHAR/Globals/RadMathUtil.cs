using System;

namespace SHARMemory.SHAR
{
    public partial class Globals
    {
        public static class RadMathUtil
        {
            public static float Fabs(float a)
            {
                uint x = BitConverter.ToUInt32(BitConverter.GetBytes(a), 0) & 0x7fffffff; // strip off bit 31
                return BitConverter.ToSingle(BitConverter.GetBytes(x), 0);
            }

            public static bool Epsilon(float x, float n, float epsilon = 0.000001f) => (x >= -epsilon + n) && (x <= epsilon + n);
        }
    }
}
