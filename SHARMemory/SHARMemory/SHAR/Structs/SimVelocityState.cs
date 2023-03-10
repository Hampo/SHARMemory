namespace SHARMemory.SHAR.Structs
{
    public struct SimVelocityState
    {
        public Vector3 Linear { get; set; }

        public Vector3 Angular { get; set; }

        public SimVelocityState(Vector3 linear, Vector3 angular)
        {
            Linear = linear;
            Angular = angular;
        }
    }
}
