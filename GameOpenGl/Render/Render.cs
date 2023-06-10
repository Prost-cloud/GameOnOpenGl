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

        public bool IsExit() => Glfw.WindowShouldClose(window);

        public Render(int widht, int height)
        {
            window = CreateWindow(widht, height);

            PrepareContext();

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.0472f, widht / height, 0.1f, 100);

            _VAO = new SquareVAO();

            _shaderProgram = new SquareTextureShader();

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

            foreach (var GameObject in gameObjects)
            {
                DrawGameObject(GameObject);
            }
        }

        private void DrawGameObject(IGameObject gameObject)
        {

            GL.glUseProgram(_shaderProgram.GetShaderId());
            var modelLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "model");
            var ProjectionLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "projection");
            var viewLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "view");
            var TextureLocation = GL.glGetUniformLocation(_shaderProgram.GetShaderId(), "inTexture");

            var objPos = gameObject.GetPosition();

            var modelMatrix = new Matrix4x4() * Matrix4x4.CreateTranslation(objPos.X, objPos.Y, 0);

            var viewMatrix = Matrix4x4.CreateLookAt(
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, 0.0f, 0.0f) + new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 1.0f, 0.0f));

            GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());
            GL.glUniformMatrix4fv(ProjectionLocation, 1, false, _projectionMatrix.ToFloatArray());
            GL.glUniformMatrix4fv(viewLocation, 1, false, viewMatrix.ToFloatArray());

            GL.glActiveTexture(GL.GL_TEXTURE0);
            GL.glBindTexture(GL.GL_TEXTURE_2D, gameObject.GetTextureId());
            GL.glUniform1i(TextureLocation, 0);

            GL.glBindVertexArray(_VAO.IdVAO);

            unsafe
            {
                // GL.glDrawArrays(GL.GL_TRIANGLES, 0, 6);
                GL.glDrawElements(GL.GL_TRIANGLES, 6, GL.GL_UNSIGNED_INT, (void*) 0);
            }

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
