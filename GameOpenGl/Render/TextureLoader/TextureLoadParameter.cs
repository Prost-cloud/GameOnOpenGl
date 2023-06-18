using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Render.TextureLoader
{
    internal class TextureLoadParameter
    {
        public int RowCount, ColumnCount;
        public int StartPositionX, StartPositionY;
        public int Width, Height;

        public int PixelsBetweenSpritesX, PixelsBetweenSpritesY;

        public TextureLoadParameter(string settingPath)
        {

        }

        public TextureLoadParameter()
        {

        }

        public TextureLoadParameter(int rowCount, int columnCount, int startPositionX, int starePositionY, int width, int height, int pixelsBetweenSpritesX, int pixelsBetweenSpritesY)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            StartPositionX = startPositionX;
            StartPositionY = starePositionY;
            Width = width;
            Height = height;
            PixelsBetweenSpritesX = pixelsBetweenSpritesX;
            PixelsBetweenSpritesY = pixelsBetweenSpritesY;
        }

    }
}
