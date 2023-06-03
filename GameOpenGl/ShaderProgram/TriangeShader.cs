using GameOpenGl.RenderProgram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;

namespace GameOpenGl.ShaderProgram
{
    internal sealed class TriangeShader : IShaderProgram
    {
        public uint ProgramID;

        private const string VertexShader = @"#version 330 core
                                                layout (location = 0) in vec3 pos;

                                                void main()
                                                {
                                                    gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
                                                }";

        private const string FragmentShader = @"#version 330 core
                                                out vec4 result;

                                                uniform vec3 color;

                                                void main()
                                                {
                                                    result = vec4(color, 1.0);
                                                }";

        private uint vertex;
        private uint fragment;

        public void PrepareShader()
        {
            vertex = CreateShader(GL.GL_VERTEX_SHADER, VertexShader);
            fragment = CreateShader(GL.GL_FRAGMENT_SHADER, FragmentShader);
        }

        public void PushShader()
        {
            ProgramID = GL.glCreateProgram();

            GL.glAttachShader(ProgramID, vertex);   
            GL.glAttachShader(ProgramID, fragment);

            GL.glLinkProgram(ProgramID);

            GL.glDeleteShader(vertex);
            GL.glDeleteShader(fragment);

            GL.glUseProgram(ProgramID);
        }

        private uint CreateShader(int type, string source)
        {
            uint shader = GL.glCreateShader(type);

            GL.glShaderSource(shader, source);
            GL.glCompileShader(shader);

            return shader;
        }

        ~TriangeShader()
        {
            GL.glDeleteProgram(ProgramID);
        }
    }
}
