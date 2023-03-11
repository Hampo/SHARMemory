using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class GeometryVehicle : Class
    {
        public GeometryVehicle(Memory memory, uint address) : base(memory, address) { }

        public TrafficBodyDrawable TrafficBodyDrawable => new(Memory, ReadUInt32(292));

        public TrafficBodyDrawable TrafficDoorDrawable => new(Memory, ReadUInt32(296));

        /// <summary>
        /// Sets the traffic body colour if applicable.
        /// </summary>
        /// <param name="Colour">
        /// The colour to set.
        /// </param>
        public void SetTrafficBodyColour(Color Colour)
        {
            TrafficBodyDrawable trafficBodyDrawable = TrafficBodyDrawable;
            if (trafficBodyDrawable.IsAddressValid)
                trafficBodyDrawable.DesiredColour = new(Colour);

            TrafficBodyDrawable trafficDoorDrawable = TrafficDoorDrawable;
            if (trafficDoorDrawable.IsAddressValid)
                trafficDoorDrawable.DesiredColour = new(Colour);
        }
    }
}
