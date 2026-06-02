using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs.Character;

[Struct(typeof(PropStruct))]
public struct Prop
{
    public const int Size = sizeof(uint) + sizeof(uint);

    public InstDynaPhysDSG PropDSG;

    public tPose Pose;

    public Prop(InstDynaPhysDSG propDSG, tPose pose)
    {
        PropDSG = propDSG;
        Pose = pose;
    }

    public override readonly string ToString() => $"{PropDSG} | {Pose}";
}

internal class PropStruct : Struct
{
    public override int Size => Prop.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        InstDynaPhysDSG PropDSG = Memory.ClassFactory.Create<InstDynaPhysDSG>(BitConverter.ToUInt32(Bytes, Offset));
        Offset += sizeof(uint);
        tPose Pose = Memory.ClassFactory.Create<tPose>(BitConverter.ToUInt32(Bytes, Offset));
        return new Prop(PropDSG, Pose);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not Prop Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(Prop)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.PropDSG?.Address ?? 0).CopyTo(Buffer, Offset);
        Offset += sizeof(uint);
        BitConverter.GetBytes(Value2.Pose?.Address ?? 0).CopyTo(Buffer, Offset);
    }
}
