using SHARMemory.Memory;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(PlayerAndCarInfoStruct))]
    public struct PlayerAndCarInfo
    {
        public const int Size = Vector3.Size + Vector3.Size + 4; // sizeof(bool) returns 1, not sure best way to handle padding

        public Vector3 PlayerPosition;

        public Vector3 ForceLocation;

        public bool DirtyFlag;

        public PlayerAndCarInfo(Vector3 playerPosition, Vector3 forceLocation, bool dirtyFlag)
        {
            PlayerPosition = playerPosition;
            ForceLocation = forceLocation;
            DirtyFlag = dirtyFlag;
        }

        public override string ToString() => $"{PlayerPosition} | {ForceLocation} | {DirtyFlag}";
    }

    internal class PlayerAndCarInfoStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new PlayerAndCarInfo(Memory.ReadStruct<Vector3>(Address), Memory.ReadStruct<Vector3>(Address + Vector3.Size), Memory.ReadBoolean(Address + Vector3.Size + Vector3.Size));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not PlayerAndCarInfo Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(PlayerAndCarInfo)}'.", nameof(Value));

            Memory.WriteStruct(Address, Value2.PlayerPosition);
            Memory.WriteStruct(Address + Vector3.Size, Value2.ForceLocation);
            Memory.WriteBoolean(Address + Vector3.Size + Vector3.Size, Value2.DirtyFlag);
        }
    }
}
