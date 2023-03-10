using SHARMemory.SHAR.Structs;
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

        public GeometryVehicle GeometryVehicle => new GeometryVehicle(Memory, ReadUInt32(180));

        public Matrix4x4 Transform => ReadMatrix4x4(184);

        public string Name => ReadNullString(248, Encoding.UTF8);

        public Vector3 InitialPosition
        {
            get => ReadVector3(252);
            set => WriteVector3(252, value);
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

        public VehicleEventListener VehicleEventListener => new VehicleEventListener(Memory, ReadUInt32(272));

        public bool DoingJumpBoost
        {
            get => ReadBoolean(276);
            set => WriteBoolean(276, value);
        }

        public Vector3 VehicleFacing
        {
            get => ReadVector3(280);
            set => WriteVector3(280, value);
        }

        public Vector3 VehicleUp
        {
            get => ReadVector3(292);
            set => WriteVector3(292, value);
        }

        public Vector3 VehicleTransverse
        {
            get => ReadVector3(304);
            set => WriteVector3(304, value);
        }

        public Vector3 VelocityCM
        {
            get => ReadVector3(316);
            set => WriteVector3(316, value);
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
            get => ReadVector3(348);
            set => WriteVector3(348, value);
        }

        public Vector3 CMOffset
        {
            get => ReadVector3(360);
            set => WriteVector3(360, value);
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

        public VehicleLocomotion VehicleLocomotion => new VehicleLocomotion(Memory, ReadUInt32(384));

        public VehicleLocomotionTypes VehicleLocomotionType
        {
            get => (VehicleLocomotionTypes)ReadUInt32(388);
            set => WriteUInt32(388, (uint)value);
        }

        public float Mass => ReadSingle(696);

        public Wheel Wheels(uint index) => new Wheel(Memory, ReadUInt32(1044 + index * 4));

        public SimStateArticulated SimStateArticulated => new SimStateArticulated(Memory, ReadUInt32(1192));

        public ArticulatedPhysicsObject ArticulatedPhysicsObject => new ArticulatedPhysicsObject(Memory, ReadUInt32(1196));
    }
}
