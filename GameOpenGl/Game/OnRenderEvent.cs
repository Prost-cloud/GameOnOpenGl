using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Game
{
    internal class OnRenderEventArgs : EventArgs
    {
        public float DeltaTime { get; set; }
        public OnRenderEventArgs(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
