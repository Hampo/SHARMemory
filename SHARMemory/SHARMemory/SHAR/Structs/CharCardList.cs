using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(CharCardListStruct))]
    public struct CharCardList
    {
        public const int Size = Record.Size * CharacterSheet.MAX_CARDS;

        public Record[] List;

        public CharCardList(Record[] list)
        {
            List = list;
        }

        public override string ToString() => $"{List}";
    }

    internal class CharCardListStruct : Struct
    {
        public override int Size => CharCardList.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            Record[] List = new Record[CharacterSheet.MAX_CARDS];
            for (int i = 0; i < CharacterSheet.MAX_CARDS; i++)
            {
                List[i] = Memory.StructFromBytes<Record>(Bytes, Offset);
                Offset += Record.Size;
            }
            return new CharCardList(List);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not CharCardList Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CharCardList)}'.", nameof(Value));

            for (int i = 0; i < CharacterSheet.MAX_CARDS; i++)
            {
                Memory.BytesFromStruct(Value2.List[i], Buffer, Offset);
                Offset += Record.Size;
            }
        }
    }
}