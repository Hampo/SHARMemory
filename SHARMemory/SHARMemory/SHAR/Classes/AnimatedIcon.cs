using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs.AnimatedIcon;
using System;
namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVAnimatedIcon@@")]
public class AnimatedIcon : Class
{
    public const int MaxIcons = 100;

    [Flags]
    public enum FlagsEnum
    {
        OneShot = 1 << 0,
        Rendering = 1 << 1,
    }

    public AnimatedIcon(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint AnimatedIconVFTableOffset = 0;

    internal const uint DSGEntityOffset = AnimatedIconVFTableOffset + sizeof(uint);
    public AnimIconDSG DSGEntity => Memory.ClassFactory.Create<AnimIconDSG>(ReadUInt32(DSGEntityOffset));

    internal const uint AnimIconOffset = DSGEntityOffset + sizeof(uint);
    public Icon AnimIcon
    {
        get => ReadStruct<Icon>(AnimIconOffset);
        set => WriteStruct(AnimIconOffset, value);
    }

    internal const uint RenderLayerOffset = AnimIconOffset + Icon.Size;
    public Globals.RenderEnums.LayerEnum RenderLayer
    {
        get => (Globals.RenderEnums.LayerEnum)ReadInt32(RenderLayerOffset);
        set => WriteInt32(RenderLayerOffset, (int)value);
    }

    internal const uint FlagsOffset = RenderLayerOffset + sizeof(int);
    public FlagsEnum Flags
    {
        get => (FlagsEnum)ReadUInt32(FlagsOffset);
        set => WriteUInt32(FlagsOffset, (uint)value);
    }

    internal const uint AllocatedOffset = FlagsOffset + sizeof(uint);
    public byte Allocated
    {
        get => ReadByte(AllocatedOffset);
        set => WriteByte(AllocatedOffset, value);
    }

    public class AnimIconDSG : InstStatEntityDSG
    {
        public AnimIconDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        internal const uint VisibleOffset = ShadowMatrixOffset + sizeof(uint);
        public bool Visible
        {
            get => ReadBoolean(VisibleOffset);
            set => WriteBoolean(VisibleOffset, value);
        }

        internal const uint SlopeOffset = VisibleOffset + 4; // Padding
        public float Slope
        {
            get => ReadSingle(SlopeOffset);
            set => WriteSingle(SlopeOffset, value);
        }

        internal const uint MaxSizeOffset = SlopeOffset + sizeof(float);
        public float MaxSize
        {
            get => ReadSingle(MaxSizeOffset);
            set => WriteSingle(MaxSizeOffset, value);
        }

        internal const uint MinSizeOffset = MaxSizeOffset + sizeof(float);
        public float MinSize
        {
            get => ReadSingle(MinSizeOffset);
            set => WriteSingle(MinSizeOffset, value);
        }

        internal const uint NearDistOffset = MinSizeOffset + sizeof(float);
        public float NearDist
        {
            get => ReadSingle(NearDistOffset);
            set => WriteSingle(NearDistOffset, value);
        }

        internal const uint ScalingEnabledOffset = NearDistOffset + sizeof(float);
        public bool ScalingEnabled
        {
            get => ReadBoolean(ScalingEnabledOffset);
            set => WriteBoolean(ScalingEnabledOffset, value);
        }
    }
}
