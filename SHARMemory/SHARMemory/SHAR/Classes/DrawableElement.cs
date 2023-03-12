using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class DrawableElement : Class
    {
        public enum Types
        {
            Base,
            Prop,
            Skin,
            Effect
        }

        public DrawableElement(Memory memory, uint address) : base(memory, address) { }

        public Types Type => (Types)ReadInt32(4);

        public T As<T>() where T : DrawableElement => Memory.CreateClass<T>(Address);
    }
}
