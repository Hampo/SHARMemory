using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVGameDataManager@@")]
public class GameDataManager : Class
{
    public const int MaxNumGameData = 16;

    public enum FileOperation
    {
        None,
        OpenForReading,
        OpenForWriting,
        Read,
        Write,
        Commit,
        FileCheck,
        LoadComplete,
        SaveComplete,
        Delete,
        DeleteComplete,
    }

    public enum RadFileError // TODO: Move to IRadFile or something idk
    {
        Success,                    // No Error
        FileNotFound,               // File not found on media
        ShellOpen,                  // Disk door, shell open
        WrongMedia,                 // Different media (ie files open and disk was changed)
        NoMedia,                    // No media present
        MediaNotFormatted,          // Media not formatted
        MediaCorrupt,               // Corrupt media
        NoFreeSpace,                // Not enough space to carry out operation
        HardwareFailure,            // General failure
        MediaEncodingErr,           // Media formatted for a different market
        MediaWrongType,             // Unsupported media
        MediaInvalid,               // Unrecognized media
        DataCorrupt                 // Data failed CRC check
    }

    public GameDataManager(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint RegisteredGameDataOffset = 12; // VFTables
    public PointerArray<Class> RegisteredGameData => new(Memory, Address + RegisteredGameDataOffset, MaxNumGameData); // TODO: GameData class

    internal const uint NumRegisteredGameDataOffset = RegisteredGameDataOffset + MaxNumGameData * sizeof(uint);
    public uint NumRegisteredGameData
    {
        get => ReadUInt32(NumRegisteredGameDataOffset);
        set => WriteUInt32(NumRegisteredGameDataOffset, value);
    }

    internal const uint GameDataBufferOffset = NumRegisteredGameDataOffset + sizeof(uint);
    public Class GameDataBuffer => Memory.ClassFactory.Create<Class>(ReadUInt32(GameDataBufferOffset)); // TODO: GameDataByte class

    internal const uint GameDataSizeOffset = GameDataBufferOffset + sizeof(uint);
    public uint GameDataSize
    {
        get => ReadUInt32(GameDataSizeOffset);
        set => WriteUInt32(GameDataSizeOffset, value);
    }

    internal const uint GameDataLoadCallbackOffset = GameDataSizeOffset + sizeof(uint);
    public Class GameDataLoadCallback => Memory.ClassFactory.Create<Class>(ReadUInt32(GameDataLoadCallbackOffset)); // TODO: GameDataLoadCallback class

    internal const uint GameDataSaveCallbackOffset = GameDataLoadCallbackOffset + sizeof(uint);
    public Class GameDataSaveCallback => Memory.ClassFactory.Create<Class>(ReadUInt32(GameDataSaveCallbackOffset)); // TODO: GameDataSaveCallback class

    internal const uint GameDataDeleteCallbackOffset = GameDataSaveCallbackOffset + sizeof(uint);
    public Class GameDataDeleteCallback => Memory.ClassFactory.Create<Class>(ReadUInt32(GameDataDeleteCallbackOffset)); // TODO: GameDataDeleteCallback class

    internal const uint MinimumLoadSaveTimeOffset = GameDataDeleteCallbackOffset + sizeof(uint);
    public uint MinimumLoadSaveTime
    {
        get => ReadUInt32(MinimumLoadSaveTimeOffset);
        set => WriteUInt32(MinimumLoadSaveTimeOffset, value);
    }

    internal const uint ElapsedOperationTimeOffset = MinimumLoadSaveTimeOffset + sizeof(uint);
    public uint ElapsedOperationTime
    {
        get => ReadUInt32(ElapsedOperationTimeOffset);
        set => WriteUInt32(ElapsedOperationTimeOffset, value);
    }

    internal const uint IsGameLoadedOffset = ElapsedOperationTimeOffset + sizeof(uint);
    public bool IsGameLoaded
    {
        get => ReadBoolean(IsGameLoadedOffset);
        set => WriteBoolean(IsGameLoadedOffset, value);
    }

    internal const uint SaveGameInfoHandlerOffset = IsGameLoadedOffset + 4; // Padding
    public Class SaveGameInfoHandler => Memory.ClassFactory.Create<Class>(ReadUInt32(SaveGameInfoHandlerOffset)); // TODO: SaveGameInfo class

    internal const uint RadFileOffset = SaveGameInfoHandlerOffset + sizeof(uint);
    public Class RadFile => Memory.ClassFactory.Create<Class>(ReadUInt32(RadFileOffset)); // TODO: IRadFile class

    internal const uint CurrentFileOperationOffset = RadFileOffset + sizeof(uint);
    public FileOperation CurrentFileOperation
    {
        get => (FileOperation)ReadUInt32(CurrentFileOperationOffset);
        set => WriteUInt32(CurrentFileOperationOffset, (uint)value);
    }

    internal const uint LastErrorOffset = CurrentFileOperationOffset + sizeof(uint);
    public RadFileError LastError
    {
        get => (RadFileError)ReadUInt32(LastErrorOffset);
        set => WriteUInt32(LastErrorOffset, (uint)value);
    }
}
