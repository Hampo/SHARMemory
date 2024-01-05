using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes
{
    [ClassFactory.TypeInfoName(".?AVAnimationPlayer@@")]
    public class AnimationPlayer : Class
    {
        public enum AnimState
        {
            Idle,
            Loading,
            Loaded,
            Playing,
            Stopped,
            NumStates
        }

        public AnimationPlayer(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

        public AnimState State => (AnimState)ReadUInt32(4);
    }
}
