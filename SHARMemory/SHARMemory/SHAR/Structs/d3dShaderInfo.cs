using SHARMemory.Memory;
using System;
using System.Drawing;

namespace SHARMemory.SHAR.Structs;

[Struct(typeof(d3dShaderInfoStruct))]
#pragma warning disable IDE1006 // Naming Styles
public struct d3dShaderInfo
#pragma warning restore IDE1006 // Naming Styles
{
    public const int Size = sizeof(int) * 4 + sizeof(float);

    public Color Diffuse;

    public Color Specular;

    public Color Ambient;

    public Color Emissive;

    public float Shininess;

    public d3dShaderInfo(Color diffuse, Color specular, Color ambient, Color emissive, float shininess)
    {
        Diffuse = diffuse;
        Specular = specular;
        Ambient = ambient;
        Emissive = emissive;
        Shininess = shininess;
    }

    public override readonly string ToString() => $"{Diffuse} | {Specular} | {Ambient} | {Emissive} | {Shininess}";
}

#pragma warning disable IDE1006 // Naming Styles
internal class d3dShaderInfoStruct : Struct
#pragma warning restore IDE1006 // Naming Styles
{
    public override int Size => d3dShaderInfo.Size;

    public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
    {
        Color Diffuse = Memory.StructFromBytes<Color>(Bytes, Offset);
        Offset += sizeof(int);
        Color Specular = Memory.StructFromBytes<Color>(Bytes, Offset);
        Offset += sizeof(int);
        Color Ambient = Memory.StructFromBytes<Color>(Bytes, Offset);
        Offset += sizeof(int);
        Color Emissive = Memory.StructFromBytes<Color>(Bytes, Offset);
        Offset += sizeof(int);
        float Shininess = BitConverter.ToSingle(Bytes, Offset);
        return new d3dShaderInfo(Diffuse, Specular, Ambient, Emissive, Shininess);
    }

    public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
    {
        if (Value is not d3dShaderInfo Value2)
            throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(d3dShaderInfo)}'.", nameof(Value));

        Memory.BytesFromStruct(Value2.Diffuse, Buffer, Offset);
        Offset += sizeof(int);
        Memory.BytesFromStruct(Value2.Specular, Buffer, Offset);
        Offset += sizeof(int);
        Memory.BytesFromStruct(Value2.Ambient, Buffer, Offset);
        Offset += sizeof(int);
        Memory.BytesFromStruct(Value2.Emissive, Buffer, Offset);
        Offset += sizeof(int);
        BitConverter.GetBytes(Value2.Shininess).CopyTo(Buffer, Offset);
    }
}