using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Misc
{
    internal struct Pos
    {
        public float X;
        public float Y;
        // public float X => x;
        // public float Y => y;

        public Pos(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public static Pos operator +(Pos left, Pos right)
        {
            return new Pos(
            left.X + right.X,
            left.Y + right.Y);
        }

        public static Pos operator -(Pos left, Pos right)
        {
            return new Pos(
            left.X - right.X,
            left.Y - right.Y);
        }
    }
}
