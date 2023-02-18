using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public IsGroundedControl IsGroundedControl { get { return isGroundedControl; } set { isGroundedControl = value; } }
        [SerializeField] private AnimationCurve moveCurve = null;
        [SerializeField] private float jumpForce = 200f;
        [SerializeField] private float maxMoveSpeed = 250f;
        private float moveSpeed = 0f;
        private float moveDirection = 1f;
        private bool jumping = false;
        private Rigidbody rigidBody;
        private IsGroundedControl isGroundedControl = null;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Move(float direction)
        {
            if (isGroundedControl.IsGrounded)
            {
                if(direction < 0)
                {
                    moveDirection = -1f;
                }
                else
                {
                    moveDirection = 1f;
                }
                moveSpeed = moveCurve.Evaluate(Mathf.Abs(direction)) * moveDirection;   
            }
        }

        public void Jump()
        {
            if(isGroundedControl.IsGrounded)
            {
                jumping = true;
                
            }
        }

        private void FixedUpdate()
        {
            if(moveSpeed > 0.01f || moveSpeed < -0.01f)
            {
                rigidBody.velocity = new Vector3((maxMoveSpeed * moveSpeed * Time.deltaTime), rigidBody.velocity.y, rigidBody.velocity.z);
                moveSpeed = 0f;
            }
            if (jumping)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce * Time.deltaTime, rigidBody.velocity.z);
                jumping = false;
            }
        }
    }
}