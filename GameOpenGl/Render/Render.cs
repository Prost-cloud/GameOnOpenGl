using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;

namespace GameOpenGl.Renders
{
    internal sealed class Render : IRender
    {

        Window window;

        public Render(int widht, int height)
        {
            window = CreateWindow(widht, height);

        }

        public void RenderFrame()
        {
                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
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

            Glfw.SetWindowPosition(window, x, y);

            Glfw.MakeContextCurrent(NewWindow);
            GL.Import(Glfw.GetProcAddress);

            return NewWindow;
        }
    }
}
