using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Render.TextureLoader
{
    internal class TextureLoader
    {
        static protected Dictionary<string, uint> _textureMap;

        static TextureLoader()
        {
            _textureMap = new();
        }

        public virtual uint GetOrCreateTexture(string name)
        {
            if (_textureMap.TryGetValue(name, out uint id))
            {
                return id;
            }
            byte[] textureByteArray;

            int width, height;
            BitmapData data;
            Bitmap image = new Bitmap(Environment.CurrentDirectory + "\\Textures\\" + name);


            width = image.Width;
            height = image.Height;

            data = image.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            var textureId = GL.glGenTexture();

            GL.glBindTexture(GL.GL_TEXTURE_2D, textureId);

            GL.glTexImage2D(
                GL.GL_TEXTURE_2D,
                0,
                GL.GL_RGBA,
                width,
                height,
                0,
                GL.GL_BGRA,
                GL.GL_UNSIGNED_BYTE,
                data.Scan0);

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);

            GL.glEnable(GL.GL_BLEND);
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

            GL.glGenerateMipmap(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, 0);

            TextureLoader.RegisterTexture(name, textureId);

            return textureId;

        }

        static protected void RegisterTexture(string name, uint value)
        {
            _textureMap.Add(name, value);
        }

        static protected uint GetValueByName(string name)
        {
            return _textureMap[name];
        }

        //public virtual uint GetTextureId() => TextureLoader.GetValueByName(_textureName);
    }
}
