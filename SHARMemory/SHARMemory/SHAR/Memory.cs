using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SHARMemory.SHAR.Classes;
using SHARMemory.SHAR.Pointers;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Class <c>SHAR.Memory</c> inherits <c>ProcessMemory</c>, and adds additional SHAR-specific methods and properties.
    /// Initially created by Lucas Cardellini, further developed by Proddy.
    /// </summary>
    public class Memory : ProcessMemory
    {
        /// <summary>
        /// An enum of supported game versions.
        /// </summary>
        public enum GameVersions
        {
            /// <summary>
            /// The English release of the game.
            /// </summary>
            ReleaseEnglish,
            /// <summary>
            /// The International release of the game.
            /// </summary>
            ReleaseInternational,
            /// <summary>
            /// The Best Seller release of the game.
            /// </summary>
            BestSellerSeries,
            /// <summary>
            /// The Demo release of the game.
            /// </summary>
            Demo,
            /// <summary>
            /// An Unknown release of the game.
            /// </summary>
            Unknown
        }

        /// <summary>
        /// An enum of game sub versions - <see cref="GameVersions.ReleaseInternational"/>
        /// </summary>
        public enum GameSubVersions
        {
            /// <summary>
            /// The English language release of <see cref="GameVersions.ReleaseInternational"/>.
            /// </summary>
            English,
            /// <summary>
            /// The French language release of <see cref="GameVersions.ReleaseInternational"/>.
            /// </summary>
            French,
            /// <summary>
            /// The German language release of <see cref="GameVersions.ReleaseInternational"/>.
            /// </summary>
            German,
            /// <summary>
            /// The Spanish language release of <see cref="GameVersions.ReleaseInternational"/>.
            /// </summary>
            Spanish,
            /// <summary>
            /// An Unknown release of <see cref="GameVersions.ReleaseInternational"/>.
            /// </summary>
            Unknown
        }

        /// <summary>
        /// The detected <see cref="GameVersions"/>
        /// </summary>
        public GameVersions GameVersion { get; }
        /// <summary>
        /// The detected <see cref="GameVersions"/>
        /// </summary>
        public GameSubVersions GameSubVersion { get; }

        /// <summary>
        /// A <c>byte</c> containing how many levels in the game. Usually 7, but can differ when using the mod laucher.
        /// </summary>
        public byte LevelCount => ReadByte(SelectAddress(0x4798A8, 0x479748, 0x479618, 0x4793D8) + 3);


        /// <summary>
        /// A class to manage SHAR's cheats.
        /// </summary>
        public Cheats Cheats { get; }


        /// <summary>
        /// A reference to SHAR's static <c>CharacterManager</c>.
        /// </summary>
        public CharacterManager CharacterManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>CharacterSheetManager</c>.
        /// </summary>
        public CharacterSheetManager CharacterSheetManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>CharacterTune</c>.
        /// </summary>
        public CharacterTune CharacterTune { get; }

        /// <summary>
        /// A reference to SHAR's static <c>GameFlow</c>.
        /// </summary>
        public GameFlow GameFlow { get; }

        /// <summary>
        /// A reference to SHAR's static <c>GameplayManager</c>.
        /// </summary>
        public GameplayManager GameplayManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>HitNRunManager</c>.
        /// </summary>
        public HitNRunManager HitNRunManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>InteriorManager</c>.
        /// </summary>
        public InteriorManager InteriorManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>IntersectManager</c>.
        /// </summary>
        public IntersectManager IntersectManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>LoadingManager</c>.
        /// </summary>
        public LoadingManager LoadingManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>TrafficManager</c>.
        /// </summary>
        public TrafficManager TrafficManager { get; }

        /// <summary>
        /// A reference to SHAR's static <c>VehicleCentral</c>.
        /// </summary>
        public VehicleCentral VehicleCentral { get; }

        /// <summary>
        /// Checks if <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" /> is loaded.
        /// </summary>
        public bool IsModLauncherLoaded { get; }

        private static readonly uint[] ModLauncherOrdinalKeys = new uint[]
        {
            3151, // Event Hacks
            3360, // Max Cars
            3364, // Cars Offset
        };
        /// <summary>
        /// A <c>Dictionary</c> containing a list of ordinals used by <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" /> and their addresses.
        /// </summary>
        public Dictionary<uint, uint> ModLauncherOrdinals { get; } = new Dictionary<uint, uint>(ModLauncherOrdinalKeys.Length);
        private void LoadModLauncherOrdinals(ProcessModule hacksModule)
        {
            IntPtr dll = LoadLibraryEx(hacksModule.FileName, IntPtr.Zero, LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
            if (dll == IntPtr.Zero)
            {
                Debug.WriteLine($"Couldn't load module: {hacksModule.FileName}");
                return;
            }

            try
            {
                foreach (uint ordinal in ModLauncherOrdinalKeys)
                {
                    UIntPtr method = GetProcAddressOrdinal(dll, new UIntPtr(ordinal));
                    if (method == UIntPtr.Zero)
                    {
                        Debug.WriteLine($"Couldn't find method: {ordinal}");
                        continue;
                    }

                    uint offset = (uint)(method.ToUInt32() - dll.ToInt32());

                    uint address = (uint)(hacksModule.BaseAddress.ToInt32() + offset);

                    ModLauncherOrdinals.Add(ordinal, address);
                }
            }
            finally
            {
                FreeLibrary(dll);
            }
        }

        /// <summary>
        /// If <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" /> is loaded, query loaded hacks.
        /// Credits: Lucas Cardellini
        /// </summary>
        /// <returns>
        /// A string array containing the names of loaded hacks.
        /// </returns>
        public string[] GetLoadedHacks()
        {
            if (!ModLauncherOrdinals.TryGetValue(3351, out uint EventHacks))
                return null;

            uint HacksLoaded = ReadUInt32(EventHacks + 8);

            string[] Hacks = new string[HacksLoaded];

            if (HacksLoaded > 0)
            {
                int i = 0;
                uint node = ReadUInt32(EventHacks);
                while (node != 0)
                {
                    uint hack = ReadUInt32(node + 12);
                    Hacks[i] = ReadString(hack + 562, Encoding.Unicode, 128u);
                    i++;

                    node = ReadUInt32(node + 8);
                }
            }

            return Hacks;
        }

        /// <summary>
        /// If <see href="https://modbakery.donutteam.com/releases/view/lucas-mod-launcher" langword=" (Lucas' Mod Launcher)" /> is loaded, check if a hack is loaded.
        /// Credits: Lucas Cardellini
        /// </summary>
        /// <param name="HackName">
        /// The name of the hack to check for.
        /// </param>
        /// <returns>
        /// A boolean if reporting if the specified hack is found to be loaded.
        /// </returns>
        public bool IsHackLoaded(string HackName)
        {
            if (!ModLauncherOrdinals.TryGetValue(3351, out uint EventHacks))
                return false;

            uint node = ReadUInt32(EventHacks);
            while (node != 0)
            {
                uint hack = ReadUInt32(node + 12);
                if (ReadString(hack + 562, Encoding.Unicode, 128u).Equals(HackName, StringComparison.OrdinalIgnoreCase))
                    return true;

                node = ReadUInt32(node + 8);
            }

            return false;
        }

        /// <summary>
        /// Checks a somewhat arbitrary memory value to try determine what version of the game is running.
        /// Credits: Lucas Cardellini
        /// </summary>
        /// <param name="GameSubVersion">
        /// If the return value is <c>GameVersions.ReleaseInternational</c>, which sub version of the International release it is.
        /// </param>
        /// <returns>
        /// One of the <c>GameVersions</c> enum matching which version was detected.
        /// </returns>
        private GameVersions DetectVersion(ref GameSubVersions GameSubVersion)
        {
            switch (ReadUInt32(0x593FFF))
            {
                case 0xFAE804C5:
                    return GameVersions.Demo;
                case 0x4B8B2274:
                    return GameVersions.ReleaseEnglish;
                case 0xC985ED33:
                    GameSubVersion = ReadUInt32(0x494FB1) switch
                    {
                        0xF6C10AE8 => GameSubVersions.English,
                        0xF984BAE8 => GameSubVersions.French,
                        0xF6C12AE8 => GameSubVersions.German,
                        0xF6C0FAE8 => GameSubVersions.Spanish,
                        _ => GameSubVersions.Unknown,
                    };
                    return GameVersions.ReleaseInternational;
                case 0xFC468D05:
                    return GameVersions.BestSellerSeries;
                default:
                    return GameVersions.Unknown;
            }
        }

        /// <summary>
        /// Based on the detected game version, returns the matching address.
        /// Credits: Lucas Cardellini
        /// </summary>
        /// <param name="ReleaseEnglishAddress">
        /// The address of the English release.
        /// </param>
        /// <param name="DemoAddress">
        /// The address of the Demo release.
        /// </param>
        /// <param name="ReleaseInternationalAddress">
        /// The address of the International release.
        /// </param>
        /// <param name="BestSellerSeriesAddress">
        /// The address of the Best Seller release.
        /// </param>
        /// <returns>
        /// The given <c>uint</c> that matches the game version.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an error in the detected game version is unknown.
        /// </exception>
        internal uint SelectAddress(uint ReleaseEnglishAddress, uint DemoAddress, uint ReleaseInternationalAddress, uint BestSellerSeriesAddress)
        {
            return GameVersion switch
            {
                GameVersions.ReleaseEnglish => ReleaseEnglishAddress,
                GameVersions.Demo => DemoAddress,
                GameVersions.ReleaseInternational => ReleaseInternationalAddress,
                GameVersions.BestSellerSeries => BestSellerSeriesAddress,
                _ => throw new Exception("Unrecognised game version."),
            };
        }

        /// <summary>
        /// The <c>SHAR.Memory</c> constructor.
        /// </summary>
        /// <param name="Process">
        /// A <c>Process</c> that points to a SHAR instance. See: <see cref="GetSHARProcess"/>.
        /// </param>
        public Memory(Process Process) : base(Process)
        {
            GameSubVersions subVersion = GameSubVersions.English;
            GameVersion = DetectVersion(ref subVersion);
            GameSubVersion = subVersion;

            using (ProcessModule hacksModule = Process.Modules.Cast<ProcessModule>().FirstOrDefault(x => x.ModuleName.Equals("Hacks.dll", StringComparison.OrdinalIgnoreCase)))
            {
                if (hacksModule == null)
                {
                    IsModLauncherLoaded = false;
                }
                else
                {
                    IsModLauncherLoaded = true;
                    LoadModLauncherOrdinals(hacksModule);
                }
            }

            Cheats = new Cheats(this);

            CharacterManager = new CharacterManager(this);
            CharacterSheetManager = new CharacterSheetManager(this);
            CharacterTune = new CharacterTune(this);
            GameFlow = new GameFlow(this);
            GameplayManager = new GameplayManager(this);
            HitNRunManager = new HitNRunManager(this);
            InteriorManager = new InteriorManager(this);
            IntersectManager = new IntersectManager(this);
            LoadingManager = new LoadingManager(this);
            TrafficManager = new TrafficManager(this);
            VehicleCentral = new VehicleCentral(this);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static IntPtr GetGameWindow() => FindWindow("The Simpsons Hit & Run", null);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpwdProcessId);

        /// <summary>
        /// Tries to get a SHAR <c>Process</c> based on window title.
        /// </summary>
        /// <returns>
        /// A SHAR <c>Process</c> or <c>null</c>.
        /// </returns>
        public static Process GetSHARProcess()
        {
            IntPtr GameWindow = GetGameWindow();
            if (GameWindow != IntPtr.Zero)
            {
                int ProcessId = 0;
                _ = GetWindowThreadProcessId(GameWindow, ref ProcessId);
                return Process.GetProcessById(ProcessId);
            }
            return null;
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Vector3</c> at the given address.
        /// </returns>
        public Vector3 ReadVector3(uint Address) => new Vector3(ReadSingle(Address), ReadSingle(Address + 4), ReadSingle(Address + 8));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Vector3</c> value to write.
        /// </param>
        public void WriteVector3(uint Address, Vector3 Value)
        {
            WriteSingle(Address, Value.X);
            WriteSingle(Address + 4, Value.Y);
            WriteSingle(Address + 8, Value.Z);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Matrix3x2</c> at the given address.
        /// </returns>
        public Matrix3x2 ReadMatrix3x2(uint Address) => new Matrix3x2(ReadSingle(Address), ReadSingle(Address + 4), ReadSingle(Address + 8), ReadSingle(Address + 12), ReadSingle(Address + 16), ReadSingle(Address + 20));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Matrix3x2</c> value to write.
        /// </param>
        public void WriteMatrix3x2(uint Address, Matrix3x2 Value)
        {
            WriteSingle(Address, Value.M11);
            WriteSingle(Address + 4, Value.M12);
            WriteSingle(Address + 8, Value.M21);
            WriteSingle(Address + 12, Value.M22);
            WriteSingle(Address + 16, Value.M31);
            WriteSingle(Address + 20, Value.M32);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Matrix4x4</c> at the given address.
        /// </returns>
        public Matrix4x4 ReadMatrix4x4(uint Address) => new Matrix4x4(ReadSingle(Address), ReadSingle(Address + 4), ReadSingle(Address + 8), ReadSingle(Address + 12), ReadSingle(Address + 16), ReadSingle(Address + 20), ReadSingle(Address + 24), ReadSingle(Address + 28), ReadSingle(Address + 32), ReadSingle(Address + 36), ReadSingle(Address + 40), ReadSingle(Address + 44), ReadSingle(Address + 48), ReadSingle(Address + 52), ReadSingle(Address + 56), ReadSingle(Address + 60));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Matrix4x4</c> value to write.
        /// </param>
        public void WriteMatrix4x4(uint Address, Matrix4x4 Value)
        {
            WriteSingle(Address, Value.M11);
            WriteSingle(Address + 4, Value.M12);
            WriteSingle(Address + 8, Value.M13);
            WriteSingle(Address + 12, Value.M14);
            WriteSingle(Address + 16, Value.M21);
            WriteSingle(Address + 20, Value.M22);
            WriteSingle(Address + 24, Value.M23);
            WriteSingle(Address + 28, Value.M24);
            WriteSingle(Address + 32, Value.M31);
            WriteSingle(Address + 36, Value.M32);
            WriteSingle(Address + 40, Value.M33);
            WriteSingle(Address + 44, Value.M34);
            WriteSingle(Address + 48, Value.M41);
            WriteSingle(Address + 52, Value.M42);
            WriteSingle(Address + 56, Value.M43);
            WriteSingle(Address + 60, Value.M44);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Box3D</c> at the given address.
        /// </returns>
        public Box3D ReadBox3D(uint Address) => new Box3D(ReadVector3(Address), ReadVector3(Address + 12));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Box3D</c> value to write.
        /// </param>
        public void WriteBox3D(uint Address, Box3D Value)
        {
            WriteVector3(Address, Value.Low);
            WriteVector3(Address + 12, Value.High);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Sphere</c> at the given address.
        /// </returns>
        public Sphere ReadSphere(uint Address) => new Sphere(ReadVector3(Address), ReadSingle(Address + 12));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Sphere</c> value to write.
        /// </param>
        public void WriteSphere(uint Address, Sphere Value)
        {
            WriteVector3(Address, Value.Centre);
            WriteSingle(Address + 12, Value.Radius);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>Smoother</c> at the given address.
        /// </returns>
        public Smoother ReadSmoother(uint Address) => new Smoother(ReadSingle(Address), ReadSingle(Address + 4));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>Smoother</c> value to write.
        /// </param>
        public void WriteSmoother(uint Address, Smoother Value)
        {
            WriteSingle(Address, Value.RollingAverage);
            WriteSingle(Address + 4, Value.Factor);
        }

        /// <summary>
        /// Reads <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to read.
        /// </param>
        /// <returns>
        /// The <c>SimVelocityState</c> at the given address.
        /// </returns>
        public SimVelocityState ReadSimVelocityState(uint Address) => new SimVelocityState(ReadVector3(Address), ReadVector3(Address + 12));

        /// <summary>
        /// Writes the given value to <see cref="ProcessMemory.Process"/>'s memory at the given address.
        /// </summary>
        /// <param name="Address">
        /// The address to write to.
        /// </param>
        /// <param name="Value">
        /// The <c>SimVelocityState</c> value to write.
        /// </param>
        public void WriteSimVelocityState(uint Address, SimVelocityState Value)
        {
            WriteVector3(Address, Value.Linear);
            WriteVector3(Address + 12, Value.Angular);
        }
    }
}
