using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOpenGl.GameObject;
using GLFW;
using OpenGL;

namespace GameOpenGl.Renders
{
    internal interface IRender
    {
        void RenderFrame(IGameObject[] gameObjects);
        bool IsExit();
    }
}
