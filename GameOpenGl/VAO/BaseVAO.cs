using OpenGL;

namespace GameOpenGl.VAO
{
    internal class BaseVAO : IVAO
    {
        static private Dictionary<Type, uint> _VAODictionary = new();

        public uint GetOrCreateVaoId()
        {
            if(_VAODictionary.TryGetValue(this.GetType(), out var id))
            {
                return id;
            }

            float[] vertices = {
                0.28f, 0.5f, 0.0f,    1.0f, 1.0f, // Upper Right
                0.28f, -0.5f, 0.0f,   1.0f, 0.0f, // Down Right
                -0.28f, -0.5f, 0.0f,  0.0f, 0.0f, // Down Left
                -0.28f, 0.5f, 0.0f,   0.0f, 1.0f  // Upper left
            };
        
            uint[] indices =
            {
                0, 1, 2,
                2, 3, 0
            };
        
            var idVAO = GL.glGenVertexArray();
            var idVBO = GL.glGenBuffer();
            var idIBO = GL.glGenBuffer();
        
            GL.glBindVertexArray(idVAO);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, idVBO);
            unsafe
            {
                fixed (float* v = &vertices[0])
                    GL.glBufferData(GL.GL_ARRAY_BUFFER, sizeof(float) * vertices.Count(), v, GL.GL_DYNAMIC_DRAW);
        
                GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 5 * sizeof(float), null);
                GL.glEnableVertexAttribArray(0u);
                GL.glVertexAttribPointer(1, 2, GL.GL_FLOAT, false, 5 * sizeof(float), new IntPtr(3 * sizeof(float)));
                GL.glEnableVertexAttribArray(1u);
            }
        
            GL.glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, idIBO);
            unsafe
            {
                fixed (uint* i = &indices[0])
                    GL.glBufferData(GL.GL_ELEMENT_ARRAY_BUFFER, sizeof(uint) * indices.Count(), i, GL.GL_DYNAMIC_DRAW);
            }
        
            GL.glBindVertexArray(0);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, 0);
            GL.glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, 0);
        
            RegisterVAO(this.GetType(), idVAO);

            return idVAO;
        }

        protected static void RegisterVAO(Type type, uint vao)
        {
            _VAODictionary.Add(type, vao);
        } 
    }
}
