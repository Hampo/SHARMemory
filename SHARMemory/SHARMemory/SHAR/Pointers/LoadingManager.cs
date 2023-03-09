using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARMemory.SHAR.Pointers
{
    public class LoadingManager : Pointer
    {
        public LoadingManager(Memory memory) : base(memory, memory.SelectAddress(0x6C8FF4, 0x6C8FB4, 0x6C8FB4, 0x6C8FEC)) { }

        public uint RequestHead => ReadUInt32(0x73A4);

        public uint RequestTail => ReadUInt32(0x73A8);

        public bool IsLoading => ReadByte(0x73AC) != 0;

        public bool IsCancellingLoads => ReadByte(0x73AD) != 0;
    }
}
