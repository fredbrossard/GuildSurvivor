using Game.Entity.Generic;
using Game.Entity.Model;
using UnityEngine;

namespace Game.Entity.Enemy
{
    public class Enemy : MonoBehaviour, IEntity<EnemyModel>
    {
        public bool IsAlive { get; set; }

        [field:SerializeField] public EnemyModel Model { get; private set; }

        public GameObject GameObject => gameObject;

        public void Disable()
        {
            
        }

        public void Enable()
        {
            
        }

        public void Initialize()
        {
           
        }
    }
}