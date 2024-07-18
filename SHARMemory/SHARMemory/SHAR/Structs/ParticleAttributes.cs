using SHARMemory.Memory;
using System;
using static SHARMemory.SHAR.Globals;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(ParticleAttributesStruct))]
public struct ParticleAttributes
{
    public const int Size = sizeof(int) + sizeof(float) + Vector3.Size;

    public ParticleEnums.ParticleID Type;

    public float EmissionBias;

    public Vector3 Velocity;

    public ParticleAttributes(ParticleEnums.ParticleID type, float emissionBias, Vector3 velocity)
    {
        Type = type;
        EmissionBias = emissionBias;
        Velocity = velocity;
    }

    public override readonly string ToString() => $"{EmissionBias} | {Velocity}";
}

internal class ParticleAttributesStruct : Struct
{
    public override int Size => ParticleAttributes.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        ParticleEnums.ParticleID Type = (ParticleEnums.ParticleID)BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        float EmissionBias = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        Vector3 Velocity = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        return new ParticleAttributes(Type, EmissionBias, Velocity);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not ParticleAttributes Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(ParticleAttributes)}'.", nameof(Value));

        BitConverter.GetBytes((int)Value2.Type).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.EmissionBias).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        Memory.BytesFromStruct(Value2.Velocity, Buffer, Offset);
    }
}
