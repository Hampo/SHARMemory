using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVpddiShader@@")]
#pragma warning disable IDE1006 // Naming Styles
public class pddiShader : pddiObject
#pragma warning restore IDE1006 // Naming Styles
{
    public pddiShader(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }
}
