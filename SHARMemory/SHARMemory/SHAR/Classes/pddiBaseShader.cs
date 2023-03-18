using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVpddiBaseShader@@")]
#pragma warning disable IDE1006 // Naming Styles
    public class pddiBaseShader : pddiShader
#pragma warning restore IDE1006 // Naming Styles
    {
        public pddiBaseShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public uint UID
        {
            get => ReadUInt32(12);
            set => WriteUInt32(12, value);
        }
    }
}
