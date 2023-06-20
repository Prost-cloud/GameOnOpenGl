using GameOpenGl.Game;
using GameOpenGl.Misc;
using GameOpenGl.Render.TextureLoader;
using GLFW;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal sealed class Player : GameObject, IGameObject
    {
        private uint[] _textures;
        private float _nextTexture;
        private int _currentTextureIndex;
        private bool _onFoot;
        //private uint _currentTexture;
        private bool[] _keyPressedStates;

        private Pos _speed;
        private Pos _acceleration;

        private Pos Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                const float maxSpeed = 3f;

                if (value.X >= maxSpeed) value.X = maxSpeed;
                if (value.X <= -maxSpeed) value.X = -maxSpeed;
                if (value.Y >= maxSpeed) value.Y = maxSpeed;
                if (value.Y <= -maxSpeed) value.Y = -maxSpeed;
                
                _speed = value;
            }
        }

        private Pos Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                const float MaxAcceleration = 1f;

                if (value.X >= MaxAcceleration) value.X = MaxAcceleration;
                if (value.X <= -MaxAcceleration) value.X = -MaxAcceleration;
                if (value.Y >= 0) value.Y = 0;
                if (value.Y <= -MaxAcceleration) value.Y = -MaxAcceleration;

                _acceleration = value;
            }
        }

        public Player(Pos pos, string texture, Game.Game onRender)
        {
            onRender.OnRender += HandleRenderEvent;

            _onFoot = true;

            _keyPressedStates = new bool[350];

            _position = pos;
            _nextTexture = 0.4f;
            _currentTextureIndex = 0;

            TextureLoadParameter Parms = new(0, 0, 0, 0, 91, 130, 4, 0);

            _textures = new AnimateTextureLoader().GetOrCreateAnimatedTexture(texture, Parms);

            onRender.OnPressedKey += HandleKeyPressed;
        }


        private void HandleRenderEvent(object sender, OnRenderEventArgs e)
        {
            UpdateTextureTick(e.DeltaTime);
            UpdateMoveTick(e.DeltaTime);
        }

        private void UpdateMoveTick(float deltaTime)
        {
            Pos newAcceleration = new Pos();

            newAcceleration.X = _speed.X > 0 ? -1.5f : 1.5f;
            newAcceleration.X = _speed.X < 0.0000001f &&  _speed.X > -0.0000001f ? 0f : newAcceleration.X;
            newAcceleration.Y = 0f;

            if (_keyPressedStates[(int)Keys.D])
            {
                newAcceleration = new Pos(3f, 0f);
            }

            if (_keyPressedStates[(int)Keys.A])
            {
                newAcceleration = new Pos(-3f, 0f);
            }

            if (_keyPressedStates[(int)Keys.W] && _onFoot)
            {
                Speed = new Pos(0f, 3f);
                _keyPressedStates[(int)Keys.W] = false;
                _onFoot = false;
            }

            if (_keyPressedStates[(int)Keys.S])
            {
                newAcceleration = new Pos(0f, -1f);
            }
            else
            {
                newAcceleration.Y = _onFoot ? -0.25f : 0f;
            }

            Acceleration = newAcceleration;

            Speed += Acceleration * deltaTime;
            Position += Speed * deltaTime;


        }

        private void UpdateTextureTick(float deltaTime)
        {
            if (_nextTexture > 0)
            {
                _nextTexture -= deltaTime;
            }
            else
            {
                _nextTexture = 0.60f;
                if (_currentTextureIndex == _textures.Count() - 1)
                {
                    _currentTextureIndex = 0;
                }
                else
                {
                    _currentTextureIndex++;
                }

                CurrentTexture = _textures[_currentTextureIndex];
            }
        }

        private void HandleKeyPressed(object sender, KeyPressedEventArgs e)
        {
            _keyPressedStates[(int)e.KeyCode] =
                e.InputState == PressedEvents.PressedState.Pressed;

            Console.WriteLine($"Handle Key Press Player {e.KeyCode} {e.InputState}");
        }
    }
}
