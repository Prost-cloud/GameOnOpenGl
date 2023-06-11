using GameOpenGl.Misc;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal sealed class Player : GameObject, IGameObject
    {
        private List<uint> _textures;
        private int _nextTexture;
        private int _currentTexture;
        public Player(Pos pos, string texture)
        {
            _position = pos;
            _nextTexture = 60;
            _currentTexture = 0;

            _textures = new();

            BitmapData data;
            Bitmap image = new Bitmap(Environment.CurrentDirectory + texture);

            int oneWidth = 115;
            int oneHeight = 130;

            int xPixel = 0;
            int yPixel = 0;

            for (int i = 0; i < 3; i++)
            {
                data = image.LockBits(new Rectangle(xPixel, yPixel, oneWidth, oneHeight),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                _textureId = GL.glGenTexture();
                _textures.Add(_textureId);

                GL.glBindTexture(GL.GL_TEXTURE_2D, _textureId);

                GL.glTexImage2D(
                    GL.GL_TEXTURE_2D,
                    0,
                    GL.GL_RGBA,
                    oneWidth,
                    oneHeight,
                    0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);

                GL.glEnable(GL.GL_BLEND);
                GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

                GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
                GL.glBindTexture(GL.GL_TEXTURE_2D, 0);

                image.UnlockBits(data);

                xPixel += oneWidth;
            }
        }

        public override uint GetTextureId()
        {
            if (_nextTexture > 0)
            {
                _nextTexture--;
                Console.WriteLine($"returned {_textures[_currentTexture]} ID {_currentTexture}");
                return _textures[_currentTexture];
            }
            else
            {
                _nextTexture = 60;
                if (_currentTexture >= _textures.Count() - 1)
                {
                    _currentTexture = 0;
                }
                else
                {
                    _currentTexture++;
                }

                Console.WriteLine($"returned {_textures[_currentTexture]} ID {_currentTexture}");

                return _textures[_currentTexture];
            }
        }
    }
}
