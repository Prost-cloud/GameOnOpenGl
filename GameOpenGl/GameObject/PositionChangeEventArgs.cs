using GameOpenGl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal class PositionChangeEventArgs : EventArgs
    {
        public Pos NewPosition;

        public PositionChangeEventArgs(Pos newPosition)
        {
            NewPosition = newPosition;
        }
    }
}
