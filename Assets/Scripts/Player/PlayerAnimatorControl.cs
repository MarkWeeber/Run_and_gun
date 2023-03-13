using UnityEngine;

namespace Run_n_gun.Space
{
    public class PlayerAnimatorControl : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement = null;
        [SerializeField] private IsGroundedControl isGroundedControl = null;
        public PlayerMovement PlayerMovement { get { return playerMovement; } set { playerMovement = value; } }
        public IsGroundedControl IsGroundedControl { get { return isGroundedControl; } set { isGroundedControl = value; } }

        private Animator animator = null;

        private void Awake()
        {
            GameManager.OnPlayerDeath += OnPlayerDeath;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerDeath += OnPlayerDeath;
        }

        private void Update()
        {
            animator.SetFloat("MoveSpeed", Mathf.Round(playerMovement.HorizontalVelocity * 100f) / 100f);
            animator.SetFloat("VerticalSpeed", Mathf.Round(playerMovement.VecrticalVelocity * 100f) / 100f);
            animator.SetBool("Grounded", isGroundedControl.IsGrounded);
        }

        private void OnPlayerDeath()
        {
            animator.SetTrigger("Die");
        }
    }
}