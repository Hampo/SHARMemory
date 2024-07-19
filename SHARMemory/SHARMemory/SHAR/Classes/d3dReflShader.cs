using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Drawing;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVd3dReflShader@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class d3dReflShader : d3dShader
{
    public d3dReflShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public d3dTexture Texture
    {
        get => Memory.ClassFactory.Create<d3dTexture>(ReadUInt32(92));
        set => WriteUInt32(92, value?.Address ?? 0);
    }

    public d3dTexture ReflectionMap
    {
        get => Memory.ClassFactory.Create<d3dTexture>(ReadUInt32(96));
        set => WriteUInt32(96, value?.Address ?? 0);
    }

    public Color EnvironmentBlend
    {
        get => ReadStruct<Color>(100);
        set => WriteStruct(100, value);
    }

    public override void SetTexture(d3dTexture newTexture)
    {
        if (newTexture == null)
            return;

        d3dTexture oldTexture = Texture;
        oldTexture.RefCount--;

        newTexture.RefCount++;
        Texture = newTexture;
    }
}
