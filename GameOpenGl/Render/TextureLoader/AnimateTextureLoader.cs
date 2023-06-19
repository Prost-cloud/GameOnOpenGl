using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Render.TextureLoader
{
    internal class AnimateTextureLoader : TextureLoader
    {
        public uint[] GetOrCreateAnimatedTexture(string name, TextureLoadParameter parms)
        {
            //return 0;

            List<uint> _textures = new();

            BitmapData data;
            Bitmap imageSource = new Bitmap(Environment.CurrentDirectory + "\\Textures\\" + name);

            //int oneWidth = 91;
            //int oneHeight = 130;

            //int xPixel = 0;
            //int yPixel = 0;

            //int pixelsBetweenSprite = 4;

            data = imageSource.LockBits(new Rectangle(0, 0, imageSource.Width, imageSource.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            for (int i = 0; i < 3; i++)
            {
                if (!_textureMap.ContainsKey(name + i))
                {

                    Bitmap temp = new Bitmap(parms.Width, parms.Height);

                    var dataSprite = temp.LockBits(new Rectangle(0, 0, temp.Width, temp.Height),
                           ImageLockMode.ReadWrite,
                           PixelFormat.Format32bppArgb);

                    for (int j = 0; j < parms.Height; j++)
                    {
                        int[] rowData = new int[parms.Width];

                        IntPtr src = data.Scan0 + ((j + 0) * data.Stride) + (parms.StartPositionX * 4) + parms.PixelsBetweenSpritesX * i;
                        IntPtr dst = dataSprite.Scan0 + (j * dataSprite.Stride);

                        Marshal.Copy(src, rowData, 0, parms.Width);
                        Marshal.Copy(rowData, 0, dst, parms.Width);
                    }

                    uint textureId = GL.glGenTexture();

                    GL.glBindTexture(GL.GL_TEXTURE_2D, textureId);

                    GL.glTexImage2D(
                        GL.GL_TEXTURE_2D,
                        0,
                        GL.GL_RGBA,
                        parms.Width,
                        parms.Height,
                        0,
                        GL.GL_BGRA,
                        GL.GL_UNSIGNED_BYTE,
                        dataSprite.Scan0);

                    //var j = GL.GetError();
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
                    GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);

                    GL.glEnable(GL.GL_BLEND);
                    //GL.glDisable(GL.GL_BLEND);
                    GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

                    GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
                    GL.glBindTexture(GL.GL_TEXTURE_2D, 0);


                    _textures.Add(textureId);
                    _textureMap.Add(name + i, textureId);

                    parms.StartPositionX += parms.Width;
                }
                else
                {
                    _textures.Add(GetValueByName(name + i));
                }
            }
            imageSource.UnlockBits(data);
            return _textures.ToArray();
        }

    }
}
