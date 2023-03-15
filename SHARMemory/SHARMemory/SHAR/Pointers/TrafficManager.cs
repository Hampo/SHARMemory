using SHARMemory.Memory;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Pointers
{
    public class TrafficManager : Pointer
    {
        public TrafficManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8468, 0x6C8428, 0x6C8428, 0x6C8460)) { }

        public StructArray<SwatchColour> SwatchColours => new(Memory, Memory.SelectAddress(0x64A700, 0x64A6F0, 0x64A6F0, 0x64A700), SwatchColour.Size, 25);

        public bool TrafficEnabled
        {
            get => ReadBoolean(100);
            set => WriteBoolean(100, value);
        }
    }
}
