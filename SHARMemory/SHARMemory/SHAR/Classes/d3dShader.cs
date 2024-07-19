using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using static SHARMemory.SHAR.Globals;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVd3dShader@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class d3dShader : pddiBaseShader
{
    public d3dShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public d3dContext Context => Memory.ClassFactory.Create<d3dContext>(ReadUInt32(16));

    public d3d D3D => Memory.ClassFactory.Create<d3d>(ReadUInt32(20));

    public pddiEnums.pddiBlendMode BlendMode
    {
        get => (pddiEnums.pddiBlendMode)ReadInt32(24);
        set => WriteInt32(24, (int)value);
    }

    public pddiEnums.pddiBlendFactor SrcBlend
    {
        get => (pddiEnums.pddiBlendFactor)ReadInt32(28);
        set => WriteInt32(28, (int)value);
    }

    public pddiEnums.pddiBlendFactor DestBlend
    {
        get => (pddiEnums.pddiBlendFactor)ReadInt32(32);
        set => WriteInt32(32, (int)value);
    }

    public bool AlphaTest
    {
        get => ReadBoolean(36);
        set => WriteBoolean(36, value);
    }

    public pddiEnums.pddiCompareMode AlphaCompare
    {
        get => (pddiEnums.pddiCompareMode)ReadInt32(40);
        set => WriteInt32(40, (int)value);
    }

    public float AlphaRef
    {
        get => ReadSingle(44);
        set => WriteSingle(44, value);
    }

    public pddiEnums.pddiTextureGen TextureGen
    {
        get => (pddiEnums.pddiTextureGen)ReadInt32(48);
        set => WriteInt32(48, (int)value);
    }

    public pddiEnums.pddiUVMode UVMode
    {
        get => (pddiEnums.pddiUVMode)ReadInt32(52);
        set => WriteInt32(52, (int)value);
    }

    public pddiEnums.pddiFilterMode FilterMode
    {
        get => (pddiEnums.pddiFilterMode)ReadInt32(56);
        set => WriteInt32(56, (int)value);
    }

    public bool IsLit
    {
        get => ReadBoolean(60);
        set => WriteBoolean(60, value);
    }

    public pddiEnums.pddiShadeMode ShadeMode
    {
        get => (pddiEnums.pddiShadeMode)ReadInt32(64);
        set => WriteInt32(64, (int)value);
    }

    public d3dShaderInfo ShaderInfo
    {
        get => ReadStruct<d3dShaderInfo>(68);
        set => WriteStruct(68, value);
    }

    public bool TwoSided
    {
        get => ReadBoolean(88);
        set => WriteBoolean(88, value);
    }

    public bool SkinMode
    {
        get => ReadBoolean(89);
        set => WriteBoolean(89, value);
    }

    public virtual void SetTexture(d3dTexture newTexture) { }
}
