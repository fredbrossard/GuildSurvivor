using Game.Entity.Generic;
using UnityEngine;

namespace Game.Entity.Player
{
    public class Player : MonoBehaviour, IEntity<PlayerModel>
    {
        public bool IsAlive { get; set ; }
        [field:SerializeField] public PlayerModel Model { get; private set ; }

        public void Enable()
        {

        }

        public void Disable()
        {
            
        }

        public void Initialize()
        {

        }
    }
}
