using GameOpenGl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal interface IGameObject
    {
        Pos GetPosition();
        uint GetTextureId();
    }
}
