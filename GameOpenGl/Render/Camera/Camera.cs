using GameOpenGl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Render.Camera
{
    internal class Camera
    {
        private float _cameraScale;
        private Pos _cameraPos;
        private float _cameraOffset;

        public Camera()
        {
            _cameraOffset = 0.55f;
            _cameraScale = 0.2f;
        }

        public Matrix4x4 GetMatrixCameraCenter()
        {
            return Matrix4x4.CreateTranslation(-_cameraOffset * _cameraPos.X, -_cameraOffset * _cameraPos.Y, 0f);
        }

        public Matrix4x4 GetMatrixCameraScale()
        {
            return Matrix4x4.CreateScale(_cameraScale);
        } 

        public void SetCameraScale(float scale)
        {
            _cameraScale = scale;
        }

        public void SetCameraPos(Pos position)
        {
            _cameraPos = position;
        }
    }
}
