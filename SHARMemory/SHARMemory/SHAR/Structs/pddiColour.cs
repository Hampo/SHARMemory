using System.Drawing;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(pddiColourStruct))]
#pragma warning disable IDE1006 // Naming Styles
    public struct pddiColour
#pragma warning restore IDE1006 // Naming Styles
    {
        public const int Size = sizeof(int);

        public Color Colour;

        public pddiColour(Color colour)
        {
            Colour = colour;
        }

        public pddiColour(byte r, byte g, byte b, byte a = 255) : this(Color.FromArgb(a, r, g, b)) { }

        public pddiColour(int colour) : this(Color.FromArgb(colour)) { }

        public override string ToString() => Colour.ToString();
    }

#pragma warning disable IDE1006 // Naming Styles
    internal class pddiColourStruct : IStruct
#pragma warning restore IDE1006 // Naming Styles
    {
        public object Read(Memory Memory, uint Address) => new pddiColour(Memory.ReadInt32(Address));

        public void Write(Memory Memory, uint Address, object Value)
        {
            if (Value is not pddiColour Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(pddiColour)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.Colour.ToArgb());
        }
    }
}