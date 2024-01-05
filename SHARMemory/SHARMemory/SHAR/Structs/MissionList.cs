using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(MissionListStruct))]
    public struct MissionList
    {
        public const int Size = MissionRecord.Size * CharacterSheet.MAX_MISSIONS;

        public MissionRecord[] List;

        public MissionList(MissionRecord[] list)
        {
            List = list;
        }

        public override string ToString() => $"{List}";
    }

    internal class MissionListStruct : Struct
    {
        public override int Size => MissionList.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            MissionRecord[] List = new MissionRecord[CharacterSheet.MAX_MISSIONS];
            for (int i = 0; i < CharacterSheet.MAX_MISSIONS; i++)
            {
                List[i] = Memory.StructFromBytes<MissionRecord>(Bytes, Offset);
                Offset += MissionRecord.Size;
            }
            return new MissionList(List);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not MissionList Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(MissionList)}'.", nameof(Value));

            for (int i = 0; i < CharacterSheet.MAX_MISSIONS; i++)
            {
                Memory.BytesFromStruct(Value2.List[i], Buffer, Offset);
                Offset += MissionRecord.Size;
            }
        }
    }
}