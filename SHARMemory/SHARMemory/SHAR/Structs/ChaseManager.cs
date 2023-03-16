using SHARMemory.Memory;
using System.Text;

namespace SHARMemory.SHAR.Structs
{
    [Struct(typeof(ChaseManagerStruct))]
    public struct ChaseManager
    {
        public const int Size = sizeof(uint) + 16 + 64;

        public Classes.ChaseManager ChaseManagerPtr;

        public string HostVehicle;

        public string HostVehicleFilename;

        public ChaseManager(Classes.ChaseManager chaseManagerPtr, string hostVehicle, string hostVehicleFilename)
        {
            ChaseManagerPtr = chaseManagerPtr;
            HostVehicle = hostVehicle;
            HostVehicleFilename = hostVehicleFilename;
        }

        public override string ToString() => $"{ChaseManagerPtr} | {HostVehicle} | {HostVehicleFilename}";
    }

    internal class ChaseManagerStruct : IStruct
    {
        public object Read(ProcessMemory Memory, uint Address) => new ChaseManager(Memory.ClassFactory.Create<Classes.ChaseManager>(Address), Memory.ReadString(Address + sizeof(uint), Encoding.UTF8, 16), Memory.ReadString(Address + sizeof(uint) + 16, Encoding.UTF8, 64));

        public void Write(ProcessMemory Memory, uint Address, object Value)
        {
            if (Value is not ChaseManager Value2)
                throw new System.ArgumentException($"Argument '{nameof(Value)}' must be of type '{nameof(ChaseManager)}'.", nameof(Value));

            Memory.WriteUInt32(Address, Value2.ChaseManagerPtr.Address);
            Memory.WriteString(Address + sizeof(uint), Value2.HostVehicle, Encoding.UTF8, 16);
            Memory.WriteString(Address + sizeof(uint) + 16, Value2.HostVehicleFilename, Encoding.UTF8, 64);
        }
    }
}
