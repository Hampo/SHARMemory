using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs.Character;

[Struct(typeof(CollisionDataStruct))]
public struct CollisionData
{
    public const int Size = Vector3.Size + Vector3.Size + sizeof(float) + sizeof(uint);
    public const int MAX_COLLISIONS = 8;

    public Vector3 CollisionPosition;

    public Vector3 CollisionNormal;

    public float CollisionDistance;

    public CollisionVolume CollisionVolume;

    public CollisionData(Vector3 collisionPosition, Vector3 collisionNormal, float collisionDistance, CollisionVolume collisionVolume)
    {
        CollisionPosition = collisionPosition;
        CollisionNormal = collisionNormal;
        CollisionDistance = collisionDistance;
        CollisionVolume = collisionVolume;
    }

    public override readonly string ToString() => $"{CollisionPosition} | {CollisionNormal} | {CollisionDistance} | {CollisionVolume}";
}

internal class CollisionDataStruct : Struct
{
    public override int Size => CollisionData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Vector3 CollisionPosition = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        Vector3 CollisionNormal = Memory.StructFromBytes<Vector3>(Bytes, Offset);
        Offset += Vector3.Size;
        float CollisionDistance = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        CollisionVolume CollisionVolume = Memory.ClassFactory.Create<CollisionVolume>(BitConverter.ToUInt32(Bytes, Offset));
        return new CollisionData(CollisionPosition, CollisionNormal, CollisionDistance, CollisionVolume);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CollisionData Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CollisionData)}'.", nameof(Value));

        Memory.BytesFromStruct(Value2.CollisionPosition, Buffer, Offset);
        Offset += Vector3.Size;
        Memory.BytesFromStruct(Value2.CollisionNormal, Buffer, Offset);
        Offset += Vector3.Size;
        BitConverter.GetBytes(Value2.CollisionDistance).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.CollisionVolume?.Address ?? 0).CopyTo(Buffer, Offset);
    }
}
