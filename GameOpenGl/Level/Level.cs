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
        private List<IGameObject> _gameObjects;
        public IGameObject[] GetGameObjects() => _gameObjects.ToArray();

        public TestLevel()
        {

            int x = 10, y = 8;

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

            _gameObjects.Add(new Player(new Misc.Pos(4, 5), "Player.png"));
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(2, 2), "1_2789.png"));
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(1, 1), "1_2789.png"));
            //_gameObjects.Add(new BackgroundWall(new Misc.Pos(0, 0), "1_2789.png"));


        }
    }
}
