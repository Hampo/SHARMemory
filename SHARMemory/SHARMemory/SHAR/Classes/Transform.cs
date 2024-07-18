using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

public class Transform : Class
{
    public enum DirtyEnum
    {
        None,
        Matrix,
        Quaternion
    }

    public Transform(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public Matrix4x4 Matrix
    {
        get => ReadStruct<Matrix4x4>(0);
        set => WriteStruct(0, value);
    }

    public Quaternion Quaternion
    {
        get => ReadStruct<Quaternion>(64);
        set => WriteStruct(64, value);
    }

    public DirtyEnum Dirty
    {
        get => (DirtyEnum)ReadInt32(80);
        set => WriteInt32(80, (int)value);
    }
}
