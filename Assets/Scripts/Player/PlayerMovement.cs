using UnityEngine;
using System.Collections;

namespace RunAndGun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private IsGroundedControl isGroundedControl = null;
        public IsGroundedControl IsGroundedControl { get { return isGroundedControl; } set { isGroundedControl = value; } }
        [SerializeField] private AnimationCurve moveCurve = null;
        [SerializeField] private float jumpForce = 200f;
        [SerializeField] private float maxMoveSpeed = 250f;
        [SerializeField] private float maxMidAirMoveSpeed = 10f;
        [SerializeField] private float jumpForceWhenStuck = 150f;
        [SerializeField] private float stuckTimeDuration = 2f;
        private bool active = true;
        private float moveSpeedRatio = 0f;
        private float moveDirection = 1f;
        private float stuckTimer = 0f;
        private bool jumping = false;
        private bool stuckJump = false;
        private Rigidbody rigidBody;
        public float HorizontalVelocity { get { return transform.InverseTransformVector(rigidBody.velocity).x; } }
        public float VecrticalVelocity { get { return rigidBody.velocity.y; } }

        private void Awake()
        {
            GameManager.playerMovement = this;
            GameManager.OnGameStateChanged += OnGameStateChanged;
            GameManager.playerTransform = this.transform;
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            isGroundedControl = GameManager.isGroundedControl;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            ManageStuck();
        }

        public void Move(float direction)
        {
            if (isGroundedControl.IsGrounded || true)
            {
                if (direction < -0.01f)
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
            if (isGroundedControl.IsGrounded)
            {
                jumping = true;

            }
        }

        private void FixedUpdate()
        {
            if (active)
            {
                if (moveSpeedRatio > 0.01f || moveSpeedRatio < -0.01f)
                {
                    if (isGroundedControl.IsGrounded)
                    {
                        rigidBody.velocity = new Vector3((maxMoveSpeed * moveSpeedRatio * Time.fixedDeltaTime), rigidBody.velocity.y, rigidBody.velocity.z);
                    }
                    else
                    {
                        rigidBody.velocity = new Vector3(
                            Mathf.Clamp(
                                rigidBody.velocity.x + (maxMidAirMoveSpeed * moveSpeedRatio * Time.fixedDeltaTime),
                                    -(maxMidAirMoveSpeed + maxMoveSpeed) * Time.fixedDeltaTime,
                                    (maxMidAirMoveSpeed + maxMoveSpeed) * Time.fixedDeltaTime),
                                rigidBody.velocity.y,
                                rigidBody.velocity.z);
                    }
                    moveSpeedRatio = 0f;
                }
                if (jumping)
                {
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime, rigidBody.velocity.z);
                    jumping = false;
                }
                if (stuckJump)
                {
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForceWhenStuck * Time.fixedDeltaTime, rigidBody.velocity.z);
                    stuckJump = false;
                }
            }
        }

        private void ManageStuck()
        {
            if (active)
            {
                if (rigidBody.velocity.magnitude < 0.01f && !isGroundedControl.IsGrounded)
                {
                    if (stuckTimer < stuckTimeDuration)
                    {
                        stuckTimer += Time.deltaTime;
                    }
                    else
                    {
                        stuckJump = true;
                        stuckTimer = 0f;
                    }
                }
                else
                {
                    stuckTimer = 0f;
                }
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnMainMenu:

                    break;
                case GameState.InGamePaused:

                    break;
                case GameState.InGameActive:

                    break;
                case GameState.PlayerDead:
                    DisablePlayerMovement();            
                    break;
                case GameState.LevelVictory:

                    break;
                case GameState.LevelGameOver:

                    break;
                default: break;
            }
        }

        private void DisablePlayerMovement()
        {
            active = false;
        }

        private void EnablePlayerMovement()
        {
            active = false;
        }
    }
}