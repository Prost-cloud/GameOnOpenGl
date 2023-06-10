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
        private uint _programID;

        private const string VertexShader = @"#version 330 core
                                                layout (location = 0) in vec3 pos;
                                                layout (location = 1) in vec2 inTexCoord;

                                                uniform mat4 model;
                                                uniform mat4 view;
                                                uniform mat4 projection;
                                                    
                                                out vec2 texCoord;

                                                void main()
                                                {
                                                   // position = projection * view * model * vec4(pos.x, pos.y, pos.z, 1.0);
                                                    texCoord = (inTexCoord.x, 1.0 - inTexCood.y);
                                                    position = vec4(pos.x, pos.y, pos.z, 1.0);
                                                }";

        private const string FragmentShader = @"#version 330 core
                                                    
                                                in vec2 texCoord;
                                                    
                                                out vec4 result;

                                                uniform sampler2D inTexture;

                                                void main()
                                                {
                                                    result = texture(inTexture, texCoord);
                                                }";

        public SquareTextureShader()
        {
            var vertex = CreateShader(GL.GL_VERTEX_SHADER, VertexShader);
            var fragment = CreateShader(GL.GL_FRAGMENT_SHADER, FragmentShader);

            _programID = GL.glCreateProgram();

            GL.glCompileShader(vertex);
            GL.glCompileShader(fragment);

            GL.glAttachShader(_programID, vertex);   
            GL.glAttachShader(_programID, fragment);

            GL.glLinkProgram(_programID);

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

        public uint GetShaderId()
        {
            return _programID;
        }

        // ~TriangeShader()
        // {
        //     GL.glDeleteProgram(ProgramID);
        // }
    }
}
