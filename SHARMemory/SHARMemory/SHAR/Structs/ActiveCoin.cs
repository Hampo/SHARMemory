using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;
using System.Linq;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(ActiveCoinStruct))]
    public struct ActiveCoin
    {
        public const int Size = 12 + Vector3.Size + sizeof(float) + sizeof(float) + sizeof(float) + sizeof(float) + sizeof(CoinManager.CoinState);

        /// <summary>
        /// It's a `union` in C++ and that's scary. Unsure on the deciding factor, but it's either a 12 byte `Vector3` Velocity, or `long` Sector and `short` PersistentObjectID.
        /// </summary>
        public byte[] VelocityOrSectorAndPersistentObjectID;

        public Vector3 Position;

        public float HeadingCos;

        public float HeadingSin;

        public float Age;

        public float Ground;

        public CoinManager.CoinState State;

        public ActiveCoin(byte[] velocityOrSectorAndPersistentObjectID, Vector3 position, float headingCos, float headingSin, float age, float ground, CoinManager.CoinState state)
        {
            if (velocityOrSectorAndPersistentObjectID.Length != 12)
                throw new ArgumentException("VelocityOrSectorAndPersistentObjectID must have a length of 12.", nameof(velocityOrSectorAndPersistentObjectID));

            VelocityOrSectorAndPersistentObjectID = velocityOrSectorAndPersistentObjectID;
            Position = position;
            HeadingCos = headingCos;
            HeadingSin = headingSin;
            Age = age;
            Ground = ground;
            State = state;
        }

        public override string ToString() => $"{Position} | {HeadingCos} | {HeadingSin} | {Age} | {Ground} | {State}";
    }

    internal class ActiveCoinStruct : Struct
    {
        public override int Size => ActiveCoin.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            byte[] VelocityOrSectorAndPersistentObjectID = Bytes.Take(12).ToArray();
            Offset += 12;
            Vector3 Position = Memory.StructFromBytes<Vector3>(Bytes, Offset);
            Offset += Vector3.Size;
            float HeadingCos = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float HeadingSin = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Age = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            float Ground = BitConverter.ToSingle(Bytes, Offset);
            Offset += sizeof(float);
            CoinManager.CoinState State = (CoinManager.CoinState)BitConverter.ToInt32(Bytes, Offset);
            return new ActiveCoin(VelocityOrSectorAndPersistentObjectID, Position, HeadingCos, HeadingSin, Age, Ground, State);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not ActiveCoin Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(ActiveCoin)}'.", nameof(Value));
            if (Value2.VelocityOrSectorAndPersistentObjectID.Length != 12)
                throw new ArgumentException($"Value '{nameof(Value)}'.'{nameof(Value2.VelocityOrSectorAndPersistentObjectID)}' must have a length of 12.", nameof(Value));

            Value2.VelocityOrSectorAndPersistentObjectID.CopyTo(Buffer, Offset);
            Offset += 12;
            Memory.BytesFromStruct(Value2.Position, Buffer, Offset);
            Offset += Vector3.Size;
            BitConverter.GetBytes(Value2.HeadingCos).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.HeadingSin).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Age).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes(Value2.Ground).CopyTo(Buffer, Offset);
            Offset += sizeof(float);
            BitConverter.GetBytes((int)Value2.State).CopyTo(Buffer, Offset);
        }
    }
}
