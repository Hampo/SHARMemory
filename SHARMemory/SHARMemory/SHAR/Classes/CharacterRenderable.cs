using SHARMemory.Memory;

namespace SHARMemory.SHAR.Classes
{
    public class CharacterRenderable : Class
    {
        public CharacterRenderable(Memory memory, uint address) : base(memory, address) { }

        public PointerArray<DrawablePose> DrawableList => new(Memory, Address, 3);

        public int CurrentLOD
        {
            get => ReadInt32(12);
            set => WriteInt32(12, value);
        }

        public bool InAnyonesFrustrum
        {
            get => ReadBoolean(16);
            set => WriteBoolean(16, value);
        }

        public Texture SwatchTexture => Memory.CreateClass<Texture>(ReadUInt32(20));

        public PointerArray<Texture> SwatchTextures => new(Memory, Address + 24, 5);

        public Shader SwatchShader => Memory.CreateClass<Shader>(ReadUInt32(44));
    }
}
