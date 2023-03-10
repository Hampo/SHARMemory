using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class TrafficManager : Pointer
    {

        public TrafficManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8468, 0x6C8428, 0x6C8428, 0x6C8460)) { }

        public SwatchColour SwatchColours(uint index) => new SwatchColour(Memory, Memory.SelectAddress(0x64A700, 0x64A6F0, 0x64A6F0, 0x64A700) + index * 12);

        public bool TrafficEnabled
        {
            get => ReadByte(100) != 0;
            set => WriteByte(100, (byte)(value ? 1 : 0));
        }
    }
}
