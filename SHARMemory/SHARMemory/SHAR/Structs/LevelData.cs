using SHARMemory.Memory;
using SHARMemory.SHAR.Classes;
using SHARMemory.SHAR.Pointers;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(LevelDataStruct))]
    public struct LevelData
    {
        public const int Size = sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int) + sizeof(int) + sizeof(RenderEnums.MissionEnum);

        public RenderEnums.LevelEnum Level;

        public int NumMissions;

        public GameplayManager.Ratings[] Ratings;

        public int NumBonusCollectibles;

        public int NumBonusCollected;

        public RenderEnums.MissionEnum Mission;

        public LevelData(RenderEnums.LevelEnum level, int numMissions, GameplayManager.Ratings[] ratings, int numBonusCollectibles, int numBonusCollected, RenderEnums.MissionEnum mission)
        {
            Level = level;
            NumMissions = numMissions;
            Ratings = ratings;
            NumBonusCollectibles = numBonusCollectibles;
            NumBonusCollected = numBonusCollected;
            Mission = mission;
        }

        public override string ToString() => $"{Level} | {NumMissions} | {Ratings} | {NumBonusCollectibles} | {NumBonusCollected} | {Mission}";
    }

    internal class LevelDataStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address)
        {
            RenderEnums.LevelEnum Level = (RenderEnums.LevelEnum)Memory.ReadInt32(Address);
            int NumMissions = Memory.ReadInt32(Address + sizeof(RenderEnums.LevelEnum));
            GameplayManager.Ratings[] Ratings = new GameplayManager.Ratings[GameplayManager.MAX_MISSIONS];
            for (uint i = 0; i < GameplayManager.MAX_MISSIONS; i++)
                Ratings[i] = (GameplayManager.Ratings)Memory.ReadInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * i);
            int NumBonusCollectibles = Memory.ReadInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS);
            int NumBonusCollected = Memory.ReadInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int));
            RenderEnums.MissionEnum Mission = (RenderEnums.MissionEnum)Memory.ReadInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int) + sizeof(int));

            return new LevelData(Level, NumMissions, Ratings, NumBonusCollectibles, NumBonusCollected, Mission);
        }

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not LevelData Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(LevelData)}'.", nameof(Value));

            Memory.WriteInt32(Address, (int)Value2.Level);
            Memory.WriteInt32(Address + sizeof(int), Value2.NumMissions);
            for (uint i = 0; i < GameplayManager.MAX_MISSIONS; i++)
                Memory.WriteInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * i, (int)Value2.Ratings[i]);
            Memory.WriteInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS, Value2.NumBonusCollectibles);
            Memory.WriteInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int), Value2.NumBonusCollected);
            Memory.WriteInt32(Address + sizeof(RenderEnums.LevelEnum) + sizeof(int) + sizeof(GameplayManager.Ratings) * GameplayManager.MAX_MISSIONS + sizeof(int) + sizeof(int), (int)Value2.Mission);
        }
    }
}