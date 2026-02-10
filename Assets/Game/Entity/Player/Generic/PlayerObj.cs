using Game.Entity.Generic;
using UnityEngine;
using Zenject;

namespace Game.Entity.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerObj : MonoBehaviour, IEntity<PlayerModel>
    {
        public bool IsAlive { get; set ; }
        public GameObject GameObject => gameObject;
        public PlayerMovement PlayerMovement { get; private set; }

        public PlayerModel Model { get; set; }

        //[Inject]
        //public void Construct(PlayerModel _model)
        //{
        //    Model = _model;
        //    Bind();
        //}

        void Awake()
        {
            PlayerMovement = GetComponent<PlayerMovement>();
        }

        void Start()
        {

        }

        public void Bind(PlayerModel _model)
        {
            Model = _model;
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

        public class Factory : PlaceholderFactory<PlayerModel, PlayerObj>
        {

        }
    }
}
