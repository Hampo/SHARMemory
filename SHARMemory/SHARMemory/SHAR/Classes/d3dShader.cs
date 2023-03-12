using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
#pragma warning disable IDE1006 // Naming Styles
    public class d3dShader : Class
#pragma warning restore IDE1006 // Naming Styles
    {
        public d3dShader(Memory memory, uint address) : base(memory, address) { }

        public d3dShaderInfo ShaderInfo
        {
            get => ReadStruct<d3dShaderInfo>(68);
            set => WriteStruct(68, value);
        }
    }
}
