using GameOpenGl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal class TestTriangle : IGameObject
    {
        private Pos pos;
        private Color color;

        public TestTriangle(Pos pos, Color color)
        {
            this.color = color;
            this.pos = pos;
        }

        public TestTriangle(int x, int y) : this(new Pos(x, y), new Color(1,1,1,1)) { }
    }
}
