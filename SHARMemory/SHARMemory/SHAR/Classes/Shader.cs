namespace SHARMemory.SHAR.Classes
{
    public class Shader : Class
    {
        public Shader(Memory memory, uint address) : base(memory, address) { }

        public d3dShader D3DShader => Memory.CreateClass<d3dShader>(ReadUInt32(20));
    }
}
