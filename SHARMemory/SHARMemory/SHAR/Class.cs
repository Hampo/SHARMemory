using SHARMemory.Memory;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.Class</c> is an abstract class representing a single instance of a SHAR class.
    /// </summary>
    public abstract class Class : SHARMemory.Memory.Class
    {

        /// <summary>
        /// The <see cref="ProcessMemory"/> manager this pointer is linked to.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public new Memory Memory => (Memory)base.Memory;

        /// <summary>
        /// The <c>SHAR.Class</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> manager this class is linked to.
        /// </param>
        /// <param name="address">
        /// The base address of this class in memory.
        /// </param>
        public Class(Memory memory, uint address) : base(memory, address) {}
    }
}
