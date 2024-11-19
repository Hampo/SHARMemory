using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVMerchandise@@")]
public class Merchandise : Reward
{
    public enum SellerTypes
    {
        Invalid = -1,
        Interior,
        Simpson,
        Gil
    }

    public Merchandise(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    internal const uint CostOffset = RepairCostOffset + sizeof(int);
    public int Cost
    {
        get => ReadInt32(CostOffset);
        set => WriteInt32(CostOffset, value);
    }

    internal const uint SellerTypeOffset = CostOffset + sizeof(int);
    public SellerTypes SellerType
    {
        get => (SellerTypes)ReadInt32(SellerTypeOffset);
        set => WriteInt32(SellerTypeOffset, (int)value);
    }
}
