using GameOpenGl.GameObject;
using GameOpenGl.GameObject.CollisionEvent;
using GameOpenGl.Misc;

namespace GameOpenGl.Level
{
    internal class TestLevel : ILevel
    {
        private readonly List<IGameObject> _gameObjects;
        private IGameObject _player;
        public IGameObject[] GetGameObjects() => _gameObjects.ToArray();

        public delegate void OnCollisionHandler(object sender, CollisionEventArgs args);
        public event OnCollisionHandler? OnCollision;


        public TestLevel(Game.Game onRender)
        {
            _gameObjects = new List<IGameObject>();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0 || i == 9 || j == 0 || j == 7)
                    {
                        _gameObjects.Add(new Wall(new Misc.Pos(i, j), "wall.png"));
                    }
                    else
                    {
                        _gameObjects.Add(new BackgroundWall(new Misc.Pos(i, j), "BGWall.jpg"));
                    }
                }

            //_gameObjects.Clear();
            _gameObjects.Add(new Wall(new Misc.Pos(-1, 5), "wall.png"));
            _player = new Player(new Misc.Pos(1.0f, 2.3f), "Player.png", onRender);
            _gameObjects.Add(_player);
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(2, 2), "1_2789.png"));
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(1, 1), "1_2789.png"));
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(0, 0), "1_2789.png"));
            onRender.OnRender += HandleRenderTick;
            OnCollision += ((Player)_player).OnCollisionHandle;
        }

        private void HandleRenderTick(object sender, Game.OnRenderEventArgs args)
        {
            var result = CheckCollisionAndReturnObject((Player)_player);
            if(result.Count() != 0)
            {

                OnCollision?.Invoke(this, new CollisionEventArgs((GameObject.GameObject)_player, result));
            }
        }

        public GameObject.GameObject[] CheckCollisionAndReturnObject(GameObject.GameObject GameObject)
        {
            Pos objPos = GameObject.GetPosition();

            var result = new List<GameObject.GameObject>();

            foreach (var obj1 in _gameObjects)
            {
                var obj = obj1 as GameObject.GameObject;
                var otherPos = obj.GetPosition();

                if (obj is null)
                {
                    continue;
                }

                if ((objPos.X - GameObject.Width / 2) < (otherPos.X + obj.Width / 2) &&
                     (objPos.X + GameObject.Width / 2) > (otherPos.X - obj.Width / 2) &&
                     (objPos.Y - GameObject.Height / 2) < (otherPos.Y + obj.Height / 2) &&
                     (objPos.Y + GameObject.Height / 2) > (otherPos.Y - obj.Height / 2))
                {
                    obj.InvokeCollision();
                    if (obj.CanCollision)
                    {
                        result.Add(obj);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
