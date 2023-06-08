using GameOpenGl.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;
using GameOpenGl.Renders;

namespace GameOpenGl.Game
{
    internal class Game
    {
        private ILevel currentLevel;
        private IRender render;

        //public event Render();

        public Game()
        {
            currentLevel = new TestLevel();
            render = new Render(1280, 720);
        }
        public void Run()
        {
            render.RenderFrame();
        }

    }
}
