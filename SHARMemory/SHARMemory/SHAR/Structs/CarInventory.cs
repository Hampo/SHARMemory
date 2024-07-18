using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(CarInventoryStruct))]
public struct CarInventory
{
    public const int Size = CarCharacterSheet.Size * CharacterSheet.MAX_CARS_OWNED + sizeof(int);

    public CarCharacterSheet[] Cars;

    public int Counter;

    public CarInventory(CarCharacterSheet[] cars, int counter)
    {
        Cars = cars;
        Counter = counter;
    }

    public override readonly string ToString() => $"{Cars} | {Counter}";
}

internal class CarInventoryStruct : Struct
{
    public override int Size => CarInventory.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        CarCharacterSheet[] Cars = new CarCharacterSheet[CharacterSheet.MAX_CARS_OWNED];
        for (int i = 0; i < CharacterSheet.MAX_CARS_OWNED; i++)
        {
            Cars[i] = Memory.StructFromBytes<CarCharacterSheet>(Bytes, Offset);
            Offset += CarCharacterSheet.Size;
        }
        int Counter = BitConverter.ToInt32(Bytes, Offset);
        return new CarInventory(Cars, Counter);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CarInventory Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CarInventory)}'.", nameof(Value));

        for (int i = 0; i < CharacterSheet.MAX_CARS_OWNED; i++)
        {
            Memory.BytesFromStruct(Value2.Cars[i], Buffer, Offset);
            Offset += CarCharacterSheet.Size;
        }
        BitConverter.GetBytes(Value2.Counter).CopyTo(Buffer, Offset);
    }
}