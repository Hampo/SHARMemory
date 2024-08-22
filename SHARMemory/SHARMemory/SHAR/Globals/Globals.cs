﻿using SHARMemory.SHAR.Classes;
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
    /// <para>If <c>true</c>, the function <c>PhysicsLocomotion::DurangoStyleStabilizer</c> is patched to instantly return.</para>
    /// <para>If <c>false</c>, the function <c>PhysicsLocomotion::DurangoStyleStabilizer</c> is returned to vanilla functionality.</para>
    /// </summary>
    public bool FlippableCars
    {
        get => Enumerable.SequenceEqual(Memory.ReadBytes(FlippableCarsAddress, 3), FlippableCarsEnable);
        set => Memory.WriteBytes(FlippableCarsAddress, value ? FlippableCarsEnable : FlippableCarsDisable);
    }

    public readonly uint DefaultNumTurbosAddress;
    /// <summary>
    /// A property representing the default <see cref="Vehicle.NumTurbos"/> set in the <c>Vehicle</c> constructor in <see cref="Memory"/>.
    /// <para>Because ASM, this also affects the default <see cref="Vehicle.WasHitByVehicleType"/> value, however this value should be updated before its ever checked so probably fine.</para>
    /// </summary>
    public int DefaultNumTurbos
    {
        get => Memory.ReadInt32(DefaultNumTurbosAddress);
        set => Memory.WriteInt32(DefaultNumTurbosAddress, value);
    }

    private static readonly byte[] TurboEnable = [0x90, 0x90];
    private static readonly byte[] TurboDisable1 = [0x75, 0x07];
    private static readonly byte[] TurboDisable2 = [0xEB, 0x41];
    private readonly uint TurboAddress1;
    private readonly uint TurboAddress2;
    /// <summary>
    /// A property representing if <c>Turbo</c> is enabled in <see cref="Memory"/>.
    /// <para>If <c>true</c>, the function <c>VehicleCentral::PreSubstepUpdate</c> is patched to run both the SuperSprint and Normal game if statements.</para>
    /// <para>If <c>false</c>, the function <c>VehicleCentral::PreSubstepUpdate</c> is returned to vanilla functionality.</para>
    /// </summary>
    public bool TurboEnabled
    {
        get => Enumerable.SequenceEqual(Memory.ReadBytes(TurboAddress1, 2), TurboEnable) && Enumerable.SequenceEqual(Memory.ReadBytes(TurboAddress2, 2), TurboEnable);
        set
        {
            if (value)
            {
                Memory.WriteBytes(TurboAddress1, TurboEnable);
                Memory.WriteBytes(TurboAddress2, TurboEnable);
            }
            else
            {
                Memory.WriteBytes(TurboAddress1, TurboDisable1);
                Memory.WriteBytes(TurboAddress2, TurboDisable2);
            }
        }
    }

    private static readonly byte[] TurboShadowEnable = [0x90, 0x90];
    private static readonly byte[] TurboShadowDisable = [0x75, 0x57];
    private readonly uint TurboShadowAddress;
    /// <summary>
    /// A property representing if <c>TurboShadow</c> is enabled in <see cref="Memory"/>.
    /// <para>If <c>true</c>, the function <c>GeometryVehicle::DisplayShadow</c> is patched to only run the SuperSprint side of the first if statement.</para>
    /// <para>If <c>false</c>, the function <c>GeometryVehicle::DisplayShadow</c> is returned to vanilla functionality.</para>
    /// </summary>
    public bool TurboShadow
    {
        get => Enumerable.SequenceEqual(Memory.ReadBytes(TurboShadowAddress, 2), TurboShadowEnable);
        set => Memory.WriteBytes(TurboShadowAddress, value ? TurboShadowEnable : TurboShadowDisable);
    }

    private static readonly byte[] CharacterShadowEnable = [0x90, 0x90];
    private static readonly byte[] CharacterShadowDisable = [0x75, 0x09];
    private readonly uint CharacterShadowAddress;
    /// <summary>
    /// A property representing if <c>CharacterShadow</c> is enabled in <see cref="Memory"/>.
    /// <para>If <c>true</c>, the function <c>CharacterRenderable::DisplayShadow</c> is patched to only run the SuperSprint side of the first if statement.</para>
    /// <para>If <c>false</c>, the function <c>CharacterRenderable::DisplayShadow</c> is returned to vanilla functionality.</para>
    /// </summary>
    public bool CharacterShadow
    {
        get => Enumerable.SequenceEqual(Memory.ReadBytes(CharacterShadowAddress, 2), CharacterShadowEnable);
        set => Memory.WriteBytes(CharacterShadowAddress, value ? CharacterShadowEnable : CharacterShadowDisable);
    }

    internal Globals(Memory memory)
    {
        Memory = memory;

        CharacterTune = new(Memory);
        Cheats = new(Memory);
        FeTextBible = new(Memory);

        LevelCountAddress = Memory.SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3;

        FlippableCarsAddress = Memory.SelectAddress(0x4E5020, 0x4E5100, 0x4E53C0, 0x4E51B0);

        DefaultNumTurbosAddress = Memory.SelectAddress(0x4EF12A, 0x4EF23A, 0x4EF4DA, 0x4EF2DA) + 1;

        TurboAddress1 = Memory.SelectAddress(0x4DB5B1, 0x4DB691, 0x4DB951, 0x4DB731);
        TurboAddress2 = Memory.SelectAddress(0x4DB5B8, 0x4DB698, 0x4DB958, 0x4DB738);

        TurboShadowAddress = Memory.SelectAddress(0x4E0D80, 0x4E0E60, 0x4E1120, 0x4E0F00);

        CharacterShadowAddress = Memory.SelectAddress(0x4FE846, 0x4FE956, 0x4FEC26, 0x4FEA46);
    }
}