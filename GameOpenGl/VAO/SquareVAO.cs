using OpenGL;

namespace GameOpenGl.VAO
{
    internal sealed class SquareVAO
    {
        private uint _idVAO;
        private uint _idVBO;
        private uint _idIBO;
        public uint IdVAO => _idVAO;
        public SquareVAO()
        {
            //float[] vertices =
            //{
            //    1.0f, 1.0f, 1.0f,    1.0f, 1.0f, // Upper Right
            //    1.0f, -1.0f, 1.0f,   1.0f, 0.0f, // Down Right
            //    -1.0f, -1.0f, 0.0f,  0.0f, 0.0f, // Down Left
            //    -1.0f, 1.0f, 0.0f,   0.0f, 1.0f  // Upper left
            //};

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

            _idVAO = GL.glGenVertexArray();
            _idVBO = GL.glGenBuffer();
            _idIBO = GL.glGenBuffer();

            GL.glBindVertexArray(_idVAO);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, _idVBO);
            unsafe
            {
                fixed (float* v = &vertices[0])
                    GL.glBufferData(GL.GL_ARRAY_BUFFER, sizeof(float) * vertices.Count(), v, GL.GL_DYNAMIC_DRAW);

                GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 5 * sizeof(float), null);
                GL.glEnableVertexAttribArray(0u);
                GL.glVertexAttribPointer(1, 2, GL.GL_FLOAT, false, 5 * sizeof(float), new IntPtr(3 * sizeof(float)));
                //GL.glVertexAttribPointer(1, 2, GL.GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));
                GL.glEnableVertexAttribArray(1u);
            }

            GL.glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, _idIBO);
            unsafe
            {
                fixed (uint* i = &indices[0])
                    GL.glBufferData(GL.GL_ELEMENT_ARRAY_BUFFER, sizeof(uint) * indices.Count(), i, GL.GL_DYNAMIC_DRAW);
            }

            GL.glBindVertexArray(0);
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, 0);
            GL.glBindBuffer(GL.GL_ELEMENT_ARRAY_BUFFER, 0);

        }
    }
}
