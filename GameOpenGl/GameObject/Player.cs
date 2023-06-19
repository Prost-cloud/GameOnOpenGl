using GameOpenGl.Game;
using GameOpenGl.Misc;
using GameOpenGl.Render.TextureLoader;
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
        //private uint _currentTexture;

        public Player(Pos pos, string texture, Game.Game onRender)
        {
            onRender.OnRender += HandleRenderEvent;

            _position = pos;
            _nextTexture = 0.4f;
            _currentTextureIndex = 0;

            TextureLoadParameter Parms = new(0, 0, 0, 0, 91, 130, 4, 0);

            _textures = new AnimateTextureLoader().GetOrCreateAnimatedTexture(texture, Parms);
        }


        private void HandleRenderEvent(object sender, OnRenderEventArgs e)
        {
            if (_nextTexture > 0)
            {
                _nextTexture -= e.DeltaTime;
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

            Position += new Pos(0.0001f, 0.0001f);
        }
    }
}
