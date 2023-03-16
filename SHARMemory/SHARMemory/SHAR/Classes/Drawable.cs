namespace SHARMemory.SHAR.Classes
{
    public class Drawable : Class
    {
        public enum Types
        {
            BillboardQuadGroup,
            Mesh,
            TrafficBodyDrawable,
            PolySkin,
            Unknown
        }

        public Drawable(Memory memory, uint address) : base(memory, address) { }

        public Types Type
        {
            get
            {
                uint VFTableAddress = ReadUInt32(0);
                if (VFTableAddress == Memory.SelectAddress(0x5FA8F4, 0x5FA8EC, 0x5FA8E4, 0x5F744C))
                    return Types.Mesh;
                if (VFTableAddress == Memory.SelectAddress(0x5FA664, 0x5FA65C, 0x5FA654, 0x5F719C))
                    return Types.BillboardQuadGroup;
                if (VFTableAddress == Memory.SelectAddress(0x60849C, 0x60848C, 0x60847C, 0x6084C4))
                    return Types.TrafficBodyDrawable;
                if (VFTableAddress == Memory.SelectAddress(0x5F96DC, 0, 0, 0))
                    return Types.PolySkin;

                return Types.Unknown;
            }
        }

        public T ReinterpretCast<T>() where T : Drawable => Memory.CreateClass<T>(Address);
    }
}
