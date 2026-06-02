using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System;
using System.Drawing;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCharacter@@")]
public class Character : DynaPhysDSG
{
    public const int MAX_ACTION_BUTTON_HANDLERS = 5;

    public enum TerrainTypes
    {
        Road,
        Grass,
        Sand,
        Gravel,
        Water,
        Wood,
        Metal,
        Dirt
    }

    public enum Roles
    {
        Unknown,
        Driver,
        Reward,
        ActiveBonus,
        CompletedBonus,
        Pedestrian,
        Mission
    }

    public Character(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint CollidedWithVehicleOffset = GroundPlaneRefsOffset + sizeof(int);
    public bool CollidedWithVehicle
    {
        get => ReadBoolean(CollidedWithVehicleOffset);
        set => WriteBoolean(CollidedWithVehicleOffset, value);
    }

    internal const uint InAnyonesFustrumOffset = CollidedWithVehicleOffset + sizeof(bool);
    public bool InAnyonesFustrum
    {
        get => ReadBoolean(InAnyonesFustrumOffset);
        set => WriteBoolean(InAnyonesFustrumOffset, value);
    }

    internal const uint IsOnCarOffset = InAnyonesFustrumOffset + sizeof(bool);
    public bool IsOnCar
    {
        get => ReadBoolean(IsOnCarOffset);
        set => WriteBoolean(IsOnCarOffset, value);
    }

    internal const uint AllowUnloadOffset = IsOnCarOffset + sizeof(bool);
    public bool AllowUnload
    {
        get => ReadBoolean(AllowUnloadOffset);
        set => WriteBoolean(AllowUnloadOffset, value);
    }

    internal const uint IsPlayingIdleAnimOffset = AllowUnloadOffset + sizeof(bool);
    public bool IsPlayingIdleAnim
    {
        get => ReadBoolean(IsPlayingIdleAnimOffset);
        set => WriteBoolean(IsPlayingIdleAnimOffset, value);
    }

    internal const uint PCCamFacingOffset = IsPlayingIdleAnimOffset + 4; // Paddng
    public int PCCamFacing
    {
        get => ReadInt32(PCCamFacingOffset);
        set => WriteInt32(PCCamFacingOffset, value);
    }

    internal const uint PrevSimTransformOffset = PCCamFacingOffset + sizeof(int);
    public Matrix4x4 PrevSimTransform
    {
        get => ReadStruct<Matrix4x4>(PrevSimTransformOffset);
        set => WriteStruct(PrevSimTransformOffset, value);
    }

    internal const uint IsNPCOffset = PrevSimTransformOffset + Matrix4x4.Size;
    public bool IsNPC
    {
        get => ReadBoolean(IsNPCOffset);
        set => WriteBoolean(IsNPCOffset, value);
    }

    internal const uint GroundPlaneSimStateOffset = IsNPCOffset + 4; // Padding
    public ManualSimState GroundPaneSimState => Memory.ClassFactory.Create<ManualSimState>(ReadUInt32(GroundPlaneSimStateOffset));

    internal const uint GroundPlaneWallVolumeOffset = GroundPlaneSimStateOffset + sizeof(uint);
    public WallVolume GroundPlaneWallVolume => Memory.ClassFactory.Create<WallVolume>(ReadUInt32(GroundPlaneWallVolumeOffset));

    internal const uint CollisionAreaIndexOffset = GroundPlaneWallVolumeOffset + sizeof(uint);
    public int CollisionAreaIndex
    {
        get => ReadInt32(CollisionAreaIndexOffset);
        set => WriteInt32(CollisionAreaIndexOffset, value);
    }

    internal const uint UnknownOffset = CollisionAreaIndexOffset + sizeof(uint);

    internal const uint LastInteriorLoadCheckOffset = UnknownOffset + 4;
    public long LastInteriorLoadCheck
    {
        get => ReadInt64(LastInteriorLoadCheckOffset);
        set => WriteInt64(LastInteriorLoadCheckOffset, value);
    }

    internal const uint ControllerOffset = LastInteriorLoadCheckOffset + sizeof(long);
    public CharacterController Controller => Memory.ClassFactory.Create<CharacterController>(ReadUInt32(ControllerOffset));

    internal const uint CharacterRenderableOffset = ControllerOffset + sizeof(uint);
    public CharacterRenderable CharacterRenderable => Memory.ClassFactory.Create<CharacterRenderable>(ReadUInt32(CharacterRenderableOffset));

    internal const uint PuppetOffset = CharacterRenderableOffset + sizeof(uint);
    public SHARMemory.Memory.Class Puppet => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(PuppetOffset)); // TODO: choreo::Puppet

    internal const uint FacingDirOffset = PuppetOffset + sizeof(uint);
    public float FacingDir
    {
        get => ReadSingle(FacingDirOffset);
        set => WriteSingle(FacingDirOffset, value);
    }

    internal const uint DesiredDirOffset = FacingDirOffset + sizeof(float);
    public float DesiredDir
    {
        get => ReadSingle(DesiredDirOffset);
        set => WriteSingle(DesiredDirOffset, value);
    }

    internal const uint SpeedOffset = DesiredDirOffset + sizeof(float);
    public float Speed
    {
        get => ReadSingle(SpeedOffset);
        set => WriteSingle(SpeedOffset, value);
    }

    internal const uint VelocityOffset = SpeedOffset + sizeof(float);
    public Vector3 Velocity
    {
        get => ReadStruct<Vector3>(VelocityOffset);
        set => WriteStruct(VelocityOffset, value);
    }

    internal const uint DesiredSpeedOffset = VelocityOffset + Vector3.Size;
    public float DesiredSpeed
    {
        get => ReadSingle(DesiredSpeedOffset);
        set => WriteSingle(DesiredSpeedOffset, value);
    }

    internal const uint InCarOffset = DesiredSpeedOffset + sizeof(float);
    public bool InCar
    {
        get => ReadBoolean(InCarOffset);
        set => WriteBoolean(InCarOffset, value);
    }

    internal const uint WasFootPlantedOffset = InCarOffset + 4; // Padding
    //TODO: public StructArray<bool> WasFootPlanted => ...

    internal const uint CharacterTargetOffset = WasFootPlantedOffset + 16;
    public SHARMemory.Memory.Class CharacterTarget => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(CharacterTargetOffset)); // TODO: CharacterTarget

    internal const uint ActionControllerOffset = CharacterTargetOffset + sizeof(uint);
    public SHARMemory.Memory.Class ActionController => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(ActionControllerOffset)); // TODO: ActionController

    internal const uint ActionButtonHandlersOffset = ActionControllerOffset + sizeof(uint);
    public PointerArray<ButtonHandler> ActionButtonHandlers => new(Memory, Address + ActionButtonHandlersOffset, MAX_ACTION_BUTTON_HANDLERS);

    internal const uint CurrentActionButtonHandlerOffset = ActionButtonHandlersOffset + sizeof(uint) * MAX_ACTION_BUTTON_HANDLERS;
    public ButtonHandler CurrentActionButtonHandler => Memory.ClassFactory.Create<ButtonHandler>(ReadUInt32(CurrentActionButtonHandlerOffset));

    internal const uint TargetVehicleOffset = CurrentActionButtonHandlerOffset  + sizeof(uint);
    public Vehicle TargetVehicle => Memory.ClassFactory.Create<Vehicle>(ReadUInt32(TargetVehicleOffset));
    [Obsolete($"Use {nameof(TargetVehicle)} instead.")]
    public Vehicle Car => TargetVehicle;

    internal const uint GroundYOffset = TargetVehicleOffset + sizeof(uint);
    public float GroundY
    {
        get => ReadSingle(GroundYOffset);
        set => WriteSingle(GroundYOffset, value);
    }

    internal const uint GroundNormalOffset = GroundYOffset + sizeof(float);
    public Vector3 GroundNormal
    {
        get => ReadStruct<Vector3>(GroundNormalOffset);
        set => WriteStruct(GroundNormalOffset, value);
    }

    internal const uint TerrainTypeOffset = GroundNormalOffset + Vector3.Size;
    public TerrainTypes TerrainType
    {
        get => (TerrainTypes)ReadUInt32(TerrainTypeOffset);
        set => WriteUInt32(TerrainTypeOffset, (uint)value);
    }

    internal const uint InteriorTerrainOffset = TerrainTypeOffset + sizeof(uint);
    public bool InteriorTerrain
    {
        get => ReadBoolean(InteriorTerrainOffset);
        set => WriteBoolean(InteriorTerrainOffset, value);
    }

    internal const uint RealGroundPosOffset = InteriorTerrainOffset + 4; // Padding
    public Vector3 RealGroundPos
    {
        get => ReadStruct<Vector3>(RealGroundPosOffset);
        set => WriteStruct(RealGroundPosOffset, value);
    }

    internal const uint RealGroundNormalOffset = RealGroundPosOffset + Vector3.Size;
    public Vector3 ReadGroundNormal
    {
        get => ReadStruct<Vector3>(RealGroundNormalOffset);
        set => WriteStruct(RealGroundNormalOffset, value);
    }

    internal const uint StateManagerOffset = RealGroundNormalOffset + Vector3.Size;
    public SHARMemory.Memory.Class StateManager => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(StateManagerOffset)); // TODO: CharacterAi::StateManager

    internal const uint RadiusOffset = StateManagerOffset + sizeof(uint);
    public float Radius
    {
        get => ReadSingle(RadiusOffset);
        set => WriteSingle(RadiusOffset, value);
    }

    internal const uint CollidedOffset = RadiusOffset + sizeof(float);
    public bool Collided
    {
        get => ReadBoolean(CollidedOffset);
        set => WriteBoolean(CollidedOffset, value);
    }

    internal const uint CurrentCollisionOffset = CollidedOffset + 4; // Padding
    public int CurrentCollision
    {
        get => ReadInt32(CurrentCollisionOffset);
        set => WriteInt32(CurrentCollisionOffset, value);
    }

    internal const uint CollisionDataOffset = CurrentCollisionOffset + sizeof(int);
    public StructArray<Structs.Character.CollisionData> CollisionData => new(Memory, Address + CollisionDataOffset, Structs.Character.CollisionData.Size, Structs.Character.CollisionData.MAX_COLLISIONS);

    internal const uint IsStandingOffset = CollisionDataOffset + Structs.Character.CollisionData.Size * Structs.Character.CollisionData.MAX_COLLISIONS;
    public bool IsStanding
    {
        get => ReadBoolean(IsStandingOffset);
        set => WriteBoolean(IsStandingOffset, value);
    }

    internal const uint WalkerLocomotionOffset = IsStandingOffset + 4; // Padding
    public SHARMemory.Memory.Class WalkerLocomotion => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(WalkerLocomotionOffset)); // TODO: WalkerLocomotionAction

    internal const uint JumpLocomotionOffset = WalkerLocomotionOffset + sizeof(uint);
    public JumpAction JumpLocomotion => Memory.ClassFactory.Create<JumpAction>(ReadUInt32(JumpLocomotionOffset));

    internal const uint StandingCollisionVolumeOffset = JumpLocomotionOffset + sizeof(uint);
    public CollisionVolume StandingCollisionVolume => Memory.ClassFactory.Create<CollisionVolume>(ReadUInt32(StandingCollisionVolumeOffset));

    internal const uint StandingJointOffset = StandingCollisionVolumeOffset + sizeof(uint);
    public Joint StandingJoint => Memory.ClassFactory.Create<Joint>(ReadUInt32(StandingJointOffset));

    internal const uint ParentTransformOffset = StandingJointOffset + sizeof(uint);
    public Matrix4x4 ParentTransform
    {
        get => ReadStruct<Matrix4x4>(ParentTransformOffset);
        set => WriteStruct(ParentTransformOffset, value);
    }

    internal const uint InvParentTransformOffset = ParentTransformOffset + Matrix4x4.Size;
    public Matrix4x4 InvParentTransform
    {
        get => ReadStruct<Matrix4x4>(InvParentTransformOffset);
        set => WriteStruct(InvParentTransformOffset, value);
    }

    internal const uint GroundVerticalVelocityOffset = InvParentTransformOffset + Matrix4x4.Size;
    public float GroundVerticalVelocity
    {
        get => ReadSingle(GroundVerticalVelocityOffset);
        set => WriteSingle(GroundVerticalVelocityOffset, value);
    }

    internal const uint GroundVerticalPositionOffset = GroundVerticalVelocityOffset + sizeof(float);
    public float GroundVerticalPosition
    {
        get => ReadSingle(GroundVerticalPositionOffset);
        set => WriteSingle(GroundVerticalPositionOffset, value);
    }

    internal const uint TurboOffset = GroundVerticalPositionOffset + sizeof(float);
    public bool Turbo
    {
        get => ReadBoolean(TurboOffset);
        set => WriteBoolean(TurboOffset, value);
    }

    internal const uint IsJumpOffset = TurboOffset + sizeof(bool);
    public bool IsJump
    {
        get => ReadBoolean(IsJumpOffset);
        set => WriteBoolean(IsJumpOffset, value);
    }

    internal const uint SolveCollisionsOffset = IsJumpOffset + sizeof(bool);
    public bool SolveCollisions
    {
        get => ReadBoolean(SolveCollisionsOffset);
        set => WriteBoolean(SolveCollisionsOffset, value);
    }

    internal const uint PropOffset = SolveCollisionsOffset + 2; // Padding
    public Structs.Character.Prop Prop
    {
        get => ReadStruct<Structs.Character.Prop>(PropOffset);
        set => WriteStruct(PropOffset, value);
    }

    internal const uint PropHandlerOffset = PropOffset + Structs.Character.Prop.Size;
    public SHARMemory.Memory.Class PropHandler => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(PropHandlerOffset));

    internal const uint PropJointOffset = PropHandlerOffset + sizeof(uint);
    public int PropJoint
    {
        get => ReadInt32(PropJointOffset);
        set => WriteInt32(PropJointOffset, value);
    }

    internal const uint VisibleOffset = PropJointOffset + sizeof(int);
    public bool Visible
    {
        get => ReadBoolean(VisibleOffset);
        set => WriteBoolean(VisibleOffset, value);
    }

    internal const uint WorldSceneOffset = VisibleOffset + 4; // Padding
    public SHARMemory.Memory.Class WorldScene => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(WorldSceneOffset)); // TODO: WorldScene

    internal const uint IsSimpleShadowOffset = WorldSceneOffset + sizeof(uint);
    public bool IsSimpleShadow
    {
        get => ReadBoolean(IsSimpleShadowOffset);
        set => WriteBoolean(IsSimpleShadowOffset, value);
    }

    internal const uint YAdjustOffset = IsSimpleShadowOffset + 4; // Padding
    public float YAdjust
    {
        get => ReadSingle(YAdjustOffset);
        set => WriteSingle(YAdjustOffset, value);
    }

    internal const uint BusyOffset = YAdjustOffset + sizeof(float);
    public bool Busy
    {
        get => ReadBoolean(BusyOffset);
        set => WriteBoolean(BusyOffset, value);
    }

    internal const uint SimpleLocoOffset = BusyOffset + sizeof(byte);
    public bool SimpleLoco
    {
        get => ReadBoolean(SimpleLocoOffset);
        set => WriteBoolean(SimpleLocoOffset, value);
    }

    internal const uint NeedChoreoUpdateOffset = SimpleLocoOffset + sizeof(byte);
    public bool NeedChoreoUpdate
    {
        get => ReadBoolean(NeedChoreoUpdateOffset);
        set => WriteBoolean(NeedChoreoUpdateOffset, value);
    }

    internal const uint ShadowColourOffset = NeedChoreoUpdateOffset + 2; // Padding
    public Color ShadowColour
    {
        get => ReadStruct<Color>(ShadowColourOffset);
        set => WriteStruct(ShadowColourOffset, value);
    }

    internal const uint TimeLeftToShockOffset = ShadowColourOffset + sizeof(int);
    public float TimeLeftToShock
    {
        get => ReadSingle(TimeLeftToShockOffset);
        set => WriteSingle(TimeLeftToShockOffset, value);
    }

    internal const uint IsBeingShockedOffset = TimeLeftToShockOffset + sizeof(float);
    public bool IsBeingShocked
    {
        get => ReadBoolean(IsBeingShockedOffset);
        set => WriteBoolean(IsBeingShockedOffset, value);
    }

    internal const uint DoKickwaveOffset = IsBeingShockedOffset + sizeof(bool);
    public bool DoKickwave
    {
        get => ReadBoolean(DoKickwaveOffset);
        set => WriteBoolean(DoKickwaveOffset, value);
    }

    internal const uint KickwaveOffset = DoKickwaveOffset + 3; // Padding
    public tDrawable Kickwave => Memory.ClassFactory.Create<tDrawable>(ReadUInt32(KickwaveOffset));

    internal const uint KickwaveControllerOffset = KickwaveOffset + sizeof(uint);
    public tFrameController KickwaveController => Memory.ClassFactory.Create<tFrameController>(ReadUInt32(KickwaveControllerOffset));

    internal const uint AmbientOffset = KickwaveControllerOffset + sizeof(uint);
    public bool Ambient
    {
        get => ReadBoolean(AmbientOffset);
        set => WriteBoolean(AmbientOffset, value);
    }

    internal const uint AmbientLocatorOffset = AmbientOffset + 4; // Padding
    public long AmbientLocator
    {
        get => ReadInt64(AmbientLocatorOffset);
        set => WriteInt64(AmbientLocatorOffset, value);
    }

    internal const uint AmbientTriggerOffset = AmbientLocatorOffset + sizeof(long);
    public SHARMemory.Memory.Class AmbientTrigger => Memory.ClassFactory.Create<SHARMemory.Memory.Class>(ReadUInt32(AmbientTriggerOffset));

    internal const uint LastFramePosOffset = AmbientTriggerOffset + sizeof(uint);
    public Vector3 LastFramePos
    {
        get => ReadStruct<Vector3>(LastFramePosOffset);
        set => WriteStruct(LastFramePosOffset, value);
    }

    internal const uint DoGroundIntersectOffset = LastFramePosOffset + Vector3.Size;
    public bool DoGroundIntersect
    {
        get => ReadBoolean(DoGroundIntersectOffset);
        set => WriteBoolean(DoGroundIntersectOffset, value);
    }

    internal const uint IntersectFrameOffset = DoGroundIntersectOffset + 4; // Padding
    public uint IntersectFrame
    {
        get => ReadUInt32(IntersectFrameOffset);
        set => WriteUInt32(IntersectFrameOffset, value);
    }

    internal const uint AllowRockinOffset = IntersectFrameOffset + sizeof(uint);
    public bool AllowRockin
    {
        get => ReadBoolean(AllowRockinOffset);
        set => WriteBoolean(AllowRockinOffset, value);
    }

    internal const uint HasBeenHitOffset = AllowRockinOffset + sizeof(bool);
    public bool HasBeenHit
    {
        get => ReadBoolean(HasBeenHitOffset);
        set => WriteBoolean(HasBeenHitOffset, value);
    }

    internal const uint SnapToGroundOffset = HasBeenHitOffset + sizeof(bool);
    public bool SnapToGround
    {
        get => ReadBoolean(SnapToGroundOffset);
        set => WriteBoolean(SnapToGroundOffset, value);
    }

    internal const uint SecondsSinceActionControllerUpdateOffset = SnapToGroundOffset + 2;
    public float SecondsSinceActionControllerUpdate
    {
        get => ReadSingle(SecondsSinceActionControllerUpdateOffset);
        set => WriteSingle(SecondsSinceActionControllerUpdateOffset, value);
    }

    internal const uint TooFarToUpdateOffset = SecondsSinceActionControllerUpdateOffset + sizeof(float);
    public bool TooFarToUpdate
    {
        get => ReadBoolean(TooFarToUpdateOffset);
        set => WriteBoolean(TooFarToUpdateOffset, value);
    }

    internal const uint SecondsSinceOnPostSimUpdateOffset = TooFarToUpdateOffset + 4; // Padding
    public float SecondsSinceOnPostSimUpdate
    {
        get => ReadSingle(SecondsSinceOnPostSimUpdateOffset);
        set => WriteSingle(SecondsSinceOnPostSimUpdateOffset, value);
    }

    internal const uint RoleOffset = SecondsSinceOnPostSimUpdateOffset + sizeof(float);
    public Roles Role
    {
        get => (Roles)ReadUInt32(RoleOffset);
        set => WriteUInt32(RoleOffset, (uint)value);
    }

    internal const uint ScaleOffset = RoleOffset + sizeof(uint);
    public float Scale
    {
        get => ReadSingle(ScaleOffset);
        set => WriteSingle(ScaleOffset, value);
    }

    internal const uint CollidedThisFrameOffset = ScaleOffset + sizeof(float);
    public bool CollidedThisFrame
    {
        get => ReadBoolean(CollidedThisFrameOffset);
        set => WriteBoolean(CollidedThisFrameOffset, value);
    }

    internal const uint IsInSubstepOffset = CollidedThisFrameOffset + sizeof(bool);
    public bool IsInSubstep
    {
        get => ReadBoolean(IsInSubstepOffset);
        set => WriteBoolean(IsInSubstepOffset, value);
    }

    internal const uint LeanOffset = IsInSubstepOffset + 3;
    public Vector3 Lean
    {
        get => ReadStruct<Vector3>(LeanOffset);
        set => WriteStruct(LeanOffset, value);
    }

    internal const uint IsLisaOffset = LeanOffset + Vector3.Size;
    public bool IsLisa
    {
        get => ReadBoolean(IsLisaOffset);
        set => WriteBoolean(IsLisaOffset, value);
    }

    internal const uint IsMargeOffset = IsLisaOffset + sizeof(bool);
    public bool IsMarge
    {
        get => ReadBoolean(IsMargeOffset);
        set => WriteBoolean(IsMargeOffset, value);
    }

    internal const uint LastGoodPosOverStaticOffset = IsMargeOffset + 3; // Padding
    public Vector3 LastGoodPosOverStatic
    {
        get => ReadStruct<Vector3>(LastGoodPosOverStaticOffset);
        set => WriteStruct(LastGoodPosOverStaticOffset, value);
    }

    internal const uint LameAssPositionOffset = LastGoodPosOverStaticOffset + Vector3.Size;
    public Vector3 LameAssPosition
    {
        get => ReadStruct<Vector3>(LameAssPositionOffset);
        set => WriteStruct(LameAssPositionOffset, value);
    }

    internal const uint ManagedOffset = LameAssPositionOffset + Vector3.Size;
    public bool Managed
    {
        get => ReadBoolean(ManagedOffset);
        set => WriteBoolean(ManagedOffset, value);
    }
}
