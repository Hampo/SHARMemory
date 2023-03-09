using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class SwatchColour : Class
    {
        public SwatchColour(Memory memory, uint address) : base(memory, address) { }

        public int R
        {
            get => ReadInt32(0);
            set => WriteInt32(0, value);
        }

        public int G
        {
            get => ReadInt32(4);
            set => WriteInt32(4, value);
        }

        public int B
        {
            get => ReadInt32(8);
            set => WriteInt32(8, value);
        }

        public Color Colour
        {
            get => Color.FromArgb((byte)R, (byte)G, (byte)B);
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }
    }
}
