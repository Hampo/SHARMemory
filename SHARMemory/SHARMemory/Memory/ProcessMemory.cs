﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace SHARMemory.Memory;

/// <summary>
/// Class <c>ProcessMemory</c> is a base class for reading and writing to a <c>Process</c>'s memory.
/// Initially created by Lucas Cardellini, further developed by Proddy.
/// </summary>
public class ProcessMemory : IDisposable
{
    /// <summary>
    /// How many bytes per block when using <see cref="AllocateMemoryBlock"/> or <see cref="AllocateMemoryBlocks(uint)"/>.
    /// </summary>
    public const uint BYTES_PER_BLOCK = 4096;

    /// <summary>
    /// The <see cref="System.Diagnostics.Process"/> that is being managed.
    /// </summary>
    public Process Process { get; }

    /// <summary>
    /// The <see cref="ClassFactory"/> of this <see cref="ProcessMemory"/>.
    /// </summary>
    public readonly ClassFactory ClassFactory;

    /// <summary>
    /// A <c>readonly</c> instance of <see cref="Structs"/>. Used for <see cref="Structs.Known"/>.
    /// </summary>
    public readonly Structs Structs = new();

    internal const int PROCESS_CREATE_THREAD = 0x0002;
    internal const int PROCESS_QUERY_INFORMATION = 0x0400;
    internal const int PROCESS_VM_OPERATION = 0x0008;
    internal const int PROCESS_VM_WRITE = 0x0020;
    internal const int PROCESS_VM_READ = 0x0010;

    internal const uint MEM_COMMIT = 0x00001000;
    internal const uint MEM_RESERVE = 0x00002000;
    internal const uint MEM_DECOMMIT = 0x00004000;
    internal const uint MEM_RELEASE = 0x00008000;
    internal const uint PAGE_READWRITE = 4;

    [Flags]
    internal enum LoadLibraryFlags : uint
    {
        DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
        LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
        LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000
    }

    /*[Flags]
    internal enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    internal enum MemoryProtection
    {
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }*/


    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool VirtualProtectEx(IntPtr hProcess, UIntPtr lpAddress, int dwSize, int flNewProtect, [Out] int lpflOldProtect);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, [Out] byte[] lpBuffer, IntPtr nSize, UIntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr LoadLibraryEx(string dllToLoad, IntPtr hFile, LoadLibraryFlags flags);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern void FreeLibrary(IntPtr module);

    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
    internal extern static UIntPtr GetProcAddressOrdinal(IntPtr hwnd, UIntPtr procedureName);

    [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
    internal extern static UIntPtr GetProcAddress(IntPtr hwnd, string procedureName);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out UIntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

    //[DllImport("kernel32.dll", SetLastError = true)]
    //internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool GetExitCodeThread(IntPtr hHandle, out uint lpExitCode);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool CloseHandle(IntPtr handle);

    /// <summary>
    /// The <c>ProcessMemory</c> constructor.
    /// </summary>
    /// <param name="Process">
    /// The <see cref="System.Diagnostics.Process"/> that you want to manage the memory of.
    /// </param>
    public ProcessMemory(Process Process)
    {
        this.Process = Process;

        ClassFactory = new(this);
    }

    /// <summary>
    /// Get if <see cref="Process"/> is still running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            try
            {
                return !Process.HasExited;
            }
            catch { return false; }
        }
    }

    /// <summary>
    /// Reads the memory of <see cref="Process"/>.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <param name="Buffer">
    /// The buffer to read to.
    /// </param>
    /// <param name="Read">
    /// The number of bytes read.
    /// </param>
    /// <exception cref="Win32Exception">
    /// Throws an exception if <see cref="ReadProcessMemory(IntPtr, UIntPtr, byte[], IntPtr, UIntPtr)"/> fails.
    /// </exception>
    public void Read(uint Address, byte[] Buffer, out uint Read)
    {
        UIntPtr lpNumberOfBytesRead = default;
        if (!ReadProcessMemory(Process.Handle, new UIntPtr(Address), Buffer, new IntPtr(Buffer.Length), lpNumberOfBytesRead))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        Read = lpNumberOfBytesRead.ToUInt32();
    }

    /// <summary>
    /// Writes to the memory of <see cref="Process"/>.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Buffer">
    /// The buffer to write.
    /// </param>
    /// <param name="Written">
    /// The number of bytes written.
    /// </param>
    /// <exception cref="Win32Exception">
    /// Throws an exception if <see cref="WriteProcessMemory(IntPtr, UIntPtr, byte[], IntPtr, out UIntPtr)"/> fails.
    /// </exception>
    public void Write(uint Address, byte[] Buffer, out uint Written)
    {
        IntPtr intPtr = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_WRITE, bInheritHandle: false, Process.Id);
        if (intPtr == IntPtr.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        try
        {
            int num = default;
            bool flag = VirtualProtectEx(intPtr, new UIntPtr(Address), Buffer.Length, 4, num);
            try
            {
                if (!WriteProcessMemory(Process.Handle, new UIntPtr(Address), Buffer, new IntPtr(Buffer.Length), out UIntPtr lpNumberOfBytesWritten))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                Written = lpNumberOfBytesWritten.ToUInt32();
            }
            finally
            {
                if (flag)
                    VirtualProtectEx(intPtr, new UIntPtr(Address), Buffer.Length, num, num);
            }
        }
        finally
        {
            CloseHandle(intPtr);
        }
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>byte</c> at the given address.
    /// </returns>
    public byte ReadByte(uint Address)
    {
        byte[] array = new byte[1];
        Read(Address, array, out _);
        return array[0];
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>bool</c> at the given address.
    /// </returns>
    public bool ReadBoolean(uint Address) => ReadByte(Address) != 0;

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <param name="Length">
    /// The number of bytes to read.
    /// </param>
    /// <returns>
    /// The <c>byte[]</c> at the given address.
    /// </returns>
    public byte[] ReadBytes(uint Address, uint Length)
    {
        checked
        {
            byte[] array = new byte[Length];
            Read(Address, array, out _);
            return array;
        }
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>double</c> at the given address.
    /// </returns>
    public double ReadDouble(uint Address)
    {
        byte[] array = new byte[8];
        Read(Address, array, out _);
        return BitConverter.ToDouble(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>float</c> at the given address.
    /// </returns>
    public float ReadSingle(uint Address)
    {
        byte[] array = new byte[4];
        Read(Address, array, out _);
        return BitConverter.ToSingle(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>short</c> at the given address.
    /// </returns>
    public short ReadInt16(uint Address)
    {
        byte[] array = new byte[2];
        Read(Address, array, out _);
        return BitConverter.ToInt16(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>int</c> at the given address.
    /// </returns>
    public int ReadInt32(uint Address)
    {
        byte[] array = new byte[4];
        Read(Address, array, out _);
        return BitConverter.ToInt32(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>long</c> at the given address.
    /// </returns>
    public long ReadInt64(uint Address)
    {
        byte[] array = new byte[8];
        Read(Address, array, out _);
        return BitConverter.ToInt64(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>ushort</c> at the given address.
    /// </returns>
    public ushort ReadUInt16(uint Address)
    {
        byte[] array = new byte[4];
        Read(Address, array, out _);
        return BitConverter.ToUInt16(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>uint</c> at the given address.
    /// </returns>
    public uint ReadUInt32(uint Address)
    {
        byte[] array = new byte[4];
        Read(Address, array, out _);
        return BitConverter.ToUInt32(array, 0);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>ulong</c> at the given address.
    /// </returns>
    public ulong ReadUInt64(uint Address)
    {
        byte[] array = new byte[4];
        Read(Address, array, out _);
        return BitConverter.ToUInt64(array, 0);
    }

    /// <summary>
    /// Returns <paramref name="String"/> null terminated.
    /// </summary>
    /// <param name="String">
    /// The <c>string</c> to null terminate
    /// </param>
    /// <returns>
    /// <paramref name="String"/> up to the first <c>null</c> character.
    /// </returns>
    public static string NullTerminate(string String)
    {
        int num = String.IndexOf('\0');
        if (num == -1)
            return String;

        return String.Substring(0, num);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <param name="Encoding">
    /// The character encoding to use.
    /// </param>
    /// <param name="maxLength">
    /// The maximum length of the string. Default to <c>512</c>.
    /// </param>
    /// <returns>
    /// The <c>string</c> at the given address.
    /// </returns>
    public string ReadString(uint Address, Encoding Encoding, uint maxLength = 512) => NullTerminate(Encoding.GetString(ReadBytes(Address, maxLength)));

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <param name="Encoding">
    /// The character encoding to use.
    /// </param>
    /// <returns>
    /// The <c>string</c> at the given address.
    /// </returns>
    public string ReadNullString(uint Address, Encoding Encoding)
    {
        List<byte> bytes = [];
        while (true)
        {
            byte b = ReadByte(Address);
            if (b == 0)
                break;
            bytes.Add(b);
            Address++;
        }
        return Encoding.GetString([.. bytes]);
    }

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <param name="Encoding">
    /// The character encoding to use.
    /// </param>
    /// <returns>
    /// The <c>string</c> pointer at the given address.
    /// </returns>
    public string ReadNullStringPointer(uint Address, Encoding Encoding) => ReadNullString(ReadUInt32(Address), Encoding);

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Type">
    /// The type to read.
    /// </param>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <paramref name="Type"/> at the given address.
    /// </returns>
    public object ReadStruct(Type Type, uint Address) => StructAttribute.Get(this, Type).Read(this, Address);

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to read.
    /// </param>
    /// <returns>
    /// The <c>T</c> at the given address.
    /// </returns>
    public T ReadStruct<T>(uint Address) => (T)ReadStruct(typeof(T), Address);

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Type">
    /// The type to read.
    /// </param>
    /// <param name="Bytes">
    /// The byte array.
    /// </param>
    /// <param name="Offset">
    /// The start offset in the <paramref name="Bytes"/>. Defaults to <c>0</c>.
    /// </param>
    /// <returns>
    /// The <paramref name="Type"/> at the given address.
    /// </returns>
    public object StructFromBytes(Type Type, byte[] Bytes, int Offset = 0) => StructAttribute.Get(this, Type).FromBytes(this, Bytes, Offset);

    /// <summary>
    /// Reads <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Bytes">
    /// The byte array.
    /// </param>
    /// <param name="Offset">
    /// The start offset in the <paramref name="Bytes"/>. Defaults to <c>0</c>.
    /// </param>
    /// <returns>
    /// The <c>T</c> at the given address.
    /// </returns>
    public T StructFromBytes<T>(byte[] Bytes, int Offset = 0) => (T)StructFromBytes(typeof(T), Bytes, Offset);

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>byte</c> value to write.
    /// </param>
    public void WriteByte(uint Address, byte Value) => Write(Address, [Value], out _);

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>bool</c> value to write.
    /// </param>
    public void WriteBoolean(uint Address, bool Value) => WriteByte(Address, (byte)(Value ? 1 : 0));

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>byte[]</c> value to write.
    /// </param>
    public void WriteBytes(uint Address, byte[] Value)
    {
        Write(Address, Value, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>double</c> value to write.
    /// </param>
    public void WriteDouble(uint Address, double Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>float</c> value to write.
    /// </param>
    public void WriteSingle(uint Address, float Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>short</c> value to write.
    /// </param>
    public void WriteInt16(uint Address, short Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>int</c> value to write.
    /// </param>
    public void WriteInt32(uint Address, int Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>long</c> value to write.
    /// </param>
    public void WriteInt64(uint Address, long Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>ushort</c> value to write.
    /// </param>
    public void WriteUInt16(uint Address, ushort Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>uint</c> value to write.
    /// </param>
    public void WriteUInt32(uint Address, uint Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>ulong</c> value to write.
    /// </param>
    public void WriteUInt64(uint Address, ulong Value)
    {
        byte[] bytes = BitConverter.GetBytes(Value);
        Write(Address, bytes, out _);
    }

    /// <summary>
    /// Gets a byte array of <paramref name="Length"/> that is the <paramref name="Encoding"/> bytes of <paramref name="Value"/>.
    /// </summary>
    /// <param name="Value">
    /// The <c>string</c> value to encode.
    /// </param>
    /// <param name="Encoding">
    /// The character encoding to use.
    /// </param>
    /// <param name="Length">
    /// The number of bytes required.
    /// </param>
    public byte[] GetStringBytes(string Value, Encoding Encoding, int Length)
    {
        byte[] Bytes = Encoding.GetBytes(Value);
        Array.Resize(ref Bytes, Length);
        Bytes[Length - 1] = 0;

        return Bytes;
    }

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>string</c> value to write.
    /// </param>
    /// <param name="Encoding">
    /// The character encoding to use.
    /// </param>
    /// <param name="Length">
    /// The number of bytes to write.
    /// </param>
    public void WriteString(uint Address, string Value, Encoding Encoding, int Length) => Write(Address, GetStringBytes(Value, Encoding, Length), out _);

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Type">
    /// The type to write.
    /// </param>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <paramref name="Type"/> value to write.
    /// </param>
    public void WriteStruct(Type Type, uint Address, object Value) => StructAttribute.Get(this, Type).Write(this, Address, Value);

    /// <summary>
    /// Writes the given value to <see cref="Process"/>'s memory at the given address.
    /// </summary>
    /// <param name="Address">
    /// The address to write to.
    /// </param>
    /// <param name="Value">
    /// The <c>T</c> value to write.
    /// </param>
    public void WriteStruct<T>(uint Address, T Value) => WriteStruct(typeof(T), Address, Value);

    /// <summary>
    /// Converts <paramref name="Value"/> to a byte array, and inserts it into <paramref name="Bytes"/> at <paramref name="Offset"/>.
    /// </summary>
    /// <param name="Type">
    /// The type to read.
    /// </param>
    /// <param name="Bytes">
    /// The byte array.
    /// </param>
    /// <param name="Value">
    /// The <c>T</c> value to convert.
    /// </param>
    /// <param name="Offset">
    /// The start offset in the <paramref name="Bytes"/>. Defaults to <c>0</c>.
    /// </param>
    public void BytesFromStruct(Type Type, object Value, byte[] Bytes, int Offset = 0) => StructAttribute.Get(this, Type).ToBytes(this, Value, Bytes, Offset);

    /// <summary>
    /// Converts <paramref name="Value"/> to a byte array, and inserts it into <paramref name="Bytes"/> at <paramref name="Offset"/>.
    /// </summary>
    /// <param name="Bytes">
    /// The byte array.
    /// </param>
    /// <param name="Value">
    /// The <c>T</c> value to convert.
    /// </param>
    /// <param name="Offset">
    /// The start offset in the <paramref name="Bytes"/>. Defaults to <c>0</c>.
    /// </param>
    public void BytesFromStruct<T>(T Value, byte[] Bytes, int Offset = 0) => BytesFromStruct(typeof(T), Value, Bytes, Offset);

    /// <summary>
    /// Allocates 1 block of memory in <see cref="Process"/>.
    /// </summary>
    /// <returns>
    /// The address of the allocated memory.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// Throws if any Windows exceptions happen.
    /// </exception>
    public uint AllocateMemoryBlock() => AllocateMemoryBlocks(1);

    /// <summary>
    /// Allocates <see cref="BYTES_PER_BLOCK"/> bytes of memory in <see cref="Process"/>.
    /// </summary>
    /// <returns>
    /// The address of the allocated memory.
    /// </returns>
    /// <exception cref="Win32Exception">
    /// Throws if any Windows exceptions happen.
    /// </exception>
    public uint AllocateMemoryBlocks(uint blocks)
    {
        if (blocks < 1)
            throw new ArgumentException($"{nameof(blocks)} must be greater than or equal to 1.", nameof(blocks));

        IntPtr intPtr = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_WRITE, bInheritHandle: false, Process.Id);
        if (intPtr == IntPtr.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error());

        try
        {
            IntPtr allocMemAddress = VirtualAllocEx(intPtr, IntPtr.Zero, BYTES_PER_BLOCK * blocks, MEM_COMMIT, PAGE_READWRITE);
            if (allocMemAddress == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return (uint)allocMemAddress.ToInt32();
        }
        finally
        {
            CloseHandle(intPtr);
        }
    }

    private uint CurrentAddress = 0;
    private uint BytesRemaining = 0;
    private readonly object AllocationLock = new();
    /// <summary>
    /// Allocates memory and writes <paramref name="Value"/> in <see cref="Process"/>.
    /// </summary>
    /// <param name="Value">
    /// The <c>byte[]</c> value to write.
    /// </param>
    /// <returns>
    /// The address written to.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Throws if <paramref name="Value"/> is empty.
    /// </exception>
    /// <exception cref="Win32Exception">
    /// Throws if any Windows exceptions happen.
    /// </exception>
    public uint AllocateAndWriteMemory(byte[] Value)
    {
        var length = (uint)Value?.Length;
        if (length == 0)
            throw new ArgumentException($"{nameof(Value)} cannot have a length of 0.");

        lock (AllocationLock)
        {
            if (CurrentAddress == 0 || BytesRemaining < length)
            {
                uint blocks = (uint)Math.Ceiling(1d * length / BYTES_PER_BLOCK);
                CurrentAddress = AllocateMemoryBlocks(blocks);
                BytesRemaining = BYTES_PER_BLOCK * blocks;
            }

            var address = CurrentAddress;
            WriteBytes(address, Value);

            BytesRemaining -= length;
            CurrentAddress += length;

            return address;
        }
    }

    /// <summary>
    /// Gets the base address of a given module name in <see cref="Process"/>.
    /// </summary>
    /// <param name="ModuleName">
    /// The module name to find.
    /// </param>
    /// <returns>
    /// An <c>IntPtr</c> for the found address, or <c>IntPtr.Zero</c> if not found.
    /// </returns>
    public IntPtr GetModuleBaseAddress(string ModuleName)
    {
        ProcessModule hacksModule = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
        return hacksModule == null ? IntPtr.Zero : hacksModule.BaseAddress;
    }

    private readonly Dictionary<string, Dictionary<uint, uint>> ordinalAddressCache = [];
    /// <summary>
    /// Gets the base address of a given module's procedure in <see cref="Process"/>.
    /// </summary>
    /// <param name="ModuleName">
    /// The module name to find.
    /// </param>
    /// <param name="Proc">
    /// The prodecdure ordinal to find.
    /// </param>
    /// <returns>
    /// A <c>uint</c> for the found address, or <c>0</c> if not found.
    /// </returns>
    public uint GetModuleProcAddress(string ModuleName, uint Proc)
    {
        ModuleName = ModuleName.ToLower();
        if (ordinalAddressCache.ContainsKey(ModuleName) && ordinalAddressCache[ModuleName].ContainsKey(Proc))
            return ordinalAddressCache[ModuleName][Proc];

        ProcessModule module = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
        if (module == null)
        {
            Debug.WriteLine($"Couldn't find module: {ModuleName}");
            return 0;
        }

        IntPtr dll = LoadLibraryEx(module.FileName, IntPtr.Zero, LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
        if (dll == IntPtr.Zero)
        {
            Debug.WriteLine($"Couldn't load module: {module.FileName}");
            return 0;
        }

        UIntPtr method = GetProcAddressOrdinal(dll, (UIntPtr)Proc);
        if (method == UIntPtr.Zero)
        {
            Debug.WriteLine($"Couldn't find method: {Proc}");
            FreeLibrary(dll);
            return 0;
        }

        uint offset = (uint)(method.ToUInt32() - dll.ToInt32());

        FreeLibrary(dll);
        uint address = (uint)(module.BaseAddress.ToInt32() + offset);

        if (!ordinalAddressCache.ContainsKey(ModuleName))
            ordinalAddressCache[ModuleName] = [];

        ordinalAddressCache[ModuleName][Proc] = address;

        return address;
    }

    private readonly Dictionary<string, Dictionary<string, uint>> namedAddressCache = [];
    private bool disposedValue;

    /// <summary>
    /// Gets the base address of a given module's procedure in <see cref="Process"/>.
    /// </summary>
    /// <param name="ModuleName">
    /// The module name to find.
    /// </param>
    /// <param name="Proc">
    /// The prodecdure name to find.
    /// </param>
    /// <returns>
    /// A <c>uint</c> for the found address, or <c>0</c> if not found.
    /// </returns>
    public uint GetModuleProcAddress(string ModuleName, string Proc)
    {
        ModuleName = ModuleName.ToLower();
        if (namedAddressCache.ContainsKey(ModuleName) && namedAddressCache[ModuleName].ContainsKey(Proc))
            return namedAddressCache[ModuleName][Proc];

        ProcessModule module = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals(ModuleName, StringComparison.OrdinalIgnoreCase));
        if (module == null)
        {
            Debug.WriteLine($"Couldn't find module: {ModuleName}");
            return 0;
        }

        IntPtr dll = LoadLibraryEx(module.FileName, IntPtr.Zero, LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
        if (dll == IntPtr.Zero)
        {
            Debug.WriteLine($"Couldn't load module: {module.FileName}");
            return 0;
        }

        UIntPtr method = GetProcAddress(dll, Proc);
        if (method == UIntPtr.Zero)
        {
            Debug.WriteLine($"Couldn't find method: {Proc}");
            FreeLibrary(dll);
            return 0;
        }

        uint offset = (uint)(method.ToUInt32() - dll.ToInt32());

        FreeLibrary(dll);
        uint address = (uint)(module.BaseAddress.ToInt32() + offset);

        if (!namedAddressCache.ContainsKey(ModuleName))
            namedAddressCache[ModuleName] = [];

        namedAddressCache[ModuleName][Proc] = address;

        return address;
    }

    ///// <summary>
    ///// Injects the given path into <see cref="Process"/>.
    ///// </summary>
    ///// <param name="path">
    ///// The filepath to inject.
    ///// </param>
    ///// <exception cref="Win32Exception"></exception>
    //protected void Inject(string path)
    //{
    //    IntPtr procHandle = IntPtr.Zero;
    //    IntPtr allocMemAddress = IntPtr.Zero;
    //    IntPtr hThread = IntPtr.Zero;
    //    try
    //    {
    //        procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
    //        if (procHandle == IntPtr.Zero)
    //            throw new Win32Exception();


    //        UIntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryW");
    //        byte[] pathBytes = Encoding.Unicode.GetBytes(path + '\0');
    //        uint pathLength = (uint)pathBytes.Length;
    //        allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, pathLength, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    //        if (allocMemAddress == IntPtr.Zero)
    //            throw new Win32Exception();

    //        if (!WriteProcessMemory(procHandle, allocMemAddress, pathBytes, pathLength, out _))
    //            throw new Win32Exception();

    //        hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, (IntPtr)loadLibraryAddr.ToUInt32(), allocMemAddress, 0, IntPtr.Zero);
    //        if (hThread == IntPtr.Zero)
    //            throw new Win32Exception();

    //        if (WaitForSingleObject(hThread, 0xFFFFFFFF) == 0xFFFFFFFF)
    //            throw new Win32Exception();
    //    }
    //    finally
    //    {
    //        if (hThread != IntPtr.Zero)
    //            if (!CloseHandle(hThread))
    //                throw new Win32Exception();

    //        if (allocMemAddress != IntPtr.Zero)
    //            if (!VirtualFreeEx(procHandle, allocMemAddress, 0, MEM_RELEASE))
    //                throw new Win32Exception();

    //        if (procHandle != IntPtr.Zero)
    //            if (!CloseHandle(procHandle))
    //                throw new Win32Exception();
    //    }
    //}

    ///// <summary>
    ///// Executes a procedure in <see cref="Process"/> at the given address, with the specified parameter.
    ///// </summary>
    ///// <param name="Address">
    ///// The address of the procedure to execute.
    ///// </param>
    ///// <param name="Parameter">
    ///// The parameter to pass to the procedure.
    ///// </param>
    ///// <returns>
    ///// A <c>uint</c> that is the procedure's exit code.
    ///// </returns>
    ///// <exception cref="Win32Exception"></exception>
    //protected uint Execute(IntPtr Address, object Parameter)
    //{
    //    IntPtr procHandle = IntPtr.Zero;
    //    IntPtr allocMemAddress = IntPtr.Zero;
    //    IntPtr hThread = IntPtr.Zero;
    //    IntPtr paramsMemory = IntPtr.Zero;
    //    try
    //    {
    //        procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
    //        if (procHandle == IntPtr.Zero)
    //            throw new Win32Exception();

    //        uint paramsSize = (uint)Marshal.SizeOf(Parameter);
    //        paramsMemory = Marshal.AllocHGlobal((int)paramsSize);
    //        Marshal.StructureToPtr(Parameter, paramsMemory, false);

    //        allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, paramsSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    //        if (allocMemAddress == IntPtr.Zero)
    //            throw new Win32Exception();

    //        if (!WriteProcessMemory(procHandle, allocMemAddress, paramsMemory, paramsSize, out _))
    //            throw new Win32Exception();

    //        hThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, Address, allocMemAddress, 0, IntPtr.Zero);
    //        if (hThread == IntPtr.Zero)
    //            throw new Win32Exception();

    //        if (WaitForSingleObject(hThread, 0xFFFFFFFF) == 0xFFFFFFFF)
    //            throw new Win32Exception();

    //        if (!GetExitCodeThread(hThread, out uint retVal))
    //            throw new Win32Exception();

    //        return retVal;
    //    }
    //    finally
    //    {
    //        if (paramsMemory != IntPtr.Zero)
    //            Marshal.FreeHGlobal(paramsMemory);

    //        if (hThread != IntPtr.Zero)
    //            if (!CloseHandle(hThread))
    //                throw new Win32Exception();

    //        if (allocMemAddress != IntPtr.Zero)
    //            if (!VirtualFreeEx(procHandle, allocMemAddress, 0, MEM_RELEASE))
    //                throw new Win32Exception();

    //        if (procHandle != IntPtr.Zero)
    //            if (!CloseHandle(procHandle))
    //                throw new Win32Exception();
    //    }
    //}

    ///// <summary>
    ///// Injects and then executes a procedure in <see cref="Process"/> at the given address, with the specified parameter.
    ///// Cleans up injected procedure afterwards.
    ///// </summary>
    ///// <param name="Bytes">
    ///// The bytes of the procedure.
    ///// </param>
    ///// <param name="Parameter">
    ///// The parameter to pass to the procedure.
    ///// </param>
    ///// <returns>
    ///// A <c>uint</c> that is the procedure's exit code.
    ///// </returns>
    ///// <exception cref="Win32Exception"></exception>
    //protected uint Execute(byte[] Bytes, object Parameter)
    //{
    //    IntPtr procHandle = IntPtr.Zero;
    //    IntPtr funcMemoryAddress = IntPtr.Zero;
    //    try
    //    {
    //        procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
    //        if (procHandle == IntPtr.Zero)
    //            throw new Win32Exception();

    //        uint funcSize = (uint)Bytes.Length;
    //        funcMemoryAddress = VirtualAllocEx(procHandle, IntPtr.Zero, funcSize, MEM_COMMIT | MEM_RESERVE, 0x40);
    //        if (funcMemoryAddress == IntPtr.Zero)
    //            throw new Win32Exception();

    //        return Execute(funcMemoryAddress, Parameter);
    //    }
    //    finally
    //    {
    //        if (funcMemoryAddress != IntPtr.Zero)
    //            if (!VirtualFreeEx(procHandle, funcMemoryAddress, 0, MEM_RELEASE))
    //                throw new Win32Exception();

    //        if (procHandle != IntPtr.Zero)
    //            if (!CloseHandle(procHandle))
    //                throw new Win32Exception();
    //    }
    //}

    ///// <summary>
    ///// Injects a procedure into <see cref="Process"/>.
    ///// </summary>
    ///// <param name="Bytes">
    ///// The bytes of the procedure.
    ///// </param>
    ///// <returns>
    ///// An <c>IntPtr</c> of the procedure's address.
    ///// </returns>
    ///// <exception cref="Win32Exception"></exception>
    //protected IntPtr InjectFunction(byte[] Bytes)
    //{
    //    IntPtr procHandle = IntPtr.Zero;
    //    try
    //    {
    //        procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
    //        if (procHandle == IntPtr.Zero)
    //            throw new Win32Exception();

    //        uint funcSize = (uint)Bytes.Length;
    //        IntPtr funcMemoryAddress = VirtualAllocEx(procHandle, IntPtr.Zero, funcSize, MEM_COMMIT | MEM_RESERVE, 0x40);

    //        if (funcMemoryAddress == IntPtr.Zero)
    //            throw new Win32Exception();

    //        if (!WriteProcessMemory(procHandle, funcMemoryAddress, Bytes, funcSize, out _))
    //            throw new Win32Exception();

    //        ASMFunctions.Add(funcMemoryAddress);
    //        return funcMemoryAddress;
    //    }
    //    finally
    //    {
    //        if (procHandle != IntPtr.Zero)
    //            if (!CloseHandle(procHandle))
    //                throw new Win32Exception();
    //    }
    //}

    /// <summary>
    /// Disposes the <see cref="Process"/>, and also cleans up all injected functions.
    /// </summary>
    /// <param name="disposing">
    /// If the object is being disposed.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                //IntPtr procHandle = IntPtr.Zero;
                //try
                //{
                //    procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
                //    if (procHandle != IntPtr.Zero)
                //        foreach (IntPtr Address in ASMFunctions)
                //            if (Address != IntPtr.Zero)
                //                VirtualFreeEx(procHandle, Address, 0, MEM_RELEASE);
                //}
                //finally
                //{
                //    if (procHandle != IntPtr.Zero)
                //        CloseHandle(procHandle);
                //}
                Process.Dispose();
            }

            disposedValue = true;
        }
    }

    /// <summary>
    /// Disposes the <see cref="Process"/>, and also cleans up all injected functions.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
