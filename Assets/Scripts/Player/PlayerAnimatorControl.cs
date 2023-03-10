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
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            playerMovement = GameManager.playerMovement;
            isGroundedControl = GameManager.isGroundedControl;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            animator.SetFloat("MoveSpeed", Mathf.Round(playerMovement.HorizontalVelocity * 100f) / 100f);
            animator.SetFloat("VerticalSpeed", Mathf.Round(playerMovement.VecrticalVelocity * 100f) / 100f);
            animator.SetBool("Grounded", isGroundedControl.IsGrounded);
        }

        private void PlayDeathAnimation()
        {
            animator.SetTrigger("Die");
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
                    PlayDeathAnimation();
                    break;
                case GameState.LevelVictory:

                    break;
                case GameState.LevelGameOver:

                    break;
                default: break;
            }
        }
    }
}