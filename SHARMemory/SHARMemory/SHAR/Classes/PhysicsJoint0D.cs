using SHARMemory.Memory;
using SHARMemory.Memory.RTTI;

namespace SHARMemory.SHAR.Classes;

[ClassFactory.TypeInfoName(".?AVPhysicsJoint0D@sim@@")]
public class PhysicsJoint0D : PhysicsJoint
{
    public PhysicsJoint0D(Memory memory, uint address, CompleteObjectLocator completeObjectLocator) : base(memory, address, completeObjectLocator) { }

    public override void ResetDeformation()
    {

    }
}
