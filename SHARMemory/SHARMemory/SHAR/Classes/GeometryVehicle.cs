using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class GeometryVehicle : Class
    {
        public GeometryVehicle(Memory memory, uint address) : base(memory, address) { }

        public TrafficBodyDrawable TrafficBodyDrawable => Memory.CreateClass<TrafficBodyDrawable>(ReadUInt32(292));

        public TrafficBodyDrawable TrafficDoorDrawable => Memory.CreateClass<TrafficBodyDrawable>(ReadUInt32(296));

        /// <summary>
        /// Sets the traffic body colour if applicable.
        /// </summary>
        /// <param name="Colour">
        /// The colour to set.
        /// </param>
        public void SetTrafficBodyColour(Color Colour)
        {
            TrafficBodyDrawable trafficBodyDrawable = TrafficBodyDrawable;
            if (trafficBodyDrawable != null)
                trafficBodyDrawable.DesiredColour = new(Colour);

            TrafficBodyDrawable trafficDoorDrawable = TrafficDoorDrawable;
            if (trafficDoorDrawable != null)
                trafficDoorDrawable.DesiredColour = new(Colour);
        }
    }
}
