namespace SHARMemory.Memory
{
    /// <summary>
    /// Handles a <c>Singleton</c> in a process's memory.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="Class"/> that is this is a singleton of.
    /// </typeparam>
    public class Singleton<T> where T : Class
    {
        private readonly ProcessMemory Memory;
        private readonly uint PointerAddress;
        private T Instance = null;

        /// <summary>
        /// The <c>Memory.Singleton</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// A <see cref="ProcessMemory"/> that the <typeparamref name="T"/> is in.
        /// </param>
        /// <param name="pointerAddress">
        /// 
        /// </param>
        public Singleton(ProcessMemory memory, uint pointerAddress)
        {
            Memory = memory;
            PointerAddress = pointerAddress;
        }

        /// <summary>
        /// Attempts to get the singleton.
        /// If <see cref="Instance"/> has a value, it will return.
        /// If <see cref="Instance"/> is <c>null</c>, it will attempt to retrieve from memory.
        /// </summary>
        /// <returns>
        /// Returns the single instance of <typeparamref name="T"/>, or <c>null</c>.
        /// </returns>
        public T Get()
        {
            Instance ??= Memory.ClassFactory.Create<T>(Memory.ReadUInt32(PointerAddress));
            return Instance;
        }
    }
}