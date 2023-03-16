using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVTrafficBodyDrawable@@")]
    public class TrafficBodyDrawable : Drawable
    {
        public TrafficBodyDrawable(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public int FadeAlpha
        {
            get => ReadInt32(16);
            set => WriteInt32(16, value);
        }

        public bool Fading
        {
            get => ReadBoolean(20);
            set => WriteBoolean(20, value);
        }

        public Drawable BodyPropDrawable => Memory.ClassFactory.Create<Drawable>(ReadUInt32(24));

        public Shader BodyShader => Memory.ClassFactory.Create<Shader>(ReadUInt32(28));

        public Color DesiredColour
        {
            get => ReadStruct<Color>(32);
            set => WriteStruct(32, value);
        }
    }
}
