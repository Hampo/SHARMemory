using SHARMemory.SHAR.Structs;
using System.Drawing;
using System.Text;

namespace SHARMemory.SHAR.Classes
{
    public class Vehicle : DynaPhysDSG
    {
        public enum VehicleTypes
        {
            User,
            AI,
            Traffic,
            Last
        }

        public enum VehicleStates
        {
            Normal,
            Slip,
            EBrake_Slip
        }

        public enum VehicleLocomotionTypes
        {
            Physics,
            Traffic
        }

        public Vehicle(Memory memory, uint address) : base(memory, address) { }

        public bool AlreadyCalledAutoResetOnSpot
        {
            get => ReadBoolean(160);
            set => WriteBoolean(160, value);
        }

        public VehicleTypes VehicleType
        {
            get => (VehicleTypes)ReadUInt32(164);
            set => WriteUInt32(164, (uint)value);
        }

        public int VehicleCentralIndex
        {
            get => ReadInt32(168);
            set => WriteInt32(168, value);
        }

        public float SecondsTillNextTurbo
        {
            get => ReadSingle(172);
            set => WriteSingle(172, value);
        }

        public int NumTurbos
        {
            get => ReadInt32(176);
            set => WriteInt32(176, value);
        }

        public GeometryVehicle GeometryVehicle => Memory.CreateClass<GeometryVehicle>(ReadUInt32(180));

        public Matrix4x4 Transform => ReadStruct<Matrix4x4>(184);

        public string Name => ReadNullString(248, Encoding.UTF8);

        public Vector3 InitialPosition
        {
            get => ReadStruct<Vector3>(252);
            set => WriteStruct(252, value);
        }

        public float ResetFacingRadians
        {
            get => ReadSingle(264);
            set => WriteSingle(264, value);
        }

        public uint VehicleID
        {
            get => ReadUInt32(268);
            set => WriteUInt32(268, value);
        }

        public VehicleEventListener VehicleEventListener => Memory.CreateClass<VehicleEventListener>(ReadUInt32(272));

        public bool DoingJumpBoost
        {
            get => ReadBoolean(276);
            set => WriteBoolean(276, value);
        }

        public Vector3 VehicleFacing
        {
            get => ReadStruct<Vector3>(280);
            set => WriteStruct(280, value);
        }

        public Vector3 VehicleUp
        {
            get => ReadStruct<Vector3>(292);
            set => WriteStruct(292, value);
        }

        public Vector3 VehicleTransverse
        {
            get => ReadStruct<Vector3>(304);
            set => WriteStruct(304, value);
        }

        public Vector3 VelocityCM
        {
            get => ReadStruct<Vector3>(316);
            set => WriteStruct(316, value);
        }

        public float Speed
        {
            get => ReadSingle(328);
            set => WriteSingle(328, value);
        }

        public float PercentOfTopSpeed
        {
            get => ReadSingle(332);
            set => WriteSingle(332, value);
        }

        public float SpeedKmh
        {
            get => ReadSingle(336);
            set => WriteSingle(336, value);
        }

        public float LastSpeedKmh
        {
            get => ReadSingle(340);
            set => WriteSingle(340, value);
        }

        public float AccelMss
        {
            get => ReadSingle(344);
            set => WriteSingle(344, value);
        }

        public Vector3 OriginalCMOffset
        {
            get => ReadStruct<Vector3>(348);
            set => WriteStruct(348, value);
        }

        public Vector3 CMOffset
        {
            get => ReadStruct<Vector3>(360);
            set => WriteStruct(360, value);
        }

        public Pointers.IntersectManager.TerrainType TerrainType
        {
            get => (Pointers.IntersectManager.TerrainType)ReadUInt32(372);
            set => WriteUInt32(372, (uint)value);
        }

        public bool Interior
        {
            get => ReadBoolean(376);
            set => WriteBoolean(376, value);
        }

        public VehicleStates VehicleState
        {
            get => (VehicleStates)ReadUInt32(380);
            set => WriteUInt32(380, (uint)value);
        }

        public VehicleLocomotion VehicleLocomotion => Memory.CreateClass<VehicleLocomotion>(ReadUInt32(384));

        public VehicleLocomotionTypes VehicleLocomotionType
        {
            get => (VehicleLocomotionTypes)ReadUInt32(388);
            set => WriteUInt32(388, (uint)value);
        }

        public float Mass => ReadSingle(696);

        public PointerArray<Wheel> Wheels => new(Memory, Address + 1044, 4);

        public SimStateArticulated SimStateArticulated => Memory.CreateClass<SimStateArticulated>(ReadUInt32(1192));

        public ArticulatedPhysicsObject ArticulatedPhysicsObject => Memory.CreateClass<ArticulatedPhysicsObject>(ReadUInt32(1196));

        /// <summary>
        /// Stops all vehicle momentum.
        /// </summary>
        public void Stop()
        {
            SimStateArticulated simStateArticulated = SimStateArticulated;
            if (simStateArticulated == null)
                return;

            SimVelocityState simVelocityState = simStateArticulated.VelocityState;
            simVelocityState.Linear = new(0);
            simVelocityState.Angular = new(0);
            simStateArticulated.VelocityState = simVelocityState;
            simStateArticulated.PhysicsObject.AngularMomentum = new(0);
        }

        /// <summary>
        /// Launches the vehicle upwards and in the direction it's facing
        /// </summary>
        /// <param name="Height">
        /// How high to send the vehicle. Defaults to <c>20</c>.
        /// </param>
        /// <param name="LaunchPower">
        /// How hard to launch the vehicle forwards. Defaults to <c>5</c>.
        /// </param>
        public void Launch(float Height = 20, float LaunchPower = 5)
        {
            SimStateArticulated simStateArticulated = SimStateArticulated;
            if (simStateArticulated == null)
                return;
            
            SimVelocityState simVelocityState = simStateArticulated.VelocityState;
            Vector3 linear = Vector3.Add(simVelocityState.Linear, VehicleFacing * LaunchPower);
            linear.Y += Height;
            simVelocityState.Linear = linear;
            simStateArticulated.VelocityState = simVelocityState;
        }

        /// <summary>
        /// Sets the traffic body colour if applicable.
        /// </summary>
        /// <param name="Colour">
        /// The colour to set.
        /// </param>
        public void SetTrafficBodyColour(Color Colour) => GeometryVehicle?.SetTrafficBodyColour(Colour);
    }
}
