using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

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
        /// <param name="completeObjectLocator">
        /// The <see cref="CompleteObjectLocator"/> of this class.
        /// </param>
        public Class(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) {}
    }
}
