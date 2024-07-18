using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;
using SHARMemory.SHAR.Structs;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVStatePropDSG@@")]
    public class StatePropDSG : DynaPhysDSG
    {
        public StatePropDSG(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        internal const uint CStatePropListenerVFTableOffset = GroundPlaneRefsOffset + sizeof(int);

        internal const uint TransformOffset = CStatePropListenerVFTableOffset + sizeof(uint);
        public Matrix4x4 Transform
        {
            get => ReadStruct<Matrix4x4>(TransformOffset);
            set => WriteStruct(TransformOffset, value);
        }

        internal const uint StatePropOffset = TransformOffset + Matrix4x4.Size;
        public CStateProp StateProp => Memory.ClassFactory.Create<CStateProp>(ReadUInt32(StatePropOffset));

        internal const uint PhysObjOffset = StatePropOffset + sizeof(uint);
        public PhysicsObject PhysObj => Memory.ClassFactory.Create<PhysicsObject>(ReadUInt32(PhysObjOffset));
    }
}
