using GameOpenGl.Misc;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal class BackgroundWall : GameObject, IGameObject
    {
        public BackgroundWall(Pos pos, string v) : base(pos, v)
        {

        }
    }
}
