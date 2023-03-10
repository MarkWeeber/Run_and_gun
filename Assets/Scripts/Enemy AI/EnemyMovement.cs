using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 50f;
        private Rigidbody _rigidbody = null;
        public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }

        private int moveDirection = 0;
        private bool stopCalled = false;

        public void Moving(int movingDirection = 1)
        {
            moveDirection = movingDirection; 
        }

        public void StopMoving()
        {
            moveDirection = 0;
            stopCalled = true;
        }

        private void FixedUpdate()
        {
            if(moveDirection != 0)
            {
                _rigidbody.velocity = new Vector3(moveSpeed * transform.forward.x * moveDirection * Time.fixedDeltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
            }
            else if(stopCalled)
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, _rigidbody.velocity.z);
                stopCalled = false;
            }
        }
    }
}