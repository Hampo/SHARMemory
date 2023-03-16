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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not PMD PMD)
                return false;

            return Equals(PMD);
        }

        public static bool operator ==(PMD PMD1, PMD PMD2) => PMD1.Equals(PMD2);
        public static bool operator !=(PMD PMD1, PMD PMD2) => !PMD1.Equals(PMD2);
    }

    internal class PMDStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new PMD(Memory.ReadInt32(Address), Memory.ReadInt32(Address + sizeof(int)), Memory.ReadInt32(Address + sizeof(int) + sizeof(int)));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not PMD Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(PMD)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.MDisp);
            Memory.WriteInt32(Address, Value2.PDisp);
            Memory.WriteInt32(Address, Value2.VDisp);
        }
    }
}