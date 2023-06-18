using GameOpenGl.GameObject;
using GameOpenGl.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.Level
{
    internal interface ILevel
    {
        IGameObject[] GetGameObjects();
        void MovePlayer(Player Player, Pos pos);
        //void CheckCollision(IGameObject GameObject);

    }
}
