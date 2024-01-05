using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVPresentationEvent@@")]
    public class PresentationEvent : Class
    {
        public PresentationEvent(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        private byte Bitfield_0x4
        {
            get => ReadByte(4);
            set => WriteByte(4, value);
        }

        public bool AutoPlay
        {
            get => (Bitfield_0x4 & 0b00000001) != 0;
            set
            {
                if (value)
                    Bitfield_0x4 |= 0b00000001;
                else
                    Bitfield_0x4 &= 0b11111110;
            }
        }

        public bool ClearWhenDone
        {
            get => (Bitfield_0x4 & 0b00000010) != 0;
            set
            {
                if (value)
                    Bitfield_0x4 |= 0b00000010;
                else
                    Bitfield_0x4 &= 0b11111101;
            }
        }

        public bool Loaded
        {
            get => (Bitfield_0x4 & 0b00000100) != 0;
            set
            {
                if (value)
                    Bitfield_0x4 |= 0b00000100;
                else
                    Bitfield_0x4 &= 0b11111011;
            }
        }

        public bool KeepLayersFrozen
        {
            get => (Bitfield_0x4 & 0b00001000) != 0;
            set
            {
                if (value)
                    Bitfield_0x4 |= 0b00001000;
                else
                    Bitfield_0x4 &= 0b11110111;
            }
        }

        public bool IsSkippable
        {
            get => (Bitfield_0x4 & 0b00010000) != 0;
            set
            {
                if (value)
                    Bitfield_0x4 |= 0b00010000;
                else
                    Bitfield_0x4 &= 0b11101111;
            }
        }
    }
}
