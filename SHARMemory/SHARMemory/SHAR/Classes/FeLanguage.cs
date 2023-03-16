using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVFeLanguage@@")]
    public class FeLanguage : Class
    {
        public FeLanguage(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public char LanguageID => (char)ReadByte(16);

        public uint Modulo => ReadUInt32(20);

        public uint BufferSize => ReadUInt32(24);

        public uint NumStrings => ReadUInt32(28);

        public StructArray<uint> Hashes => new(Memory, ReadUInt32(32), sizeof(uint), NumStrings);

        public StructArray<uint> Offsets => new(Memory, ReadUInt32(36), sizeof(uint), NumStrings);

        public StructArray<ushort> Buffer => new(Memory, ReadUInt32(40), sizeof(ushort), BufferSize / 2);

        private uint? GetOffset(uint hash)
        {
            for (uint i = 0; i < NumStrings; i++)
                if (Hashes[i] == hash)
                    return Offsets[i] / 2;

            return null;
        }

        public string GetString(uint hash)
        {
            uint? offset = GetOffset(hash);
            if (!offset.HasValue)
                return null;

            List<ushort> data = new();
            while (true)
            {
                ushort c = Buffer[offset.Value];
                if (c == 0) break;

                data.Add(c);
                offset++;
            }

            byte[] asBytes = new byte[data.Count * sizeof(ushort)];
            System.Buffer.BlockCopy(data.ToArray(), 0, asBytes, 0, asBytes.Length);
            return Encoding.Unicode.GetString(asBytes);
        }

        public string GetString(string name) => GetString(GetHash(name));

        public bool SetString(uint hash, string value)
        {
            uint? offset = GetOffset(hash);
            if (!offset.HasValue)
                return false;

            uint maxLength = 0;
            bool foundNull = false;
            while (offset.Value + maxLength < Buffer.Count)
            {
                ushort c = Buffer[offset.Value + maxLength];
                if (c == 0)
                    foundNull = true;
                else if (foundNull)
                {
                    maxLength--;
                    break;
                }
                maxLength++;
            }

            if (value.Length > maxLength)
                throw new ArgumentException($"{nameof(value)} cannot be of a higher length than {maxLength}'", nameof(value));

            ushort[] chars = value.Select(c => (ushort)c).ToArray();

            for (int i = 0; i < maxLength; i++)
            {
                Buffer[(uint)(offset.Value + i)] = (ushort)(i < chars.Length ? chars[i] : 0);
            }
            return true;
        }

        public bool SetString(string name, string value) => SetString(GetHash(name), value);

        public uint GetHash(string name)
        {
            uint Hash = 0;

            foreach (char c in name)
                Hash = ((byte)c + (Hash << 6)) % Modulo;

            return Hash;
        }
    }
}
