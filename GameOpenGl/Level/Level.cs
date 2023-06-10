using GameOpenGl.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Level
{
    internal class TestLevel : ILevel
    {
        private IGameObject[] _gameObjects;
        public IGameObject[] GetGameObjects() => _gameObjects;

        public TestLevel()
        {
            _gameObjects = new IGameObject[]
            {
                new Wall(new Misc.Pos(-1,0), "\\Textures\\wall.png"),
                new Wall(new Misc.Pos(0,0), "\\Textures\\wall.png"),
                new Wall(new Misc.Pos(1,0), "\\Textures\\wall.png")
            };
        }
    }
}
