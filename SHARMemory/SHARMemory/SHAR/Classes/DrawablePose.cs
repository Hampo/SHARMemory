using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVtDrawablePose@@")]
public class DrawablePose : Drawable
{
    public DrawablePose(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint tPosableVFTableOffset = NameOffset + sizeof(long);

    internal const uint SkeletonOffset = tPosableVFTableOffset + sizeof(uint);
    public Skeleton Skeleton => Memory.ClassFactory.Create<Skeleton>(ReadUInt32(SkeletonOffset));

    internal const uint PoseOffset = SkeletonOffset + sizeof(uint);
    public Pose Pose => Memory.ClassFactory.Create<Pose>(ReadUInt32(PoseOffset));
}
