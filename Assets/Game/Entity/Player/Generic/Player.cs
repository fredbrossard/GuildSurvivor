using Game.Entity.Generic;
using UnityEngine;

namespace Game.Entity.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, IEntity<PlayerModel>
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
            Initialize();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(true);
        }

        public void Initialize()
        {
            PlayerMovement.SetSpeed(Model.initialSpeed);

            Enable();
        }
    }
}
