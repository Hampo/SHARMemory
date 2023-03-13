using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class FeTextBible : Pointer
    {
        public override uint Value => base.Value - 0x34;

        public FeTextBible(Memory memory) : base(memory, memory.SelectAddress(0x6C8944, 0x6C8904, 0x6C8904, 0x6C893C)) { }

        public uint LanguageIndex => Memory.ReadUInt32(Memory.SelectAddress(0x65C7B0, 0x65C770, 0x65C770, 0x6C6F3C));

        public PointerArray<FeLanguage> Languages => PointerArray<FeLanguage>.FromVector(Memory, this, 20);

        public FeLanguage CurrentLanguage => Memory.GameFlow.State == GameFlow.GameState.PreLicence || Memory.GameFlow.State == GameFlow.GameState.Licence ? null : Languages[LanguageIndex];
    }
}
