using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOpenGl.GameObject;
using GameOpenGl.VAO;
using GLFW;
using OpenGL;
using System.Numerics;
using GameOpenGl.RenderProgram;
using GameOpenGl.Misc;
using GameOpenGl.ShaderProgram;

namespace GameOpenGl.Renders
{
    internal sealed class Render : IRender
    {

        private Window window;
        private SquareVAO _VAO;
        private Matrix4x4 _projectionMatrix;
        private IShaderProgram _shaderProgram;
        private uint _currentTextureId;
        private Matrix4x4 _matrixScale;

        private double _lastTime = 0;
        private double _currentTime, _deltaTime;

        public bool IsExit() => Glfw.WindowShouldClose(window);

        public Render(int widht, int height)
        {
            window = CreateWindow(widht, height);

            PrepareContext();

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.0472f, widht / height, 0.1f, 100);

            _VAO = new SquareVAO();

            _shaderProgram = new SquareTextureShader();

            GL.glUseProgram(_shaderProgram.GetShaderId());

            _matrixScale = Matrix4x4.CreateScale(0.2f);
            var modelLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "model");
            var modelMatrix =  Matrix4x4.CreateTranslation(-3f, -3.5f, 0f);
            GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());
        }

        private void PrepareContext()
        {
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.SwapInterval(0);

        }

        public void RenderFrame(IGameObject[] gameObjects)
        {
            _currentTime = Glfw.Time;

            _deltaTime = _currentTime - _lastTime;
            Console.WriteLine($"FPS: {(int)(1 / _deltaTime)}");

            Glfw.SwapBuffers(window);
            Glfw.PollEvents();
            //Glfw.WaitEvents();

            GL.glClearColor(0f, 0f, 0f, 1f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);

            foreach (var GameObject in gameObjects)
            {
                DrawGameObject(GameObject);
            }

            _lastTime = _currentTime;
            _currentTextureId = 0;
        }

        private void DrawGameObject(IGameObject gameObject)
        {

            GL.glUseProgram(_shaderProgram.GetShaderId());
            var viewLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "view");

            var objPos = gameObject.GetPosition();


            var viewMatrix = Matrix4x4.CreateTranslation(objPos.X - (0.44f * objPos.X), objPos.Y, 0);

            //GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());
            GL.glUniformMatrix4fv(viewLocation, 1, false, (viewMatrix * _matrixScale).ToFloatArray());

            if (gameObject.GetTextureId() != _currentTextureId)
            {
                _currentTextureId = gameObject.GetTextureId();
                GL.glBindTexture(GL.GL_TEXTURE_2D, gameObject.GetTextureId());
            }
            GL.glBindVertexArray(_VAO.IdVAO);

            unsafe
            {
                GL.glDrawElements(GL.GL_TRIANGLES, 6, GL.GL_UNSIGNED_INT, (void*)0);
            }

            //GL.glBindTexture(GL.GL_TEXTURE_2D, 0);

            GL.glBindVertexArray(0);
        }

        private Window CreateWindow(int width, int height)
        {
            return CreateWindow(width, height, "Problems got Kittens");
        }

        private Window CreateWindow(int width, int height, string Title)
        {
            var NewWindow = Glfw.CreateWindow(width, height, Title, GLFW.Monitor.None, Window.None);

            var screen = Glfw.PrimaryMonitor.WorkArea;

            var x = (screen.Width - width) / 2;
            var y = (screen.Height - height) / 2;

            Glfw.SetWindowPosition(NewWindow, x, y);

            Glfw.MakeContextCurrent(NewWindow);
            GL.Import(Glfw.GetProcAddress);

            return NewWindow;
        }
    }
}
