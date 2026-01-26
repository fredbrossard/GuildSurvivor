using Game.Entity.Generic;
using Game.Entity.Model;
using Managers.Spawner;
using UnityEngine;

namespace Game.Entity.Enemy
{
    public class EnemyObj : MonoBehaviour, IEntity<EnemyModel>, ISpawnableObj
    {
        public SpriteRenderer spriteRenderer;
        public bool IsAlive { get; set; }

        [field:SerializeField] public EnemyModel Model { get; private set; }

        public GameObject GameObject => gameObject;

        public void Alive()
        {
            IsAlive = true;
        }

        public void Die()
        {
            IsAlive = false;
        }

        public void Bind()
        {
            spriteRenderer.sprite = Model.sprite;
        }
    }
}