using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVTriggerVolumeTracker@@")]
public class TriggerVolumeTracker : EventListener
{
    public const int MAX_VOLUMES = 500;
    public const int MAX_ACTIVE = 20;
    public const int MAX_AI = 10;
    public const int MAX_AI_VOLUMES = 100;

    public TriggerVolumeTracker(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator)
    {
        if (memory.ModLauncherOrdinalAddresses.TryGetValue(Memory.ModLauncherOrdinals.MaxTriggers, out uint MaxTriggersAddress) && memory.ModLauncherOrdinalAddresses.TryGetValue(Memory.ModLauncherOrdinals.TriggersOffset, out uint TriggersOffsetAddress))
        {
            _maxTriggerVolumes = memory.ReadInt32(MaxTriggersAddress);
            _triggerVolumesOffset = memory.ReadUInt32(TriggersOffsetAddress);
        }
        else
        {
            _maxTriggerVolumes = MAX_VOLUMES;
            _triggerVolumesOffset = TriggerVolumesOffset;
        }
    }

    internal const uint TriggerSphereOffset = EventListenerVFTableOffset + sizeof(uint);
    public tDrawable TriggerSphere => Memory.ClassFactory.Create<tDrawable>(ReadUInt32(TriggerSphereOffset));

    internal const uint TriggerCountOffset = TriggerSphereOffset + sizeof(uint);
    public uint TriggerCount
    {
        get => ReadUInt32(TriggerCountOffset);
        set => WriteUInt32(TriggerCountOffset, value);
    }

    internal const uint TriggerVolumesOffset = TriggerCountOffset + sizeof(uint);
    private readonly uint _triggerVolumesOffset;
    private readonly int _maxTriggerVolumes;
    public PointerArray<TriggerVolume> TriggerVolumes => new(Memory, Address + _triggerVolumesOffset, _maxTriggerVolumes);
}
