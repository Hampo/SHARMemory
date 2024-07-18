namespace SHARMemory.SHAR;

public partial class Globals
{
    public sealed class FeTextBibleHandler
    {
        private readonly Memory Memory;

        public uint LanguageIndex => Memory.ReadUInt32(Memory.SelectAddress(0x65C7B0, 0x65C770, 0x65C770, 0x6C6F3C));

        internal FeTextBibleHandler(Memory memory)
        {
            Memory = memory;
        }
    }
}