namespace SHARMemory.SHAR.Classes
{
    public class Drawable : Class
    {
        public enum Types
        {
            Mesh,
            Unknown
        }

        public Drawable(Memory memory, uint address) : base(memory, address) { }

        public Types Type
        {
            get
            {
                uint VFTableAddress = ReadUInt32(0);
                if (VFTableAddress == Memory.SelectAddress(0x5FA8F4, 0x5FA8EC, 0x5FA8E4, 0x5FA8E4))
                    return Types.Mesh;

                return Types.Unknown;
            }
        }

        public T As<T>() where T : Drawable => Memory.CreateClass<T>(Address);
    }
}
