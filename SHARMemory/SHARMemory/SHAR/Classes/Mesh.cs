﻿using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtGeometry@@")]
public class Mesh : Drawable
{
    public Mesh(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint BoxOffset = NameOffset + sizeof(long);
    public Box3D Box
    {
        get => ReadStruct<Box3D>(BoxOffset);
        set => WriteStruct(BoxOffset, value);
    }

    internal const uint SphereOffset = BoxOffset + Box3D.Size;
    public Sphere Sphere
    {
        get => ReadStruct<Sphere>(SphereOffset);
        set => WriteStruct(SphereOffset, value);
    }

    internal const uint CastsShadowOffset = SphereOffset + Sphere.Size;
    public int CastsShadow
    {
        get => ReadInt32(CastsShadowOffset);
        set => WriteInt32(CastsShadowOffset, value);
    }

    internal const uint PrimGroupsOffset = CastsShadowOffset + sizeof(int);
    public PointerArray<PrimGroup> PrimGroups => PointerArrayExtensions.FromPtrArray<PrimGroup>(Memory, this, PrimGroupsOffset);
}
