using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    public class TrafficBodyDrawable : Class
    {
        public TrafficBodyDrawable(Memory memory, uint address) : base(memory, address) { }

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

        // tDrawable mBodyPropDrawable (24)

        // tShader mBodyShader (28)

        public pddiColour DesiredColour
        {
            get => ReadStruct<pddiColour>(32);
            set => WriteStruct(32, value);
        }
    }
}
