# SHARMemory
A class library designed to help with interacting with SHAR memory. Written with support for `.NET Standard 2.0` and `.NET Framework 4.5`.

## Special Thanks
A special thanks to [Lucas Cardellini](https://github.com/lucasc190) for the original memory classes and concept, plus all their additional help with locating the pointer addresses for the different game versions.

## Setup
* Make sure your project targets `x86` (32-bit).
* Download the [latest release](https://github.com/Hampo/SHARMemory/releases/latest) or clone and build the repository.
* Add `SHARMemory.dll` as a reference in your project.

## Usage
The first step is to get a running SHAR `Process`. There is a helper function, which will either return a `Process`, or `null` if not found:
```cs
var p = SHARMemory.SHAR.Memory.GetSHARProcess();
if (p == null)
    return; // Process not found, return.
```
Once a SHAR `Process` has been found, that can be passed to the constructor of `SHAR.Memory`:
```cs
var sharMemory = new SHARMemory.SHAR.Memory(p);
```
`SHAR.Memory` contains a `Globals` class for static values and a `Singletons` class for single instance classes. One such singleton class is `HitNRunManager`.

Singleton classes will return `null` if they're not yet initialised:
```cs
var hitNRunManager = sharMemory.Singletons.HitNRunManager;
if (hitNRunManager == null)
    return; // HitNRunManager not initialised, return.
```
Once you have verified the instance is initialised, you can use the properties within to affect the game:
```cs
HitNRunManager.HitAndRun = 100f;
```
Putting that all together you get:
```cs
void TriggerHitAndRun()
{
    var p = SHARMemory.SHAR.Memory.GetSHARProcess();
    if (p == null)
        return;
    var sharMemory = new SHARMemory.SHAR.Memory(p);

    var hitNRunManager = sharMemory.Singletons.HitNRunManager;
    if (hitNRunManager == null)
        return; // HitNRunManager not initialised, return.

    hitNRunManager.HitAndRun = 100f;
}
```
