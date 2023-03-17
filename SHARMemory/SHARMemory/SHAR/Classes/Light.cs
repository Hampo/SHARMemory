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
            get => ReadUInt32(32);
            set => WriteUInt32(32, value);
        }

        public bool Active
        {
            get => ReadBoolean(36);
            set => WriteBoolean(36, value);
        }

        public bool Enabled
        {
            get => ReadBoolean(37);
            set => WriteBoolean(37, value);
        }

        public bool IsShadowCaster
        {
            get => ReadBoolean(38);
            set => WriteBoolean(38, value);
        }

        public bool Animated
        {
            get => ReadBoolean(39);
            set => WriteBoolean(3391, value);
        }

        public DecayRange DecayRange
        {
            get => ReadStruct<DecayRange>(40);
            set => WriteStruct(40, value);
        }

        public IlluminationTypes IlluminationType
        {
            get => (IlluminationTypes)ReadInt32(76);
            set => WriteInt32(76, (int)value);
        }
    }
}
