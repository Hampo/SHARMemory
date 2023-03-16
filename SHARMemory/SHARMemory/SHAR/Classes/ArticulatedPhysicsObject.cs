using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVArticulatedPhysicsObject@sim@@")]
    public class ArticulatedPhysicsObject : PhysicsObject
    {
        public ArticulatedPhysicsObject(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public float DissipationDeformationRate
        {
            get => ReadSingle(484);
            set => WriteSingle(484, value);
        }

        public float DissipationDeformationSpeedRate
        {
            get => ReadSingle(488);
            set => WriteSingle(488, value);
        }

        public float DissipationInternalRate
        {
            get => ReadSingle(492);
            set => WriteSingle(492, value);
        }
    }
}
