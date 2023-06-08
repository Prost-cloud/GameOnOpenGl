using GameOpenGl.Misc;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal sealed class Wall : IGameObject
    {
        private Pos _position;
        private byte[] _texture;
        private uint _textureId;

        //private int _textureWidth;
        //private int _textureHeight;

        public Wall(Pos pos, string texturePath, int width, int height)
        {
            _position = pos;

            byte[] textureByteArray = new byte[width * height];

            Bitmap image = new Bitmap(texturePath);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);

                textureByteArray = memoryStream.ToArray();
            }

            _textureId = GL.glGenTexture();
            GL.glBindTexture(GL.GL_TEXTURE_2D, _textureId);
            unsafe
            {
                fixed (byte* textureArray = &textureByteArray[0])
                {
                    GL.glTexImage2D(
                        GL.GL_TEXTURE_2D,
                        0,
                        GL.GL_RGBA,
                        image.Width,
                        image.Height,
                        0,
                        GL.GL_RGBA,
                        GL.GL_UNSIGNED_BYTE,
                        textureArray);
                }
            }

            GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);
        }
    }
}
