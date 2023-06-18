using GameOpenGl.GameObject;
using GameOpenGl.Render.TextureLoader;
using GameOpenGl.Misc;
using GameOpenGl.RenderProgram;
using GameOpenGl.ShaderProgram;
using GameOpenGl.VAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Render.Object2D
{
    internal class Object2D
    {
        public uint VAO { get; private set; }
        public uint ShaderProgram{ get; private set; }
        public byte IsNeedRender;

        IGameObject _gameObject;
        protected uint _textureId;

        public Object2D(IGameObject gameObject)
        {
            IsNeedRender = 2;
            _gameObject = gameObject;

            if (gameObject.GetRenderType() == RenderTypeEnum.SquareRender)
            {
                VAO = new BaseVAO().GetOrCreateVaoId();
                ShaderProgram = new BaseShader().GetOrCreateShaderId();
            }
            gameObject.OnTextureChange += HandleTextureChange;
            _textureId = gameObject.GetCurrentTextureId();
            //this._textureId = new TextureLoader.TextureLoader().GetOrCreateTexture(gameObject.GetTextureName());
        }

        public Pos GetPosition()
        {
            return _gameObject.GetPosition();
        }

        public uint GetTextureId() => _textureId;

        protected void HandleTextureChange(object sender, TextureChangeEventArgs e)
        {
            IsNeedRender = 2;
            _textureId = e.TextureId;
        }
    }
}
