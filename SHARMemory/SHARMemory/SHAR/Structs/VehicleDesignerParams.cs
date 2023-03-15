using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(VehicleDesignerParamsStruct))]
    public struct VehicleDesignerParams
    {
        public const int Size = sizeof(float) * 28 + Vector3.Size;

        public float GasScale;

        public float SlipGasScale;

        public float HighSpeedGasScale;

        public float GasScaleSpeedThreshold;

        public float BrakeScale;

        public float TopSpeedKmh;

        public float Mass;

        public float MaxWheelTurnAngle;

        public float HighSpeedSteeringDrop;

        public float TireLateralStaticGrip;

        public float TireLateralResistanceNormal;

        public float TireLateralResistanceSlip;

        public float TireLateralResistanceSlipNoEBrake;

        public float SlipEffectNoEBrake;

        public float EBrakeEffect;

        public float SuspensionLimit;

        public float Springk;

        public float Damperc;

        public float SuspensionYOffset;

        public float HitPoints;

        public float BurnoutRange;

        public float WheelieRange;

        public float WheelieYOffset;

        public float WheelieZOffset;

        public float MaxSpeedBurstTime;

        public float DonutTorque;

        public float WeebleOffset;

        public float GamblingOdds;

        public Vector3 CMOffset;

        public VehicleDesignerParams(float gasScale, float slipGasScale, float highSpeedGasScale, float gasScaleSpeedThreshold, float brakeScale, float topSpeedKmh, float mass, float maxWheelTurnAngle, float highSpeedSteeringDrop, float tireLateralStaticGrip, float tireLateralResistanceNormal, float tireLateralResistanceSlip, float tireLateralResistanceSlipNoEBrake, float slipEffectNoEBrake, float eBrakeEffect, float suspensionLimit, float springk, float damperc, float suspensionYOffset, float hitPoints, float burnoutRange, float wheelieRange, float wheelieYOffset, float wheelieZOffset, float maxSpeedBurstTime, float donutTorque, float weebleOffset, float gamblingOdds, Vector3 cmOffset)
        {
            GasScale = gasScale;
            SlipGasScale = slipGasScale;
            HighSpeedGasScale = highSpeedGasScale;
            GasScaleSpeedThreshold = gasScaleSpeedThreshold;
            BrakeScale = brakeScale;
            TopSpeedKmh = topSpeedKmh;
            Mass = mass;
            MaxWheelTurnAngle = maxWheelTurnAngle;
            HighSpeedSteeringDrop = highSpeedSteeringDrop;
            TireLateralStaticGrip = tireLateralStaticGrip;
            TireLateralResistanceNormal = tireLateralResistanceNormal;
            TireLateralResistanceSlip = tireLateralResistanceSlip;
            TireLateralResistanceSlipNoEBrake = tireLateralResistanceSlipNoEBrake;
            SlipEffectNoEBrake = slipEffectNoEBrake;
            EBrakeEffect = eBrakeEffect;
            SuspensionLimit = suspensionLimit;
            Springk = springk;
            Damperc = damperc;
            SuspensionYOffset = suspensionYOffset;
            HitPoints = hitPoints;
            BurnoutRange = burnoutRange;
            WheelieRange = wheelieRange;
            WheelieYOffset = wheelieYOffset;
            WheelieZOffset = wheelieZOffset;
            MaxSpeedBurstTime = maxSpeedBurstTime;
            DonutTorque = donutTorque;
            WeebleOffset = weebleOffset;
            GamblingOdds = gamblingOdds;
            CMOffset = cmOffset;
        }

        public override string ToString() => $"{GasScale} | {SlipGasScale} | {HighSpeedGasScale} | {GasScaleSpeedThreshold} | {BrakeScale} | {TopSpeedKmh} | {Mass} | {MaxWheelTurnAngle} | {HighSpeedSteeringDrop} | {TireLateralStaticGrip} | {TireLateralResistanceNormal} | {TireLateralResistanceSlip} | {TireLateralResistanceSlipNoEBrake} | {SlipEffectNoEBrake} | {EBrakeEffect} | {SuspensionLimit} | {Springk} | {Damperc} | {SuspensionYOffset} | {HitPoints} | {BurnoutRange} | {WheelieRange} | {WheelieYOffset} | {WheelieZOffset} | {MaxSpeedBurstTime} | {DonutTorque} | {WeebleOffset} | {GamblingOdds} | {CMOffset}";
    }

    internal class VehicleDesignerParamsStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new VehicleDesignerParams(Memory.ReadSingle(Address), Memory.ReadSingle(Address + sizeof(float)), Memory.ReadSingle(Address + sizeof(float) * 2), Memory.ReadSingle(Address + sizeof(float) * 3), Memory.ReadSingle(Address + sizeof(float) * 4), Memory.ReadSingle(Address + sizeof(float) * 5), Memory.ReadSingle(Address + sizeof(float) * 6), Memory.ReadSingle(Address + sizeof(float) * 7), Memory.ReadSingle(Address + sizeof(float) * 8), Memory.ReadSingle(Address + sizeof(float) * 9), Memory.ReadSingle(Address + sizeof(float) * 10), Memory.ReadSingle(Address + sizeof(float) * 11), Memory.ReadSingle(Address + sizeof(float) * 12), Memory.ReadSingle(Address + sizeof(float) * 13), Memory.ReadSingle(Address + sizeof(float) * 14), Memory.ReadSingle(Address + sizeof(float) * 15), Memory.ReadSingle(Address + sizeof(float) * 16), Memory.ReadSingle(Address + sizeof(float) * 17), Memory.ReadSingle(Address + sizeof(float) * 18), Memory.ReadSingle(Address + sizeof(float) * 19), Memory.ReadSingle(Address + sizeof(float) * 20), Memory.ReadSingle(Address + sizeof(float) * 21), Memory.ReadSingle(Address + sizeof(float) * 22), Memory.ReadSingle(Address + sizeof(float) * 23), Memory.ReadSingle(Address + sizeof(float) * 24), Memory.ReadSingle(Address + sizeof(float) * 25), Memory.ReadSingle(Address + sizeof(float) * 26), Memory.ReadSingle(Address + sizeof(float) * 27), Memory.ReadStruct<Vector3>(Address + sizeof(float) * 28));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not VehicleDesignerParams Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(VehicleDesignerParams)}'.", nameof(Value));

            Memory.WriteSingle(Address, Value2.GasScale);
            Memory.WriteSingle(Address + sizeof(float), Value2.SlipGasScale);
            Memory.WriteSingle(Address + sizeof(float) * 2, Value2.HighSpeedGasScale);
            Memory.WriteSingle(Address + sizeof(float) * 3, Value2.GasScaleSpeedThreshold);
            Memory.WriteSingle(Address + sizeof(float) * 4, Value2.BrakeScale);
            Memory.WriteSingle(Address + sizeof(float) * 5, Value2.TopSpeedKmh);
            Memory.WriteSingle(Address + sizeof(float) * 6, Value2.Mass);
            Memory.WriteSingle(Address + sizeof(float) * 7, Value2.MaxWheelTurnAngle);
            Memory.WriteSingle(Address + sizeof(float) * 8, Value2.HighSpeedSteeringDrop);
            Memory.WriteSingle(Address + sizeof(float) * 9, Value2.TireLateralStaticGrip);
            Memory.WriteSingle(Address + sizeof(float) * 10, Value2.TireLateralResistanceNormal);
            Memory.WriteSingle(Address + sizeof(float) * 11, Value2.TireLateralResistanceSlip);
            Memory.WriteSingle(Address + sizeof(float) * 12, Value2.TireLateralResistanceSlipNoEBrake);
            Memory.WriteSingle(Address + sizeof(float) * 13, Value2.SlipEffectNoEBrake);
            Memory.WriteSingle(Address + sizeof(float) * 14, Value2.EBrakeEffect);
            Memory.WriteSingle(Address + sizeof(float) * 15, Value2.SuspensionLimit);
            Memory.WriteSingle(Address + sizeof(float) * 16, Value2.Springk);
            Memory.WriteSingle(Address + sizeof(float) * 17, Value2.Damperc);
            Memory.WriteSingle(Address + sizeof(float) * 18, Value2.SuspensionYOffset);
            Memory.WriteSingle(Address + sizeof(float) * 19, Value2.HitPoints);
            Memory.WriteSingle(Address + sizeof(float) * 20, Value2.BurnoutRange);
            Memory.WriteSingle(Address + sizeof(float) * 21, Value2.WheelieRange);
            Memory.WriteSingle(Address + sizeof(float) * 22, Value2.WheelieYOffset);
            Memory.WriteSingle(Address + sizeof(float) * 23, Value2.WheelieZOffset);
            Memory.WriteSingle(Address + sizeof(float) * 24, Value2.MaxSpeedBurstTime);
            Memory.WriteSingle(Address + sizeof(float) * 25, Value2.DonutTorque);
            Memory.WriteSingle(Address + sizeof(float) * 26, Value2.WeebleOffset);
            Memory.WriteSingle(Address + sizeof(float) * 27, Value2.GamblingOdds);
            Memory.WriteStruct(Address + sizeof(float) * 28, Value2.CMOffset);
        }
    }
}
