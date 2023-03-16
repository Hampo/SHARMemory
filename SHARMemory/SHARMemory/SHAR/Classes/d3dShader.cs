using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVd3dShader@@")]
#pragma warning disable IDE1006 // Naming Styles
    public class d3dShader : Class
#pragma warning restore IDE1006 // Naming Styles
    {
        public d3dShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public d3dShaderInfo ShaderInfo
        {
            get => ReadStruct<d3dShaderInfo>(68);
            set => WriteStruct(68, value);
        }
    }
}
