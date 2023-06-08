using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;

namespace GameOpenGl.Renders
{
    internal interface IRender
    {
        void RenderFrame();
    }
}
