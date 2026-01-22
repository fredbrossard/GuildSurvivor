using Game.Entity.Generic;
using UnityEngine;

namespace Game.Entity.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IMovable
    {
        public Rigidbody2D Rigidbody { get; set; }
        private float _speed;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void Move(Vector2 position)
        {
            transform.position += new Vector3(position.x, position.y, 0f) * _speed * Time.deltaTime;
            Debug.Log(transform.position);
        }

        public void FixedUpdate()
        {
           Rigidbody.MovePosition(transform.position);
        }
    }
}
