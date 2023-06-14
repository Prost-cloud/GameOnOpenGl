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
        private ILevel _currentLevel;
        private IRender _render;

        //public event Render();

        public Game()
        {
            _render = new Render(1280, 720);
            _currentLevel = new TestLevel();
        }
        public void Run()
        {
            while (!_render.IsExit())
            {
                _render.RenderFrame(_currentLevel.GetGameObjects());
            }
        }

    }
}
