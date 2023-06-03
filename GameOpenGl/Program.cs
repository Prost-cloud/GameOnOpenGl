using System;
using GameOpenGl.Game;
using GLFW;
using OpenGL;

namespace ProblemsGotKittens
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();

            game.Run();
        }
    }
}
