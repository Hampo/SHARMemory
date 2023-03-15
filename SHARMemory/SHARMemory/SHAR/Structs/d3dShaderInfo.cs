using SHARMemory.Memory;
using System.Drawing;

namespace SHARMemory.SHAR.Structs
{
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

        public override string ToString() => $"{Diffuse} | {Specular} | {Ambient} | {Emissive} | {Shininess}";
    }

#pragma warning disable IDE1006 // Naming Styles
    internal class d3dShaderInfoStruct : IStruct
#pragma warning restore IDE1006 // Naming Styles
    {
        public object Read(ProcessMemory Memory, uint Address) => new d3dShaderInfo(Memory.ReadStruct<Color>(Address), Memory.ReadStruct<Color>(Address + sizeof(int)), Memory.ReadStruct<Color>(Address + sizeof(int) + sizeof(int)), Memory.ReadStruct<Color>(Address + sizeof(int) + sizeof(int) + sizeof(int)), Memory.ReadSingle(Address + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(int)));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not d3dShaderInfo Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(d3dShaderInfo)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.Diffuse);
            Memory.WriteStruct(Address + sizeof(int), Value2.Specular);
            Memory.WriteStruct(Address + sizeof(int) + sizeof(int), Value2.Ambient);
            Memory.WriteStruct(Address + sizeof(int) + sizeof(int) + sizeof(int), Value2.Emissive);
            Memory.WriteSingle(Address + sizeof(int) + sizeof(int) + sizeof(int) + sizeof(int), Value2.Shininess);
        }
    }
}