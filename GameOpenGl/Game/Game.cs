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
    internal sealed class Game
    {
        private ILevel _currentLevel;
        private IRender _render;

        private double _currentTime, _deltaTime, _lastTime;

        public delegate void OnRenderHandler(object sender, OnRenderEventArgs args); 
        public event OnRenderHandler? OnRender;

        public Game()
        {
            Window window = GameRender.PrepareWindow(1280, 720);
            _currentLevel = new TestLevel(this);
            _render = new GameRender(window, _currentLevel.GetGameObjects());
        }
        public void Run()
        {
            while (!_render.IsExit())
            {
                _currentTime = Glfw.Time;

                _deltaTime = _currentTime - _lastTime;
                _render.RenderFrame();

                Console.WriteLine($"FPS: {(int)(1 / _deltaTime)}");

                OnRender?.Invoke(this, new OnRenderEventArgs((float)_deltaTime));

                _lastTime = _currentTime;
            }
        }

    }
}
