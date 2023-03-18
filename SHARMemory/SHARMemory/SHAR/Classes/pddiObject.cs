using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVpddiObject@@")]
#pragma warning disable IDE1006 // Naming Styles
    public class pddiObject : Class
#pragma warning restore IDE1006 // Naming Styles
    {
        public pddiObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public int RefCount
        {
            get => ReadInt32(4);
            set => WriteInt32(4, value);
        }

        public int LastError
        {
            get => ReadInt32(8);
            set => WriteInt32(8, value);
        }
    }
}
