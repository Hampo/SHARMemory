using SHARMemory.Memory;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR
{
    public partial class Globals
    {
        public sealed class TrafficManagerHandler
        {
            private readonly Memory Memory;

            public StructArray<SwatchColour> SwatchColours => new(Memory, Memory.SelectAddress(0x64A700, 0x64A6F0, 0x64A6F0, 0x64A700), SwatchColour.Size, 25);

            internal TrafficManagerHandler(Memory memory)
            {
                Memory = memory;
            }
        }
    }
}