using GameOpenGl.Misc;
using GameOpenGl.Render.Object2D;
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
        RenderTypeEnum GetRenderType();
        string GetTextureName();

        event EventHandler<TextureChangeEventArgs> OnTextureChange;
        uint GetCurrentTextureId();
    }
}
