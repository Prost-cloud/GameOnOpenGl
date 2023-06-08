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
        private float[] vertices = new float[9]
        {
            -1.0f, -1.0f, 0.0f,
            1.0f, -1.0f, 0.0f, 
            0.0f,  1.0f, 0.0f
        };

        public TestTriangle(Pos pos, Color color)
        {
            this.color = color;
            this.pos = pos;
        }

        public TestTriangle(float x, float y) : this(new Pos(x, y), new Color(1, 1, 1, 1)) { }
        public TestTriangle(float x, float y, Color color) : this(new Pos(x, y), color) { }
        public TestTriangle(float x, float y, float r, float g, float b, float a) : this(new Pos(x, y), new Color(r, g, b, a)) { }
    }
}
