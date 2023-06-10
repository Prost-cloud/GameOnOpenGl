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
            render = new Render(1280, 720);
            currentLevel = new TestLevel();
        }
        public void Run()
        {
            while (!render.IsExit())
            {
                render.RenderFrame(currentLevel.GetGameObjects());
            }
        }

    }
}
