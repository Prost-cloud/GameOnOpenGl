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
        private Matrix4x4 _ZeroMatrix;

        public bool IsExit() => Glfw.WindowShouldClose(window);

        public Render(int widht, int height)
        {
            window = CreateWindow(widht, height);

            PrepareContext();

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.0472f, widht / height, 0.1f, 100);

            _VAO = new SquareVAO();

            _shaderProgram = new SquareTextureShader();

            _ZeroMatrix = new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -2, 0, 0, 0, 0, 1);
        }

        private void PrepareContext()
        {
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
        }

        public void RenderFrame(IGameObject[] gameObjects)
        {
            Glfw.SwapBuffers(window);
            Glfw.PollEvents();

            GL.glClearColor(0f, 0f, 0f, 1f);
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);

            foreach (var GameObject in gameObjects)
            {
                DrawGameObject(GameObject);
            }
        }

        private void DrawGameObject(IGameObject gameObject)
        {

            GL.glUseProgram(_shaderProgram.GetShaderId());
            var modelLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "model");
            var projectionLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "projection");
            var viewLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "view");
            var TextureLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "inTexture");

            var objPos = gameObject.GetPosition();

            //var modelMatrix = _ZeroMatrix * Matrix4x4.CreateTranslation(objPos.X, objPos.Y, 0);
            var modelMatrix =  Matrix4x4.CreateTranslation(0, 0, 0);

            ///var viewMatrix = Matrix4x4.CreateLookAt(
            ///    new Vector3(0.0f, 0.0f, 0.0f),
            ///    new Vector3(0.0f, 0.0f, 0.0f) + new Vector3(0.0f, 0.0f, 5.0f),
            ///    new Vector3(0.0f, 1.0f, 0.0f));

            var viewMatrix = Matrix4x4.CreateTranslation(objPos.X - (0.44f * objPos.X), objPos.Y, 0);

            var matrixScale = Matrix4x4.CreateScale(0.1f);

            GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());
            //GL.glUniformMatrix4fv(projectionLocation, 1, false, _projectionMatrix.ToFloatArray());
            GL.glUniformMatrix4fv(viewLocation, 1, false, (viewMatrix * matrixScale).ToFloatArray());

            //modelMatrix.ShowMatrix("model");
            //viewMatrix.ShowMatrix("view");
            //(modelMatrix * viewMatrix).ShowMatrix("model * view");
            //(viewMatrix * modelMatrix).ShowMatrix("view * model");

            //GL.glActiveTexture(GL.GL_TEXTURE0);
            Console.WriteLine($"Texture Id {gameObject.GetTextureId()}");
            GL.glBindTexture(GL.GL_TEXTURE_2D, gameObject.GetTextureId());
            //GL.glUniform1i(TextureLocation, 0);

            GL.glBindVertexArray(_VAO.IdVAO);

            unsafe
            {
                //GL.glDrawArrays(GL.GL_TRIANGLES, 0, 6);
                GL.glDrawElements(GL.GL_TRIANGLES, 6, GL.GL_UNSIGNED_INT, (void*) 0);
            }

            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);

            GL.glBindVertexArray(0);

            //  GL.glDrawElements(); 
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
