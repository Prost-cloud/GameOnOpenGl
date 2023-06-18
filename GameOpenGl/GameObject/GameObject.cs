using GameOpenGl.Misc;
using GameOpenGl.Render.Object2D;
using GameOpenGl.Render.TextureLoader;

namespace GameOpenGl.GameObject
{
    internal class GameObject : IGameObject
    {
        public event EventHandler<TextureChangeEventArgs>? OnTextureChange;
        public event EventHandler<PositionChangeEventArgs>? OnPositionChange;
        public event EventHandler<CollisionEventArgs>? OnCollision;

        protected Pos _position;
        protected RenderTypeEnum _renderType;
        protected string _textureName;
        protected uint _currentTexture;

        protected bool _canCollision;

        public bool CanCollision => _canCollision;


        public float Widht { get; private set; }
        public float Height { get; private set; }

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
            Widht = 1f;
            Height = 1f;
            _canCollision = false;
        }

        public GameObject(Pos pos, string textureName, RenderTypeEnum renderType)
        {
            _renderType = renderType;
            _position = pos;
            _textureName = textureName;

            _currentTexture = new TextureLoader().GetOrCreateTexture(textureName);

            Widht = 1f;
            Height = 1f;
        }

        public Pos Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPositionChange?.Invoke(this, new PositionChangeEventArgs(value));
            }

        }

        public RenderTypeEnum GetRenderType()
        {
            return _renderType;
        }

        public string GetTextureName() => _textureName;

        public uint GetCurrentTextureId() => _currentTexture;

        public Pos GetPosition()
        {
            return _position;
        }

        public void InvokeCollision()
        {
            OnCollision?.Invoke(this, new CollisionEventArgs());
        }
    }
}
