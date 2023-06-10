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
    internal sealed class Wall : IGameObject
    {
        private Pos _position;
        private byte[] _texture;
        private uint _textureId;

        //private int _textureWidth;
        //private int _textureHeight;

        public Wall(Pos pos, string texturePath)
        {
            _position = pos;

            byte[] textureByteArray;

            int width, height;
            BitmapData data;
            Bitmap image = new Bitmap(Environment.CurrentDirectory + texturePath);


            width = image.Width;
            height = image.Height;

            // using (MemoryStream memoryStream = new MemoryStream())
            // {
            data = image.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            //image.Save(memoryStream, image.RawFormat);

            //textureByteArray = memoryStream.ToArray();
            //}

            _textureId = GL.glGenTexture();

            GL.glBindTexture(GL.GL_TEXTURE_2D, _textureId);

            GL.glTexParameterf(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
            GL.glTexParameterf(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);

            //var t = textureByteArray.AsSpan();
            image.UnlockBits(data);

            // unsafe
            // {
            //     fixed (byte* textureArray = &t[0])
            //     {
            GL.glTexImage2D(
                GL.GL_TEXTURE_2D,
                0,
                GL.GL_RGBA,
                width,
                height,
                0,
                GL.GL_BGRA,
                GL.GL_BYTE,
                data.Scan0);
            //     }
            // }


            GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);
        }

        public Pos GetPosition()
        {
            return _position;
        }

        public uint GetTextureId()
        {
            return _textureId;
        }
    }
}
