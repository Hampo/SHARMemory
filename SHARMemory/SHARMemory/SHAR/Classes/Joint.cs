using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    public class Joint : Class
    {
        public enum DirtyEnum
        {
            None,
            Matrix,
            Quaternion
        }

        public Joint(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public int Index
        {
            get => ReadInt32(0);
            set => WriteInt32(0, value);
        }

        public Joint ParentJoint => Memory.ClassFactory.Create<Joint>(ReadUInt32(4));

        public int MaxChildJointCount
        {
            get => ReadInt32(8);
            set => WriteInt32(8, value);
        }

        public int ChildJointCount
        {
            get => ReadInt32(12);
            set => WriteInt32(12, value);
        }

        public PointerArray<Joint> ChildJoints => new(Memory, ReadUInt32(16), ChildJointCount);

        public Transform Object => Memory.ClassFactory.Create<Transform>(Address + 20);

        public Transform World => Memory.ClassFactory.Create<Transform>(Address + 104);


        public DirtyEnum Dirty
        {
            get => (DirtyEnum)ReadInt32(188);
            set => WriteInt32(188, (int)value);
        }
    }
}
