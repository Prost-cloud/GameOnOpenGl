using GameOpenGl.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;
using GameOpenGl.Renders;
using GameOpenGl.GameObject;

namespace GameOpenGl.Game
{
    internal sealed class Game
    {
        private ILevel _currentLevel;
        private IRender _render;
        private Player _player;

        char[] _buttonPressed;

        private double _currentTime, _deltaTime, _lastTime;

        public delegate void OnRenderHandler(object sender, OnRenderEventArgs args); 
        public event OnRenderHandler? OnRender;

        public Game()
        {
            Window window = GameRender.PrepareWindow(1280, 720);
            _currentLevel = new TestLevel(this);
            _render = new GameRender(window, _currentLevel.GetGameObjects());

            //_buttonPressed = new char[1024];

            Glfw.SetKeyCallback(window,
                (window, key, scan, action, mods)
                    => HandleKeyPressed(window, key, scan, action, mods));
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

                //_currentLevel.MovePlayer((Player)_currentLevel._player, new Misc.Pos(-0.001f, 0.001f)) ;
            }
        }

        static void HandleKeyPressed(Window window, Keys key, int scancode, InputState action, ModifierKeys mods)
        {

        }
    }
}
