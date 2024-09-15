using System;

namespace SHARMemory.SHAR;

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

        public static float Clamp(float x, float min, float max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static int Clamp(int x, int min, int max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static uint Clamp(uint x, uint min, uint max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }
    }
}
