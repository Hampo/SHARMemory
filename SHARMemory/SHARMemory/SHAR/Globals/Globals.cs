using SHARMemory.SHAR.Classes;
using System.Linq;

namespace SHARMemory.SHAR;

public sealed partial class Globals
{
    private readonly Memory Memory;

    /// <summary>
    /// A reference to SHAR's <see cref="Classes.GameplayManager"/> static global.
    /// </summary>
    public GameplayManager GameplayManager => Memory.ClassFactory.Create<GameplayManager>(Memory.ReadUInt32(Memory.SelectAddress(0x6C8998, 0x6C8958, 0x6C8958, 0x6C8990)));
    /// <summary>
    /// A reference to SHAR's <see cref="Classes.FeTextBible"/> static global.
    /// </summary>
    public FeTextBible TextBible => Memory.ClassFactory.Create<FeTextBible>(Memory.ReadUInt32(Memory.SelectAddress(0x6C8944, 0x6C8904, 0x6C8904, 0x6C893C)));

    /// <summary>
    /// A reference to the handler for SHAR's <c>CharacterTune</c> statics.
    /// </summary>
    public CharacterTuneHandler CharacterTune { get; }
    /// <summary>
    /// A reference to the handler for SHAR's <c>Cheat</c> statics.
    /// </summary>
    public CheatsHandler Cheats { get; }
    /// <summary>
    /// A reference to the handler for SHAR's <c>FeTextBible</c> statics.
    /// </summary>
    public FeTextBibleHandler FeTextBible { get; }
    /// <summary>
    /// A reference to the handler for SHAR's <c>TrafficManager</c> statics.
    /// </summary>
    public TrafficManagerHandler TrafficManager { get; }

    private readonly uint LevelCountAddress;
    /// <summary>
    /// A <c>byte</c> containing how many levels in the game. Usually 7, but can differ when using <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" />.
    /// </summary>
    public byte LevelCount => Memory.ReadByte(LevelCountAddress);

    private static readonly byte[] FlippableCarsEnable = [0xC2, 0x04, 0x00];
    private static readonly byte[] FlippableCarsDisable = [0x83, 0xEC, 0x28];
    private readonly uint FlippableCarsAddress;
    /// <summary>
    /// A property representing if <c>FlippableCars</c> is enabled in <see cref="Memory"/>.
    /// </summary>
    public bool FlippableCars
    {
        get => Enumerable.SequenceEqual(Memory.ReadBytes(FlippableCarsAddress, 3), FlippableCarsEnable);
        set => Memory.WriteBytes(FlippableCarsAddress, value ? FlippableCarsEnable : FlippableCarsDisable);
    }

    internal Globals(Memory memory)
    {
        Memory = memory;

        CharacterTune = new(Memory);
        Cheats = new(Memory);
        FeTextBible = new(Memory);
        TrafficManager = new(Memory);

        LevelCountAddress = Memory.SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3;
        FlippableCarsAddress = Memory.SelectAddress(0x4E5020, 0x4E5100, 0x4E53C0, 0x4E51B0);
    }
}