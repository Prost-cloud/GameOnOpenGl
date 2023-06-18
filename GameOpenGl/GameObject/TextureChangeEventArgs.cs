using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal class TextureChangeEventArgs : EventArgs
    {
        public uint TextureId;

        public TextureChangeEventArgs(uint textureId)
        {
            TextureId = textureId;
        }
    }
}
