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
    internal class BaseShader : IShaderProgram
    {
        static private Dictionary<Type, uint> _shaderProgramDictionary = new();

        private const string VertexShader = @"#version 330 core
                                                layout (location = 0) in vec3 pos;
                                                layout (location = 1) in vec2 inTexCoord;

                                                uniform mat4 model;
                                                uniform mat4 view;
                                                uniform mat4 projection;
                                                    
                                                out vec2 texCoord;

                                                void main()
                                                {
                                                    //gl_Position = projection * view * model * vec4(pos.x, pos.y, pos.z, 1.0);
                                                    gl_Position = view * model * vec4(pos.x, pos.y, pos.z, 1.0);
                                                    texCoord = vec2(inTexCoord.x, 1.0f - inTexCoord.y);
                                                    //texCoord = vec2(inTexCoord.x, 1.0f - inTexCoord.y);
                                                    //gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
                                                }";

        private const string FragmentShader = @"#version 330 core
                                                    
                                                in vec2 texCoord;
                                                    
                                                out vec4 result;

                                                uniform sampler2D inTexture;

                                                void main()
                                                {
                                                    result = texture(inTexture, texCoord);
                                                    //result = vec4(1f, 0f, 0.5f, 1);
                                                    //gl_FragColor = texture(inTexture, texCoord);
                                                    //result = vec4(1f, 0f, 0f, 0f);
                                                }";

        public uint GetOrCreateShaderId()
        {
            if (_shaderProgramDictionary.TryGetValue(this.GetType(), out uint id))
            {
                return id;
            }

            var vertex = CreateShader(GL.GL_VERTEX_SHADER, VertexShader);
            var fragment = CreateShader(GL.GL_FRAGMENT_SHADER, FragmentShader);

            var programID = GL.glCreateProgram();

            GL.glAttachShader(programID, vertex);
            GL.glAttachShader(programID, fragment);

            GL.glLinkProgram(programID);

            GL.glDetachShader(programID, vertex);
            GL.glDetachShader(programID, fragment);

            GL.glDeleteShader(vertex);
            GL.glDeleteShader(fragment);

            RegisterShader(this.GetType(), programID);

            return programID;
        }

        private uint CreateShader(int type, string source)
        {
            uint shader = GL.glCreateShader(type);

            GL.glShaderSource(shader, source);
            GL.glCompileShader(shader);

            int[] status = GL.glGetShaderiv(shader, GL.GL_COMPILE_STATUS, 1);

            if (status[0] == 0)
            {
                string error = GL.glGetShaderInfoLog(shader);
                Console.WriteLine("Shader compiling error: " + error);
            }

            return shader;
        }

        static protected void RegisterShader(Type shaderType, uint programId)
        {
            _shaderProgramDictionary.Add(shaderType, programId);
        }
    }
}
