using System;

namespace SHARMemory.SHAR.Events.Character;

public class CollisionEventArgs : EventArgs
{
    public Classes.Character Character { get; }

    public CollisionEventArgs(Classes.Character character)
    {
        Character = character;
    }
}