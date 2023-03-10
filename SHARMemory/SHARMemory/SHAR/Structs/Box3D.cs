namespace SHARMemory.SHAR.Structs
{
    public struct Box3D
    {
        public Vector3 Low { get; set; }

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
