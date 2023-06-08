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
    internal sealed class SquareTextureShader : IShaderProgram
    {
        public uint ProgramID;

        private const string VertexShader = @"#version 330 core
                                                layout (location = 0) in vec3 pos;
                                                layout (location = 1) in vec2 inTexCoord;

                                                uniform vec4 model;
                                                uniform vec4 view;
                                                uniform vec4 projection;
                                                    
                                                out vec2 texCoord;

                                                void main()
                                                {
                                                    position = projection * view * model * vec4(pos, 1.0);
                                                    texCoord = (inTexCoord.x, 1.0 - inTexCood.y);
                                                }";

        private const string FragmentShader = @"#version 330 core
                                                    
                                                in vec2 texCoord;
                                                    
                                                out vec4 result;

                                                uniform sampler2D inTexture;

                                                void main()
                                                {
                                                    result = texture(inTexture, texCoord);
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

            //GL.glUseProgram(ProgramID);
        }

        private uint CreateShader(int type, string source)
        {
            uint shader = GL.glCreateShader(type);

            GL.glShaderSource(shader, source);
            GL.glCompileShader(shader);

            return shader;
        }

       // ~TriangeShader()
       // {
       //     GL.glDeleteProgram(ProgramID);
       // }
    }
}
