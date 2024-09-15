using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(CardListStruct))]
public struct CardList
{
    public const int Size = sizeof(uint) * CardGallery.NUM_CARDS_PER_LEVEL + sizeof(int);

    public Card[] Cards;
    public int NumCards;

    public CardList(Card[] cards, int numCards)
    {
        Cards = cards;
        NumCards = numCards;
    }

    public override readonly string ToString() => $"{Cards} | {NumCards}";
}

internal class CardListStruct : Struct
{
    public override int Size => CardList.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Card[] Cards = new Card[CardGallery.NUM_CARDS_PER_LEVEL];
        for (int i = 0; i < CardGallery.NUM_CARDS_PER_LEVEL; i++)
        {
            Cards[i] = Memory.ClassFactory.Create<Card>(BitConverter.ToUInt32(Bytes, Offset));
            Offset += sizeof(uint);
        }
        int NumCards = BitConverter.ToInt32(Bytes, Offset);
        return new CardList(Cards, NumCards);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CardList Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CardList)}'.", nameof(Value));

        for (uint i = 0; i < CardGallery.NUM_CARDS_PER_LEVEL; i++)
        {
            BitConverter.GetBytes(Value2.Cards[i]?.Address ?? 0).CopyTo(Buffer, Offset);
            Offset += sizeof(uint);
        }
        BitConverter.GetBytes(Value2.NumCards).CopyTo(Buffer, Offset);
    }
}