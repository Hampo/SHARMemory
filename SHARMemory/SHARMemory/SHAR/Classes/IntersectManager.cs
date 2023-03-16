namespace SHARMemory.SHAR.Classes
{
    public class IntersectManager : Class
    {
        public enum TerrainType : uint
        {
            Road,    // Default road terrain. Also used for sidewalk. This is default. If not set, it's this.
            Grass,   // Grass type terrain most everything else which isn't road or sidewalk.
            Sand,    // Sand type terrain.
            Gravel,  // Loose gravel type terrain.
            Water,   // Water on surface type terrain.
            Wood,    // Boardwalks, docks type terrain.
            Metal,   // Powerplant and other structures.
            Dirt,    // Dirt type terrain.
            NumTerrainTypes
        }

        public IntersectManager(Memory memory, uint address) : base(memory, address) { }
    }
}
