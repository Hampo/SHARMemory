using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtDirectionalLight@@")]
public class DirectionalLight : Light
{
    public DirectionalLight(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public Vector3 Direction
    {
        get => ReadStruct<Vector3>(80);
        set => WriteStruct(80, value);
    }
}
