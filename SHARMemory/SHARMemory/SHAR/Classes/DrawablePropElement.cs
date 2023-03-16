namespace SHARMemory.SHAR.Classes
{
    public class DrawablePropElement : DrawableElement
    {

        public DrawablePropElement(Memory memory, uint address) : base(memory, address) { }

        public Drawable Drawable => Memory.CreateClass<Drawable>(ReadUInt32(20));
    }
}
