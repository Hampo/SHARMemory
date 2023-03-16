using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class FeTextBible : Class
    {
        public override uint Address => base.Address - 0x34;

        public FeTextBible(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<FeLanguage> Languages => PointerArrayExtensions.FromVector<FeLanguage>(Memory, this, 20);

        public FeLanguage CurrentLanguage => Memory.Singletons.GameFlow.State is GameFlow.GameState.PreLicence or GameFlow.GameState.Licence ? null : Languages[Memory.Globals.FeTextBible.LanguageIndex];
    }
}
