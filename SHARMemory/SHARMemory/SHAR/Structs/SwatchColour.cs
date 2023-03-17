using SHARMemory.Memory;
using System;
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

    internal class SwatchColourStruct : Struct
    {
        public override int Size => SwatchColour.Size;

        public override object FromBytes(ProcessMemory Memory, byte[] Bytes, int Offset = 0)
        {
            int R = BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(int);
            int G = BitConverter.ToInt32(Bytes, Offset);
            Offset += sizeof(int);
            int B = BitConverter.ToInt32(Bytes, Offset);
            return new SwatchColour(R, G, B);
        }

        public override void ToBytes(ProcessMemory Memory, object Value, byte[] Buffer, int Offset = 0)
        {
            if (Value is not SwatchColour Value2)
                throw new ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(SwatchColour)}'.", nameof(Value));

            BitConverter.GetBytes(Value2.R).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
            BitConverter.GetBytes(Value2.G).CopyTo(Buffer, Offset);
            Offset += sizeof(int);
            BitConverter.GetBytes(Value2.B).CopyTo(Buffer, Offset);
        }
    }
}
