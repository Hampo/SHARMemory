namespace SHARMemory.SHAR.Structs
{
    public struct Smoother
    {
        public float RollingAverage { get; set; }

        public float Factor { get; set; }

        public Smoother(float rollingAverage, float factor)
        {
            RollingAverage = rollingAverage;
            Factor = factor;
        }
    }
}
