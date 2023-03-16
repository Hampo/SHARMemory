using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;

namespace SHARMemory.SHAR
{
    /// <summary>
    /// Handles all of SHAR's <see cref="Singleton{T}"/>s.
    /// </summary>
    public sealed class Singletons
    {
        private readonly Singleton<CharacterManager> CharacterManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.CharacterManager"/> singleton.
        /// </summary>
        public CharacterManager CharacterManager => CharacterManagerSingleton.Get();

        private readonly Singleton<CharacterSheetManager> CharacterSheetManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.CharacterSheetManager"/> singleton.
        /// </summary>
        public CharacterSheetManager CharacterSheetManager => CharacterSheetManagerSingleton.Get();

        private readonly Singleton<GameFlow> GameFlowSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.GameFlow"/> singleton.
        /// </summary>
        public GameFlow GameFlow => GameFlowSingleton.Get();

        private readonly Singleton<HitNRunManager> HitNRunManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.HitNRunManager"/> singleton.
        /// </summary>
        public HitNRunManager HitNRunManager => HitNRunManagerSingleton.Get();

        private readonly Singleton<InteriorManager> InteriorManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.InteriorManager"/> singleton.
        /// </summary>
        public InteriorManager InteriorManager => InteriorManagerSingleton.Get();

        private readonly Singleton<IntersectManager> IntersectManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.IntersectManager"/> singleton.
        /// </summary>
        public IntersectManager IntersectManager => IntersectManagerSingleton.Get();

        private readonly Singleton<LoadingManager> LoadingManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.LoadingManager"/> singleton.
        /// </summary>
        public LoadingManager LoadingManager => LoadingManagerSingleton.Get();

        private readonly Singleton<MissionManager> MissionManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.MissionManager"/> singleton.
        /// </summary>
        public MissionManager MissionManager => MissionManagerSingleton.Get();

        private readonly Singleton<TrafficManager> TrafficManagerSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.TrafficManager"/> singleton.
        /// </summary>
        public TrafficManager TrafficManager => TrafficManagerSingleton.Get();

        private readonly Singleton<VehicleCentral> VehicleCentralSingleton;
        /// <summary>
        /// A reference to SHAR's <see cref="Classes.VehicleCentral"/> singleton.
        /// </summary>
        public VehicleCentral VehicleCentral => VehicleCentralSingleton.Get();

        internal Singletons(Memory memory)
        {
            CharacterManagerSingleton = new(memory, memory.SelectAddress(0x6C8470, 0x6C8430, 0x6C8430, 0x6C8468));
            CharacterSheetManagerSingleton = new(memory, memory.SelectAddress(0x6C8984, 0x6C8944, 0x6C8944, 0x6C897C));
            GameFlowSingleton = new(memory, memory.SelectAddress(0x6C9014, 0x6C8FD4, 0x6C8FD4, 0x6C900C));
            HitNRunManagerSingleton = new(memory, memory.SelectAddress(0x6C84E0, 0x6C84A0, 0x6C84A0, 0x6C84D8));
            InteriorManagerSingleton = new(memory, memory.SelectAddress(0x6C8FF8, 0x6C8FB8, 0x6C8FB8, 0x6C8FF0));
            IntersectManagerSingleton = new(memory, memory.SelectAddress(0x6C87A4, 0x6C8764, 0x6C8764, 0x6C879C));
            LoadingManagerSingleton = new(memory, memory.SelectAddress(0x6C8FF4, 0x6C8FB4, 0x6C8FB4, 0x6C8FEC));
            MissionManagerSingleton = new(memory, memory.SelectAddress(0x6C8994, 0x6C8954, 0x6C8954, 0x6C898C));
            TrafficManagerSingleton = new(memory, memory.SelectAddress(0x6C8468, 0x6C8428, 0x6C8428, 0x6C8460));
            VehicleCentralSingleton = new(memory, memory.SelectAddress(0x6C84D8, 0x6C8498, 0x6C8498, 0x6C84D0));
        }
    }
}