namespace SHARMemory.SHAR.Structs
{
    public struct Matrix3x2
    {
        public float M11 { get; set; }
        public float M12 { get; set; }

        public float M21 { get; set; }
        public float M22 { get; set; }

        public float M31 { get; set; }
        public float M32 { get; set; }

        public Matrix3x2(float M11, float M12, float M21, float M22, float M31, float M32)
        {
            this.M11 = M11;
            this.M12 = M12;

            this.M21 = M21;
            this.M22 = M22;

            this.M31 = M31;
            this.M32 = M32;
        }
    }
}