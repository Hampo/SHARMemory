using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Pointers
{
    public class InteriorManager : Pointer
    {
        public enum InteriorStates : uint
        {
            None,
            Enter,
            Exit,
            Inside
        }

        public InteriorManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8FF8, 0x6C8FB8, 0x6C8FB8, 0x6C8FF0)) { }

        public InteriorStates InteriorState => (InteriorStates)ReadUInt32(4);
    }
}
