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
        private IGameObject[] gameObjects;

        public TestLevel()
        {
            gameObjects = new IGameObject[]
            {
                new TestTriangle(1,1),
                new TestTriangle(2,1),
                new TestTriangle(3,1)
            };
        }
    }
}
