namespace SHARMemory.SHAR.Classes
{
    public class GeometryVehicle : Class
    {
        public GeometryVehicle(Memory memory, uint address) : base(memory, address) { }

        // TODO
        public TrafficBodyDrawable TrafficBodyDrawable => new(Memory, ReadUInt32(292));

        public TrafficBodyDrawable TrafficDoorDrawable => new(Memory, ReadUInt32(296));
    }
}
