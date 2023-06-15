using GameOpenGl.Misc;
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
        private List<string> _textures;
        private int _nextTexture;
        private int _currentTexture;
        public Player(Pos pos, string texture)
        {
            _position = pos;
            _nextTexture = 60;
            _currentTexture = 0;

            _textures = new();

            BitmapData data;
            Bitmap imageSource = new Bitmap(Environment.CurrentDirectory + "\\Textures\\" + texture);

            int oneWidth = 91;
            int oneHeight = 130;

            int xPixel = 0;
            int yPixel = 0;

            int pixelsBetweenSprite = 4;

            data = imageSource.LockBits(new Rectangle(0, 0, imageSource.Width, imageSource.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            for (int i = 0; i < 3; i++)
            {
                if (!_textureMap.ContainsKey(texture + i))
                {

                    Bitmap temp = new Bitmap(oneWidth, oneHeight);

                    var dataSprite = temp.LockBits(new Rectangle(0, 0, temp.Width, temp.Height),
                           ImageLockMode.ReadWrite,
                           PixelFormat.Format32bppArgb);

                    for (int j = 0; j < oneHeight; j++)
                    {
                        int[] rowData = new int[oneWidth];

                        IntPtr src = data.Scan0 + ((j + 0) * data.Stride) + (xPixel * 4) + pixelsBetweenSprite * i;
                        IntPtr dst = dataSprite.Scan0 + (j * dataSprite.Stride);

                        Marshal.Copy(src, rowData, 0, oneWidth);
                        Marshal.Copy(rowData, 0, dst, oneWidth);
                    }

                    uint textureId = GL.glGenTexture();

                    GL.glBindTexture(GL.GL_TEXTURE_2D, textureId);

                    GL.glTexImage2D(
                        GL.GL_TEXTURE_2D,
                        0,
                        GL.GL_RGBA,
                        oneWidth,
                        oneHeight,
                        0,
                        GL.GL_BGRA,
                        GL.GL_UNSIGNED_BYTE,
                        dataSprite.Scan0);

                    //var j = GL.GetError();
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);

                    GL.glEnable(GL.GL_BLEND);
                    //GL.glDisable(GL.GL_BLEND);
                    //GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

                    GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
                    GL.glBindTexture(GL.GL_TEXTURE_2D, 0);


                    _textures.Add(texture + i);
                    _textureMap.Add(texture + i, textureId);

                    xPixel += oneWidth;
                }
            }
            imageSource.UnlockBits(data);
        }

        public override uint GetTextureId()
        {
            if (_nextTexture > 0)
            {
                _nextTexture--;
                return GameObject.GetValueByName(_textures[_currentTexture]);
                
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

                return GameObject.GetValueByName(_textures[_currentTexture]);
            }
        }
    }
}
