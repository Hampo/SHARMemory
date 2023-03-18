using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVFeTextBible@@")]
    public class FeTextBible : Class
    {
        public FeTextBible(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public PointerArray<FeLanguage> Languages => PointerArrayExtensions.FromVector<FeLanguage>(Memory, this, 20);

        public FeLanguage CurrentLanguage => Memory.Singletons.GameFlow.State is GameFlow.GameState.PreLicence or GameFlow.GameState.Licence || Languages.Count < 1 ? null : Languages[(int)Memory.Globals.FeTextBible.LanguageIndex];
    }
}
