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
    internal class GameObject : IGameObject
    {
        static protected Dictionary<string, uint> _textureMap;

        protected Pos _position;
        protected string _textureName;

        static GameObject()
        {
            _textureMap = new();
        }
        public GameObject()
        {
            _position = new Pos(0,0);
        }

        public GameObject(Pos pos, string textureName)
        {
            this._position = pos;
            this._textureName = textureName;

            if (!_textureMap.TryGetValue(textureName, out _))
            {
                byte[] textureByteArray;

                int width, height;
                BitmapData data;
                Bitmap image = new Bitmap(Environment.CurrentDirectory + "\\Textures\\" + textureName);


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

                GameObject.AddTexture(textureName, textureId);
            }
        }

        public Pos GetPosition() => _position;

        public virtual uint GetTextureId() => GameObject.GetValueByName(_textureName);

        static protected void AddTexture(string name, uint value)
        {
            _textureMap.Add(name, value);
        }
        
        static protected uint GetValueByName(string name)
        {
            return _textureMap[name];
        }
    }
}
