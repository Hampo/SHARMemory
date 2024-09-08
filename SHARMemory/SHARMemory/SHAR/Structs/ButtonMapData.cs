using SHARMemory.Memory;
using System;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(ButtonMapDataStruct))]
public struct ButtonMapData
{
    public const int Size = 4 + sizeof(int) + sizeof(int) + sizeof(uint);

    public bool MapNext;
    public int Map;
    public int VirtualButton;
    public uint ButtonMappedCallback;

    public ButtonMapData(bool mapNext, int map, int virtualButton, uint buttonMappedCallback)
    {
        MapNext = mapNext;
        Map = map;
        VirtualButton = virtualButton;
        ButtonMappedCallback = buttonMappedCallback;
    }

    public override readonly string ToString() => $"{MapNext} | {Map} | {VirtualButton} | {ButtonMappedCallback}";
}

internal class ButtonMapDataStruct : Struct
{
    public override int Size => ButtonMapData.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        bool MapNext = BitConverter.ToBoolean(Bytes, Offset);
        Offset += 4;
        int Map = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        int VirtualButton = BitConverter.ToInt32(Bytes, Offset);
        Offset += sizeof(int);
        uint ButtonMappedCallback = BitConverter.ToUInt32(Bytes, Offset);
        return new ButtonMapData(MapNext, Map, VirtualButton, ButtonMappedCallback);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not ButtonMapData Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(ButtonMapData)}'.", nameof(Value));

        BitConverter.GetBytes(Value2.MapNext).CopyTo(Buffer, Offset);
        Offset += 4;
        BitConverter.GetBytes(Value2.Map).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.VirtualButton).CopyTo(Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.ButtonMappedCallback).CopyTo(Buffer, Offset);
    }
}
