using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Misc
{
    internal struct Pos
    {
        float x, y;
        public float X => x;
        public float Y => y;

        public Pos(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
