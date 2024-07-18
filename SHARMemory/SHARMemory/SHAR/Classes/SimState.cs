using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVSimState@sim@@")]
public class SimState : Class
{
    public const float ApproxSpeedMagnitudeFactor = 2f;

    public enum SimControlEnum
    {
        AICtrl = 0,
        SimulationCtrl
    }

    public SimState(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public int AIRefIndex
    {
        get => ReadInt32(12);
        set => WriteInt32(12, value);
    }

    public Matrix4x4 Transform
    {
        get => ReadStruct<Matrix4x4>(16);
        set => WriteStruct(16, value);
    }

    public SimVelocityState VelocityState
    {
        get => ReadStruct<SimVelocityState>(80);
        set => WriteStruct(80, value);
    }

    public float Scale
    {
        get => ReadSingle(104);
        set => WriteSingle(104, value);
    }

    public SimControlEnum Control
    {
        get => (SimControlEnum)ReadUInt32(108);
        set => WriteUInt32(108, (uint)value);
    }

    public SimulatedObject SimulatedObject => Memory.ClassFactory.Create<SimulatedObject>(ReadUInt32(112));

    public CollisionObject CollisionObject => Memory.ClassFactory.Create<CollisionObject>(ReadUInt32(116));

    public VirtualCM VirtualCM => Memory.ClassFactory.Create<VirtualCM>(ReadUInt32(120));

    public bool ObjectMoving
    {
        get => ReadBoolean(132);
        set => WriteBoolean(132, value);
    }

    public float SafeTimeBeforeCollision
    {
        get => ReadSingle(136);
        set => WriteSingle(136, value);
    }

    public float ApproxSpeedMagnitude
    {
        get => ReadSingle(140);
        set => WriteSingle(140, value);
    }

    public bool Articulated
    {
        get => ReadBoolean(144);
        set => WriteBoolean(144, value);
    }

    public void InitVirtualCM()
    {
        VirtualCM virtualCM = VirtualCM;
        if (virtualCM != null)
        {
            Matrix4x4 transform = Transform;
            Vector3 position = new(transform.M41, transform.M42, transform.M43);

            Vector3 linearVelocity = VelocityState.Linear;

            // TODO: virtualCM.InitLinear(position, linearVelocity);
        }
    }

    public void SetControl(SimControlEnum inControl)
    {
        if (Control == inControl)
            return;

        Control = inControl;

        if (inControl == SimControlEnum.SimulationCtrl)
        {
            SimulatedObject simulatedObject = SimulatedObject;
            if (simulatedObject != null)
            {
                // TODO: simulatedObject.SyncSimObj(false);
                // TODO: simulatedObject.WakeUp();
            }
            else
            {
                Control = SimControlEnum.AICtrl;
                return;
            }
        }
        InitVirtualCM();
    }

    public virtual void SetTransform(Matrix4x4 inTransform, float dt = 0)
    {
        Matrix4x4 transform = Transform;
        bool objectMoving = !transform.SameMatrix(inTransform);
        ObjectMoving = objectMoving;

        CollisionObject collisionObject = CollisionObject;
        SimVelocityState velocityState = VelocityState;

        if (Control == SimControlEnum.AICtrl)
        {
            if (objectMoving)
            {
                if (dt != 0)
                {
                    // TODO: ExtractVelocityFromMatrix(ref transform, ref inTransform, Scale, dt, ref velocityState);
                    VelocityState = velocityState;
                }
                else
                {
                    ResetVelocities();

                    if (collisionObject != null)
                    {
                        collisionObject.Relocated();
                        collisionObject.Update();
                    }
                }
            }
            else
            {
                ResetVelocities();
            }
        }

        if (collisionObject != null && objectMoving)
        {
            MoveCollisionObject(transform, inTransform);
        }

        Transform = inTransform;

        VirtualCM virtualCM = VirtualCM;
        if (virtualCM != null)
        {
            // TODO: virtualCM.Update(GetPosition(), velocityState.Linear, dt);
        }

        if (!Articulated)
        {
            float tmp = velocityState.Linear.DotProduct(velocityState.Linear);
            float upApproxSpeedMagnitude = UpApproxSpeedMagnitude();
            if (tmp > upApproxSpeedMagnitude * upApproxSpeedMagnitude)
            {
                ApproxSpeedMagnitude = (float)Math.Sqrt(tmp);
                collisionObject?.Relocated();
            }
            else if (tmp < DownApproxSpeedMagnitude())
            {
                ApproxSpeedMagnitude = (float)Math.Sqrt(tmp);
            }
        }
    }

    public void MoveCollisionObject(Matrix4x4 previousTransform, Matrix4x4 newTransform)
    {
        Matrix4x4 m = previousTransform;
        m.Invert();

        Vector3 p0 = new(0);
        Vector3 dp = new(0);

        CollisionObject collisionObject = CollisionObject;
        CollisionVolume collisionVolume = collisionObject?.CollisionVolume;
        if (collisionVolume != null)
        {
            Vector3 position = collisionVolume.Position;
            m.Transform(position, ref p0);
            newTransform.Transform(p0, ref dp);
            dp.Sub(position);
            collisionObject.Moved(dp);
        }
    }

    public Vector3 GetPosition()
    {
        Matrix4x4 transform = Transform;
        return new Vector3(transform.M41, transform.M42, transform.M43);
    }

    public float UpApproxSpeedMagnitude() => ApproxSpeedMagnitude * ApproxSpeedMagnitudeFactor;

    public float DownApproxSpeedMagnitude() => ApproxSpeedMagnitude / ApproxSpeedMagnitudeFactor;

    public virtual void ResetVelocities()
    {
        SimVelocityState velocityState = VelocityState;
        velocityState.Reset();
        VelocityState = velocityState;
    }

    public void ExtractVelocityFromMatrix(ref Matrix4x4 oldMatrix, ref Matrix4x4 newMatrix, float scale, float dt, ref SimVelocityState velocity)
    {
        // TODO
    }
}
