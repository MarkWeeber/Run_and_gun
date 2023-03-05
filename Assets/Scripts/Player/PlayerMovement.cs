using UnityEngine;
using System.Collections;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public IsGroundedControl IsGroundedControl { get { return isGroundedControl; } set { isGroundedControl = value; } }
        [SerializeField] private AnimationCurve moveCurve = null;
        [SerializeField] private float jumpForce = 200f;
        [SerializeField] private float maxMoveSpeed = 250f;
        private float moveSpeedRatio = 0f;
        private float moveDirection = 1f;
        private bool jumping = false;
        private Rigidbody rigidBody;
        private IsGroundedControl isGroundedControl = null;
        public float HorizontalVelocity { get { return transform.InverseTransformVector(rigidBody.velocity).x; } }
        public float VecrticalVelocity { get { return rigidBody.velocity.y; } }
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void Move(float direction)
        {
            if (isGroundedControl.IsGrounded)
            {
                if(direction < -0.01f )
                {
                    moveDirection = -1f;
                }
                else if (direction > 0.01f)
                {
                    moveDirection = 1f;
                }
                else
                {
                    moveDirection = 0f;
                }
                moveSpeedRatio = moveCurve.Evaluate(Mathf.Abs(direction)) * moveDirection;   
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
            if(moveSpeedRatio > 0.01f || moveSpeedRatio < -0.01f)
            {
                rigidBody.velocity = new Vector3((maxMoveSpeed * moveSpeedRatio * Time.fixedDeltaTime), rigidBody.velocity.y, rigidBody.velocity.z);
                moveSpeedRatio = 0f;
            }
            if (jumping)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime, rigidBody.velocity.z);
                jumping = false;
            }
        }
    }
}