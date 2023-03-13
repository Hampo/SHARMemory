using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SHARMemory.SHAR.Classes;
using SHARMemory.SHAR.Pointers;

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
        /// A reference to SHAR's static <c>FeTextBible</c>.
        /// </summary>
        public FeTextBible FeTextBible { get; }

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
        /// Create an instance of a <see cref="Class"/> at the given address.
        /// </summary>
        /// <typeparam name="T">
        /// The <see cref="Class"/> type to use.
        /// </typeparam>
        /// <param name="Address">
        /// The base address of the class.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="Class"/> or <c>null</c>.
        /// </returns>
        public T CreateClass<T>(uint Address) where T : Class
        {
            if (Address == 0)
                return null;

            return (T)Activator.CreateInstance(typeof(T), this, Address);
        }

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

            CharacterManager = new(this);
            CharacterSheetManager = new(this);
            CharacterTune = new(this);
            FeTextBible = new(this);
            GameFlow = new(this);
            GameplayManager = new(this);
            HitNRunManager = new(this);
            InteriorManager = new(this);
            IntersectManager = new(this);
            LoadingManager = new(this);
            TrafficManager = new(this);
            VehicleCentral = new(this);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static IntPtr GetGameWindow() => FindWindow("The Simpsons Hit & Run", null);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, ref int lpwdProcessId);

        /// <summary>
        /// Tries to get a SHAR <c>Process</c> based on window class.
        /// </summary>
        /// <returns>
        /// A SHAR <c>Process</c> or <c>null</c>.
        /// </returns>
        public static Process GetSHARProcess()
        {
            IntPtr GameWindow = GetGameWindow();
            if (GameWindow == IntPtr.Zero)
                return null;

            int ProcessId = 0;
            _ = GetWindowThreadProcessId(GameWindow, ref ProcessId);
            return Process.GetProcessById(ProcessId);
        }

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
        public object ReadStruct(Type Type, uint Address) => StructAttribute.Get(Type).Read(this, Address);

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
        public void WriteStruct(Type Type, uint Address, object Value) => StructAttribute.Get(Type).Write(this, Address, Value);

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
    }
}
