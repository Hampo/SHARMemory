using System;

namespace SHARMemory.Memory.RTTI
{
    [Struct(typeof(PMDStruct))]
    public struct PMD : IEquatable<PMD>
    {
        public const int Size = sizeof(int) + sizeof(int) + sizeof(int);

        public int MDisp;
        public int PDisp;
        public int VDisp;

        public PMD(int MDisp, int PDisp, int VDisp)
        {
            this.MDisp = MDisp;
            this.PDisp = PDisp;
            this.VDisp = VDisp;
        }

        public override string ToString() => $"{MDisp} | {PDisp} | {VDisp}";

        public override int GetHashCode()
        {
            var hashCode = -2050652928;
            hashCode = hashCode * -1521134295 + MDisp.GetHashCode();
            hashCode = hashCode * -1521134295 + PDisp.GetHashCode();
            hashCode = hashCode * -1521134295 + VDisp.GetHashCode();
            return hashCode;
        }

        public bool Equals(PMD other)
        {
            if (MDisp != other.MDisp)
                return false;
            if (PDisp != other.PDisp)
                return false;
            if (VDisp != other.VDisp)
                return false;

            return true;
        }

        public override bool Equals(object obj) => obj is PMD pmd && Equals(pmd);

        public static bool operator ==(PMD PMD1, PMD PMD2) => PMD1.Equals(PMD2);
        public static bool operator !=(PMD PMD1, PMD PMD2) => !PMD1.Equals(PMD2);
    }

    internal class PMDStruct : Struct
    {
        public override int Size => PMD.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            int MDisp = BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(int);
            int PDisp = BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(int);
            int VDisp = BitConverter.ToInt32(Bytes, Offset);
            return new PMD(MDisp, PDisp, VDisp);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not PMD Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(PMD)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.MDisp).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
            BitConverter.GetBytes(Value2.PDisp).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
            BitConverter.GetBytes(Value2.VDisp).CopyTo(Buffer, Offset);
        }
    }
}