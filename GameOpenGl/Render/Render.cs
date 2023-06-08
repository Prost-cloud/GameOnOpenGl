using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOpenGl.VAO;
using GLFW;
using OpenGL;

namespace GameOpenGl.Renders
{
    internal sealed class Render : IRender
    {

        private Window window;
        private SquareVAO _IBO;

        public Render(int widht, int height)
        {
            window = CreateWindow(widht, height);

            PrepareContext();
        }

        private void PrepareIBO()
        {

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

        public void RenderFrame()
        {
            while (!Glfw.WindowShouldClose(window))
            {
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }
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
