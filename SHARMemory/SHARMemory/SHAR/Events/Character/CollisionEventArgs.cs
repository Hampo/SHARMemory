using System;

namespace SHARMemory.SHAR.Events.Character;

public class CollisionEventArgs : EventArgs
{
    public Classes.Character Character { get; }
    public bool InVehicle { get; }

    public CollisionEventArgs(Classes.Character character, bool inVehicle)
    {
        Character = character;
        InVehicle = inVehicle;
    }
}