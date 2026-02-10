using Game.Entity.Generic;
using Game.Entity.Model;
using Managers.Spawner;
using System;
using UnityEngine;

namespace Game.Entity.Enemy
{
    public class EnemyObj : MonoBehaviour, IEntity<EnemyModel>, ISpawnableObj
    {
        public SpriteRenderer spriteRenderer;
        public bool IsAlive { get; set; }

        public EnemyModel Model { get; set; }

        public GameObject GameObject => gameObject;

        public Action<GameObject> OnReleaseObj { get; set ; }

        public void Alive()
        {
            IsAlive = true;
        }

        public void Die()
        {
            IsAlive = false;
            OnReleaseObj?.Invoke(gameObject);
        }

        public void Bind(EnemyModel _model)
        {
            spriteRenderer.sprite = Model.sprite;
        }
    }
}