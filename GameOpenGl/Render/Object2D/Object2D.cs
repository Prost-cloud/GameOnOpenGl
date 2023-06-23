using GameOpenGl.GameObject;
using GameOpenGl.GameObject.CollisionEvent;
using GameOpenGl.Misc;
using GameOpenGl.ShaderProgram;
using GameOpenGl.VAO;

namespace GameOpenGl.Render.Object2D
{
    internal class Object2D
    {
        public uint VAO { get; private set; }
        public uint ShaderProgram{ get; private set; }
        public byte IsNeedRender;

        public IGameObject _gameObject;
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
            gameObject.OnPositionChange += HandlePositionChange;
            gameObject.OnCollision += HandleCollision;

            _textureId = gameObject.GetCurrentTextureId();
            //this._textureId = new TextureLoader.TextureLoader() 
            //.GetOrCreateTexture(gameObject.GetTextureName());
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

        protected void HandlePositionChange(object sender, PositionChangeEventArgs e)
        {
            IsNeedRender = 2;
        }

        protected void HandleCollision(object sender, CollisionEventArgs e)
        {
            IsNeedRender = 2;
        }
    }
}
