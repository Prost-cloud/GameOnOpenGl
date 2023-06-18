using GameOpenGl.Misc;
using GameOpenGl.Render.Object2D;
using GameOpenGl.Render.TextureLoader;
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
        //public delegate void OnTextureChangeHandler(object sender, TextureChangeEventArgs e); 
        public event EventHandler<TextureChangeEventArgs> OnTextureChange;

        protected Pos _position;
        protected RenderTypeEnum _renderType;
        protected string _textureName;
        protected uint _currentTexture;

        object _objectLock = new object();

        public uint CurrentTexture
        {
            get
            {
                return _currentTexture;
            }
            protected set
            {
                _currentTexture = value;

                OnTextureChange?.Invoke(this, new TextureChangeEventArgs(value));
            }
        }

        public GameObject()
        {
            _position = new Pos(0, 0);
        }

        public GameObject(Pos pos, string textureName, RenderTypeEnum renderType)
        {
            _renderType = renderType;
            _position = pos;
            _textureName = textureName;

            _currentTexture = new TextureLoader().GetOrCreateTexture(textureName);
        }

        public Pos GetPosition() => _position;

        public RenderTypeEnum GetRenderType()
        {
            return _renderType;
        }

        public string GetTextureName() => _textureName;

        public uint GetCurrentTextureId() => _currentTexture;
    }
}
