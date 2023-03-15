using SHARMemory.Memory;
using System.Drawing;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(SwatchColourStruct))]
    public struct SwatchColour
    {
        public const int Size = sizeof(int) * 3;

        public int R;
        public int G;
        public int B;

        public Color Colour
        {
            get => Color.FromArgb(R, G, B);
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public SwatchColour(Color colour)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
        }

        public SwatchColour(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public override string ToString() => Colour.ToString();
    }

    internal class SwatchColourStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new SwatchColour(Memory.ReadInt32(Address), Memory.ReadInt32(Address + sizeof(int)), Memory.ReadInt32(Address + sizeof(int) + sizeof(int)));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not SwatchColour Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(SwatchColour)}'.", nameof(Value));

            Memory.WriteInt32(Address, Value2.R);
            Memory.WriteInt32(Address + sizeof(int), Value2.G);
            Memory.WriteInt32(Address + sizeof(int) + sizeof(int), Value2.B);
        }
    }
}
