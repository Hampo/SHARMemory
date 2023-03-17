using SHARMemory.Memory;
using System;

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

    internal class VehicleDesignerParamsStruct : Struct
    {
        public override int Size => VehicleDesignerParams.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            float GasScale = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float SlipGasScale = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float HighSpeedGasScale = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float GasScaleSpeedThreshold = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float BrakeScale = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float TopSpeedKmh = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Mass = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float MaxWheelTurnAngle = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float HighSpeedSteeringDrop = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float TireLateralStaticGrip = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float TireLateralResistanceNormal = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float TireLateralResistanceSlip = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float TireLateralResistanceSlipNoEBrake = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float SlipEffectNoEBrake = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float EBrakeEffect = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float SuspensionLimit = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Springk = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Damperc = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float SuspensionYOffset = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float HitPoints = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float BurnoutRange = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float WheelieRange = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float WheelieYOffset = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float WheelieZOffset = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float MaxSpeedBurstTime = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float DonutTorque = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float WeebleOffset = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float GamblingOdds = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            Vector3 CMOffset = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            return new VehicleDesignerParams(GasScale, SlipGasScale, HighSpeedGasScale, GasScaleSpeedThreshold, BrakeScale, TopSpeedKmh, Mass, MaxWheelTurnAngle, HighSpeedSteeringDrop, TireLateralStaticGrip, TireLateralResistanceNormal, TireLateralResistanceSlip, TireLateralResistanceSlipNoEBrake, SlipEffectNoEBrake, EBrakeEffect, SuspensionLimit, Springk, Damperc, SuspensionYOffset, HitPoints, BurnoutRange, WheelieRange, WheelieYOffset, WheelieZOffset, MaxSpeedBurstTime, DonutTorque, WeebleOffset, GamblingOdds, CMOffset);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not VehicleDesignerParams Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(VehicleDesignerParams)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.GasScale).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.SlipGasScale).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.HighSpeedGasScale).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.GasScaleSpeedThreshold).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.BrakeScale).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.TopSpeedKmh).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Mass).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.MaxWheelTurnAngle).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.HighSpeedSteeringDrop).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.TireLateralStaticGrip).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.TireLateralResistanceNormal).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.TireLateralResistanceSlip).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.TireLateralResistanceSlipNoEBrake).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.SlipEffectNoEBrake).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.EBrakeEffect).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.SuspensionLimit).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Springk).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Damperc).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.SuspensionYOffset).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.HitPoints).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.BurnoutRange).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.WheelieRange).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.WheelieYOffset).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.WheelieZOffset).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.MaxSpeedBurstTime).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.DonutTorque).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.WeebleOffset).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.GamblingOdds).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            Memory.BytesFromStruct(Value2.CMOffset, Buffer, Offset);
        }
    }
}
