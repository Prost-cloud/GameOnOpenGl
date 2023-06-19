using GameOpenGl.GameObject;
using GLFW;
using OpenGL;
using System.Numerics;
using GameOpenGl.Misc;
using GameOpenGl.Render.Object2D;
using GameOpenGl.ShaderProgram;

namespace GameOpenGl.Renders
{
    internal sealed class GameRender : IRender
    {
        private Object2D[] object2Ds;

        static private Window _window;
        private uint _currentTextureId;
        private Matrix4x4 _matrixScale;

        //private int _isNeedRerender = 2;

        public bool IsExit() => Glfw.WindowShouldClose(_window);

        public static Window PrepareWindow(int widht, int height)
        {
            _window = CreateWindow(widht, height);

            PrepareContext();

            return _window;
        }
        public GameRender(Window window, IGameObject[] gameObjects)
        {
            _window = window;
            //_projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(1.0472f, widht / height, 0.1f, 100);
            uint programID = new BaseShader().GetOrCreateShaderId();
            GL.glUseProgram(programID);

            object2Ds = gameObjects.Select(x => new Object2D(x)).ToArray();

            _matrixScale = Matrix4x4.CreateScale(0.2f);
            var modelLocation = GL.glGetUniformLocation(programID, "model");
            var modelMatrix = Matrix4x4.CreateTranslation(-3f, -3.5f, 0f);
            GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());

            _currentTextureId = 0;
        }

        private static void PrepareContext()
        {
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.SwapInterval(1);
            GL.glClearColor(0f, 0f, 0f, 1f);
            //GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDisable(GL.GL_DEPTH_TEST);
        }

        public void RenderFrame()
        {

            Glfw.SwapBuffers(_window);
            Glfw.PollEvents();
            //Glfw.WaitEvents();

            //if (_isNeedRerender != 0)
            //{
            //    GL.glClear(GL.GL_COLOR_BUFFER_BIT);
            //}

            foreach (var GameObject in object2Ds)
            {
                DrawGameObject(GameObject);
            }

            //if (_isNeedRerender != 0)
            //{
            //    _isNeedRerender--;
            //}
        }

        private void DrawGameObject(Object2D renderObject)
        {
            if (renderObject.IsNeedRender == 0)
            {
                return;
            }

            GL.glUseProgram(renderObject.VAO);
            var viewLocation = GL.glGetUniformLocation(renderObject.ShaderProgram, "view");

            var objPos = renderObject.GetPosition();


            var viewMatrix = Matrix4x4.CreateTranslation(objPos.X - (0.44f * objPos.X), objPos.Y, 0);

            //GL.glUniformMatrix4fv(modelLocation, 1, false, modelMatrix.ToFloatArray());
            GL.glUniformMatrix4fv(viewLocation, 1, false, (viewMatrix * _matrixScale).ToFloatArray());

            if (renderObject.GetTextureId() != _currentTextureId)
            {
                _currentTextureId = renderObject.GetTextureId();
                GL.glBindTexture(GL.GL_TEXTURE_2D, renderObject.GetTextureId());
            }
            GL.glBindVertexArray(renderObject.VAO);

            unsafe
            {
                GL.glDrawElements(GL.GL_TRIANGLES, 6, GL.GL_UNSIGNED_INT, (void*)0);
            }

            //GL.glBindTexture(GL.GL_TEXTURE_2D, 0);

            GL.glBindVertexArray(0);

            renderObject.IsNeedRender--;
        }

        private static Window CreateWindow(int width, int height)
        {
            return CreateWindow(width, height, "Problems got Kittens");
        }

        private static Window CreateWindow(int width, int height, string title)
        {
            var NewWindow = Glfw.CreateWindow(width, height, title, GLFW.Monitor.None, Window.None);

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
