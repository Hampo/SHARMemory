using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(StreetRaceListStruct))]
    public struct StreetRaceList
    {
        public const int Size = MissionRecord.Size * CharacterSheet.MAX_STREETRACES;

        public MissionRecord[] List;

        public StreetRaceList(MissionRecord[] list)
        {
            List = list;
        }

        public override string ToString() => $"{List}";
    }

    internal class StreetRaceListStruct : Struct
    {
        public override int Size => StreetRaceList.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            MissionRecord[] List = new MissionRecord[CharacterSheet.MAX_STREETRACES];
            for (int i = 0; i < CharacterSheet.MAX_STREETRACES; i++)
            {
                List[i] = Memory.StructFromBytes<MissionRecord>(Bytes, Offset);
                Offset += MissionRecord.Size;
            }
            return new StreetRaceList(List);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not StreetRaceList Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(StreetRaceList)}'.", nameof(Value));

            for (int i = 0; i < CharacterSheet.MAX_STREETRACES; i++)
            {
                Memory.BytesFromStruct(Value2.List[i], Buffer, Offset);
                Offset += MissionRecord.Size;
            }
        }
    }
}