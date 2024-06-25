using SHARMemory.Memory;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(MissionRecordStruct))]
    public struct MissionRecord
    {
        public const int Size = 16 + 4 + sizeof(uint) + 4 + sizeof(int);

        public string Name;
        public bool Completed;
        public bool BonusObjective;
        public uint NumAttempts;
        public bool SkippedMission;
        public int BestTime;

        public MissionRecord(string name, bool completed, bool bonusObjective, uint numAttempts, bool skippedMission, int bestTime)
        {
            Name = name;
            Completed = completed;
            BonusObjective = bonusObjective;
            NumAttempts = numAttempts;
            SkippedMission = skippedMission;
            BestTime = bestTime;
        }

        public override string ToString() => $"{Name} | {Completed} | {BonusObjective} | {NumAttempts} | {SkippedMission} | {BestTime}";
    }

    internal class MissionRecordStruct : Struct
    {
        public override int Size => MissionRecord.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
            Offset += 16;
            bool Completed = BitConverter.ToBoolean(Bytes, Offset);
            Offset += 1;
            bool BonusObjective = BitConverter.ToBoolean(Bytes, Offset);
            Offset += 3;
            uint NumAttempts = BitConverter.ToUInt32(Bytes, Offset);
            Offset += sizeof(uint);
            bool SkippedMission = BitConverter.ToBoolean(Bytes, Offset);
            Offset += 4;
            int BestTime = BitConverter.ToInt32(Bytes, Offset);
            return new MissionRecord(Name, Completed, BonusObjective, NumAttempts, SkippedMission, BestTime);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not MissionRecord Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(MissionRecord)}'.", nameof(Value));

            Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
            Offset += 16;
            BitConverter.GetBytes(Value2.Completed).CopyTo(Buffer, Offset);
            Offset += 1;
            BitConverter.GetBytes(Value2.BonusObjective).CopyTo(Buffer, Offset);
            Offset += 3;
            BitConverter.GetBytes(Value2.NumAttempts).CopyTo(Buffer, Offset);
            Offset += sizeof(uint);
            BitConverter.GetBytes(Value2.SkippedMission).CopyTo(Buffer, Offset);
            Offset += 4;
            BitConverter.GetBytes(Value2.BestTime).CopyTo(Buffer, Offset);
        }
    }
}
