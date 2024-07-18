using SHARMemory.Memory;
using System;
using System.Text;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(CarCharacterSheetStruct))]
public struct CarCharacterSheet
{
    public const int Size = 16 + sizeof(float) + sizeof(float);

    public string Name;

    public float CurrentHealth;

    public float MaxHealth;

    public CarCharacterSheet(string name, float currentHealth, float maxHealth)
    {
        Name = name;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }

    public override readonly string ToString() => $"{Name} | {CurrentHealth} | {MaxHealth}";
}

internal class CarCharacterSheetStruct : Struct
{
    public override int Size => CarCharacterSheet.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        string Name = ProcessMemory.NullTerminate(Encoding.UTF8.GetString(Bytes, Offset, 16));
        Offset += 16;
        float CurrentHealth = BitConverter.ToSingle(Bytes, Offset);
        Offset += sizeof(float);
        float MaxHealth = BitConverter.ToSingle(Bytes, Offset);
        return new CarCharacterSheet(Name, CurrentHealth, MaxHealth);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not CarCharacterSheet Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(CarCharacterSheet)}'.", nameof(Value));

        Memory.GetStringBytes(Value2.Name, Encoding.UTF8, 16).CopyTo(Buffer, Offset);
        Offset += 16;
        BitConverter.GetBytes(Value2.CurrentHealth).CopyTo(Buffer, Offset);
        Offset += sizeof(float);
        BitConverter.GetBytes(Value2.MaxHealth).CopyTo(Buffer, Offset);
    }
}
