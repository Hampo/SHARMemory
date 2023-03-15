using SHARMemory.Memory;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.Pointer</c> is an abstract class representing a single instance of a SHAR pointer.
    /// </summary>
    public abstract class Pointer : SHARMemory.Memory.Pointer
    {

        /// <summary>
        /// The <see cref="ProcessMemory"/> manager this pointer is linked to.
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public new Memory Memory => (Memory)base.Memory;

        /// <summary>
        /// The <c>SHAR.Pointer</c> constructor.
        /// </summary>
        /// <param name="memory">
        /// The <see cref="SHAR.Memory"/> manager this pointer is linked to.
        /// </param>
        /// <param name="address">
        /// The base address of this pointer in memory.
        /// </param>
        public Pointer(Memory memory, uint address) : base(memory, address) {}
    }
}
