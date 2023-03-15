using SHARMemory.Memory;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class GeometryVehicle : Class
    {
        public GeometryVehicle(Memory memory, uint address) : base(memory, address) { }

        public Vehicle Vehicle => Memory.CreateClass<Vehicle>(ReadUInt32(0));

        public CompositeDrawable CompositeDrawable => Memory.CreateClass<CompositeDrawable>(ReadUInt32(4));

        public PointerArray<Shader> RefractionShaders => new(Memory, Address + 72, 16);

        public Shader HoodShader => Memory.CreateClass<Shader>(ReadUInt32(76));

        public Shader TrunkShader => Memory.CreateClass<Shader>(ReadUInt32(80));

        public Shader DoorPShader => Memory.CreateClass<Shader>(ReadUInt32(84));

        public Shader DoorDShader => Memory.CreateClass<Shader>(ReadUInt32(88));

        public Shader ChassisShader => Memory.CreateClass<Shader>(ReadUInt32(124));

        public Shader RoofShader => Memory.CreateClass<Shader>(ReadUInt32(480));

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
