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
    internal sealed class Wall : GameObject, IGameObject
    {

        public Wall(Pos pos, string texturePath) : base(pos, texturePath, Render.Object2D.RenderTypeEnum.SquareRender)
        {

        }
    }
}
