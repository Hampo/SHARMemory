using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVCD3DSphereMapShader@@")]
    public class CD3DSphereMapShader : d3dShader
    {
        public CD3DSphereMapShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public pddiTexture Texture
        {
            get => Memory.ClassFactory.Create<pddiTexture>(ReadUInt32(96));
            set => WriteUInt32(96, value?.Address ?? 0);
        }

        public pddiTexture ReflectionTexture
        {
            get => Memory.ClassFactory.Create<pddiTexture>(ReadUInt32(100));
            set => WriteUInt32(100, value?.Address ?? 0);
        }

        public Color EnvironmentColour
        {
            get => ReadStruct<Color>(104);
            set => WriteStruct(104, value);
        }

        public Vector3 Rotation
        {
            get => ReadStruct<Vector3>(108);
            set => WriteStruct(108, value);
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
}
