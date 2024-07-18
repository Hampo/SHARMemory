using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVMissionObjective@@")]
public class MissionObjective : Class
{
    public enum DirectionalArrowType
    {
        Intersection = 0x01,
        NearestRoad = 0x10,
        Both = 0x11,
        Neither = 0x100,
        NumTypes = 4
    }

    public enum ObjectiveTypes
    {
        Invalid,
        Goto,
        Delivery,
        Follow,
        Destroy,
        Race,
        LoseTail,
        TalkTo,
        Dialogue,
        GetIn,
        Dump,
        FMV,
        Interior,
        Coin,
        DestroyBoss,
        LoadVehicle,
        PickupItem,
        Timer,
        BuyCar,
        BuySkin,
        GoOutside,
        NumObjectives
    };

    public MissionObjective(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public DirectionalArrowType ArrowType
    {
        get => (DirectionalArrowType)ReadInt32(4);
        set => WriteInt32(4, (int)value);
    }

    public ObjectiveTypes ObjectiveType
    {
        get => (ObjectiveTypes)ReadInt32(8);
        set => WriteInt32(8, (int)value);
    }

    public bool Finished
    {
        get => ReadBoolean(12);
        set => WriteBoolean(12, value);
    }
}
