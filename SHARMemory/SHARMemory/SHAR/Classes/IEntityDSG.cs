namespace SHARMemory.SHAR.Classes
{
    public class IEntityDSG : Drawable
    {
        public IEntityDSG(Memory memory, uint address) : base(memory, address) { }

        public float Rank
        {
            get => ReadSingle(0);
            set => WriteSingle(0, value);
        }

        public bool Translucent
        {
            get => ReadBoolean(4);
            set => WriteBoolean(4, value);
        }

        // TODO: Implement the name bullshit for shader name (8)

        // TODO: SpatialNode? (12)
    }
}
