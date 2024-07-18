using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Drawing;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVCD3DRefractShader@@")]
public class CD3DRefractShader : d3dShader
{
    public CD3DRefractShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public pddiTexture Texture
    {
        get => Memory.ClassFactory.Create<pddiTexture>(ReadUInt32(96));
        set => WriteUInt32(96, value?.Address ?? 0);
    }

    public d3dTexture ScreenCaptureTexture
    {
        get => Memory.ClassFactory.Create<d3dTexture>(ReadUInt32(100));
        set => WriteUInt32(100, value?.Address ?? 0);
    }

    public float REFI
    {
        get => ReadSingle(104);
        set => WriteSingle(104, value);
    }

    public float REFB
    {
        get => ReadSingle(108);
        set => WriteSingle(108, value);
    }

    public Color Colour
    {
        get => ReadStruct<Color>(112);
        set => WriteStruct(112, value);
    }

    public override void SetTexture(d3dTexture newTexture)
    {
        if (newTexture == null)
            return;

        pddiTexture oldTexture = Texture;
        oldTexture.RefCount--;

        newTexture.RefCount++;
        Texture = newTexture;
    }
}
