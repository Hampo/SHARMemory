using System.Drawing;

#pragma warning disable IDE1006 // Naming Styles
namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(pddiColourStruct))]
    public struct pddiColour
    {
        public const int Size = sizeof(int);

        public int ColourVal { get; private set; }

        public byte R
        {
            get => (byte)((ColourVal & 0x00FF0000) >> 16);
            set => ColourVal = (int)((ColourVal & 0xFF00FFFF) | (value << 16));
        }
        public byte G
        {
            get => (byte)((ColourVal & 0x0000FF00) >> 8);
            set => ColourVal = (int)((ColourVal & 0xFFFF00FF) | (value << 8));
        }
        public byte B
        {
            get => (byte)((ColourVal & 0x000000FF) >> 16);
            set => ColourVal = (int)((ColourVal & 0xFFFFFF00) | (value));
        }
        public byte A
        {
            get => (byte)((ColourVal & 0xFF000000) >> 24);
            set => ColourVal = (ColourVal & 0x00FFFFFF) | (value << 24);
        }

        public Color Colour
        {
            get => Color.FromArgb(ColourVal);
            set => ColourVal = value.ToArgb();
        }

        public pddiColour(int colour)
        {
            ColourVal = colour;
        }

        public pddiColour(Color colour)
        {
            ColourVal = colour.ToArgb();
        }

        public pddiColour(byte r, byte g, byte b, byte a = 255) : this(Color.FromArgb(a, r, g, b)) { }

        public override string ToString() => Colour.ToString();
    }

    internal class pddiColourStruct : IStruct
    {
        public object Read(Memory Memory, uint Address) => new pddiColour(Memory.ReadInt32(Address));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not pddiColour Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(pddiColour)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.ColourVal);
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles