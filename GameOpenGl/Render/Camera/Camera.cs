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
        private float _cameraXOffset;
        private Pos _CameraLeftDownBorder;
        private Pos _CameraRightUpBorder;

        public Camera()
        {
            _cameraXOffset = 0.55f;
            _cameraScale = 0.2f;
            _CameraLeftDownBorder = new Pos(8.5f, 4.50f);
            _CameraRightUpBorder = new Pos(90, 72);
        }

        public Matrix4x4 GetMatrixCameraCenter()
        {
            return Matrix4x4.CreateTranslation(-_cameraXOffset * _cameraPos.X, -_cameraPos.Y, 0f);
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
            Pos newPosition = position;

            if(newPosition.X <= _CameraLeftDownBorder.X || newPosition.X >= _CameraRightUpBorder.X)
            {
                newPosition.X = _CameraLeftDownBorder.X < newPosition.X ? _CameraRightUpBorder.X : _CameraLeftDownBorder.X;
            }

            if(newPosition.Y <= _CameraLeftDownBorder.Y || newPosition.Y >= _CameraRightUpBorder.Y)
            {
                newPosition.Y = _CameraLeftDownBorder.Y < newPosition.Y ? _CameraRightUpBorder.Y : _CameraLeftDownBorder.Y;
            }

            _cameraPos = newPosition;
        }
    }
}
