using SHARMemory.SHAR.Classes;
using System;

namespace SHARMemory.SHAR.Events.Character;

public class VehicleHealthChangedEventArgs : EventArgs
{
    public Vehicle Vehicle { get; }
    public float LastHealth { get; }
    public float NewHealth { get; }

    public VehicleHealthChangedEventArgs(Vehicle vehicle, float lastHealth, float newHealth)
    {
        Vehicle = vehicle;
        LastHealth = lastHealth;
        NewHealth = newHealth;
    }
}