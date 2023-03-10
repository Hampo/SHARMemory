namespace SHARMemory.SHAR.Structs
{
    public struct Sphere
    {
        public Vector3 Centre { get; set; }

        public float Radius { get; set; }

        public Sphere(Vector3 centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }
    }
}
