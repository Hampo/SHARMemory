using System.Drawing;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(d3dShaderInfoStruct))]
#pragma warning disable IDE1006 // Naming Styles
    public struct d3dShaderInfo
#pragma warning restore IDE1006 // Naming Styles
    {
        public const int Size = pddiColour.Size * 4 + sizeof(float);

        public pddiColour Diffuse;

        public pddiColour Specular;

        public pddiColour Ambient;

        public pddiColour Emissive;

        public float Shininess;

        public d3dShaderInfo(pddiColour diffuse, pddiColour specular, pddiColour ambient, pddiColour emissive, float shininess)
        {
            Diffuse = diffuse;
            Specular = specular;
            Ambient = ambient;
            Emissive = emissive;
            Shininess = shininess;
        }

        public override string ToString() => $"{Diffuse} | {Specular} | {Ambient} | {Emissive} | {Shininess}";
    }

#pragma warning disable IDE1006 // Naming Styles
    internal class d3dShaderInfoStruct : IStruct
#pragma warning restore IDE1006 // Naming Styles
    {
        public object Read(Memory Memory, uint Address) => new d3dShaderInfo(Memory.ReadStruct<pddiColour>(Address), Memory.ReadStruct<pddiColour>(Address + pddiColour.Size), Memory.ReadStruct<pddiColour>(Address + pddiColour.Size + pddiColour.Size), Memory.ReadStruct<pddiColour>(Address + pddiColour.Size + pddiColour.Size + pddiColour.Size), Memory.ReadSingle(Address + pddiColour.Size + pddiColour.Size + pddiColour.Size + pddiColour.Size));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not d3dShaderInfo Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(d3dShaderInfo)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.Diffuse);
            Memory.WriteStruct(Address + pddiColour.Size, Value2.Specular);
            Memory.WriteStruct(Address + pddiColour.Size + pddiColour.Size, Value2.Ambient);
            Memory.WriteStruct(Address + pddiColour.Size + pddiColour.Size + pddiColour.Size, Value2.Emissive);
            Memory.WriteSingle(Address + pddiColour.Size + pddiColour.Size + pddiColour.Size + pddiColour.Size, Value2.Shininess);
        }
    }
}