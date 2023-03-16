using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class GeometryVehicle : Class
    {
        public GeometryVehicle(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Vehicle Vehicle => Memory.ClassFactory.Create<Vehicle>(ReadUInt32(0));

        public CompositeDrawable CompositeDrawable => Memory.ClassFactory.Create<CompositeDrawable>(ReadUInt32(4));

        public PointerArray<Shader> RefractionShaders => new(Memory, Address + 72, 16);

        public Shader HoodShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(76));

        public Shader TrunkShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(80));

        public Shader DoorPShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(84));

        public Shader DoorDShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(88));

        public Shader ChassisShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(124));

        public Shader RoofShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(480));

        public TrafficBodyDrawable TrafficBodyDrawable => Memory.ClassFactory.Create<TrafficBodyDrawable>(ReadUInt32(292));

        public TrafficBodyDrawable TrafficDoorDrawable => Memory.ClassFactory.Create<TrafficBodyDrawable>(ReadUInt32(296));

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
                trafficBodyDrawable.DesiredColour = Colour;

            TrafficBodyDrawable trafficDoorDrawable = TrafficDoorDrawable;
            if (trafficDoorDrawable != null)
                trafficDoorDrawable.DesiredColour = Colour;
        }
    }
}
