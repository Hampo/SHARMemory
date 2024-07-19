using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Drawing;
using static SHARMemory.SHAR.Globals;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVd3dSimpleShader@@")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Radical naming")]
public class d3dSimpleShader : d3dShader
{
    public d3dSimpleShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public d3dTexture Texture
    {
        get => Memory.ClassFactory.Create<d3dTexture>(ReadUInt32(92));
        set => WriteUInt32(92, value?.Address ?? 0);
    }

    public bool UseMultiCBV
    {
        get => ReadBoolean(96);
        set => WriteBoolean(96, value);
    }

    public Color MultiCBVBlendValue
    {
        get => ReadStruct<Color>(100);
        set => WriteStruct(100, value);
    }

    public Color MultiCBVBlendColour
    {
        get => ReadStruct<Color>(104);
        set => WriteStruct(104, value);
    }

    public pddiEnums.pddiMultiCBVBlendMode MultiCBVBlendMode
    {
        get => (pddiEnums.pddiMultiCBVBlendMode)ReadInt32(108);
        set => WriteInt32(108, (int)value);
    }

    public int MultiCBVBlendSetA
    {
        get => ReadInt32(112);
        set => WriteInt32(112, value);
    }

    public int MultiCBVBlendSetB
    {
        get => ReadInt32(116);
        set => WriteInt32(116, value);
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
