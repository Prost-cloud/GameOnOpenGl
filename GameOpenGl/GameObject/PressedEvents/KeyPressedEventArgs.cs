using GameOpenGl.GameObject.PressedEvents;
using GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject
{
    internal class KeyPressedEventArgs : EventArgs
    {
        public Keys KeyCode;
        public PressedEvents.PressedState InputState;

        public KeyPressedEventArgs(Keys keyCode, PressedState inputState)
        {
            KeyCode = keyCode;
            InputState = inputState;
        }
    }
}
