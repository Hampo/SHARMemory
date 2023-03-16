namespace SHARMemory.SHAR.Classes
{
    public class TrafficManager : Class
    {
        public TrafficManager(Memory memory, uint address) : base(memory, address) { }

        public bool TrafficEnabled
        {
            get => ReadBoolean(100);
            set => WriteBoolean(100, value);
        }
    }
}
