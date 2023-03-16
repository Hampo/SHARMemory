using System.Drawing;

namespace SHARMemory.SHAR.Classes
{
    public class DrawablePoseElement : DrawableElement
    {

        public DrawablePoseElement(Memory memory, uint address) : base(memory, address) { }

        public DrawablePose Skin => Memory.CreateClass<DrawablePose>(ReadUInt32(20));
    }
}
