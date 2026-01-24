using UnityEngine;

namespace Game.Entity.Generic
{
    public interface IEntity<T> where T : IEntityModel
    {
        public GameObject GameObject { get;}
        public bool IsAlive { get; }
        public T Model { get; }

        public void Initialize();
        public void Alive();
        public void Die();
    }
}