﻿using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVtDrawablePose@@")]
    public class DrawablePose : Drawable
    {
        public DrawablePose(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public Skeleton Skeleton => Memory.ClassFactory.Create<Skeleton>(ReadUInt32(20));

        public Pose Pose => Memory.ClassFactory.Create<Pose>(ReadUInt32(24));
    }
}
