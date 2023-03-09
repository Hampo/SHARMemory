using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR.Pointers
{
    public class CharacterManager : Pointer
    {
        public CharacterManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8470, 0x6C8430, 0x6C8430, 0x6C8468)) { }

        public Character Characters(uint Index) => new Character(Memory, ReadUInt32(192u + Index * 4u));

        public Character Player => Characters(0);
    }
}
