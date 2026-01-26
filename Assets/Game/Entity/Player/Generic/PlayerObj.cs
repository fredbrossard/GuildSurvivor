using Game.Entity.Generic;
using UnityEngine;

namespace Game.Entity.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerObj : MonoBehaviour, IEntity<PlayerModel>
    {
        public bool IsAlive { get; set ; }
        [field:SerializeField] public PlayerModel Model { get; private set ; }

        public GameObject GameObject => gameObject;
        public PlayerMovement PlayerMovement { get; private set; }

        void Awake()
        {
            PlayerMovement = GetComponent<PlayerMovement>();
        }

        void Start()
        {
            Bind();
        }

        public void Bind()
        {
            PlayerMovement.SetSpeed(Model.initialSpeed);
        }

        public void Alive()
        {
            IsAlive = true;
        }

        public void Die()
        {
            IsAlive = false;
        }
    }
}
