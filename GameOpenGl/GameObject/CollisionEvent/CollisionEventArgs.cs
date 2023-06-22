using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOpenGl.GameObject.CollisionEvent
{
    internal class CollisionEventArgs : EventArgs
    {
        public GameObject[] GameObjects;
        public GameObject CollisionObject;

        public CollisionEventArgs(GameObject collisionObject , GameObject[] objects)
        {
            CollisionObject = collisionObject;
            GameObjects = objects;
        }

        public CollisionEventArgs()
        {
            CollisionObject = null;
            GameObjects = null;
        }

    }
}
