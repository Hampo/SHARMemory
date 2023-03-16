namespace SHARMemory.SHAR.Classes
{
    public class InteriorManager : Class
    {
        public enum InteriorStates : uint
        {
            None,
            Enter,
            Exit,
            Inside
        }

        public InteriorManager(Memory memory, uint address) : base(memory, address) { }

        public InteriorStates InteriorState => (InteriorStates)ReadUInt32(4);
    }
}
