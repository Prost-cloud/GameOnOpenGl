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
            _gameObjects = new IGameObject[] {
                new Wall(new Misc.Pos(-1f, 0f), "\\Textures\\wall.png"),
                new Wall(new Misc.Pos(1f, 0f), "\\Textures\\Blue.png"),
                new Wall(new Misc.Pos(0f, 1f), "\\Textures\\Blue.png"),
                new Wall(new Misc.Pos(-1f, 1f), "\\Textures\\wall.png"),
                new Wall(new Misc.Pos(-1f, 1f), "\\Textures\\1_2789.png"),
                new Wall(new Misc.Pos(-1f, 0f), "\\Textures\\1_2789.png")
            };
        }
    }
}
