using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtLight@@")]
    public class Light : Class
    {
        public enum IlluminationTypes
        {
            Positive,
            Zero,
            Negative,
        }

        public Light(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Color Colour
        {
            get => ReadStruct<Color>(16);
            set => WriteStruct(16, value);
        }

        public Vector3 Position
        {
            get => ReadStruct<Vector3>(20);
            set => WriteStruct(20, value);
        }

        public uint Slot
        {
            get => ReadUInt32(24);
            set => WriteUInt32(24, value);
        }

        public bool Active
        {
            get => ReadBoolean(28);
            set => WriteBoolean(28, value);
        }

        public bool Enabled
        {
            get => ReadBoolean(29);
            set => WriteBoolean(29, value);
        }

        public bool IsShadowCaster
        {
            get => ReadBoolean(30);
            set => WriteBoolean(30, value);
        }

        public bool Animated
        {
            get => ReadBoolean(31);
            set => WriteBoolean(31, value);
        }

        public DecayRange DecayRange
        {
            get => ReadStruct<DecayRange>(32);
            set => WriteStruct(32, value);
        }

        public IlluminationTypes IlluminationType
        {
            get => (IlluminationTypes)ReadInt32(68);
            set => WriteInt32(68, (int)value);
        }
    }
}
