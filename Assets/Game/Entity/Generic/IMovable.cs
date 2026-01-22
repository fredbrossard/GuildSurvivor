using UnityEngine;

namespace Game.Entity.Generic
{
    public interface IMovable
    {
        public Rigidbody2D Rigidbody { get; set; }
        public void Move(Vector2 position);
    }
}
