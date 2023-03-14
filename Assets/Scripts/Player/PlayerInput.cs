using UnityEngine;
namespace Run_n_gun.Space
{
    public class PlayerInput : MonoBehaviour
    {
        public bool Activated { get { return activated; } set { activated = value; } }
        private bool activated = false;
        [SerializeField] private float deadZone = 0.01f;
        public PlayerMovement PlayerMovement { get { return playerMovement; } set { playerMovement = value; } }
        private PlayerMovement playerMovement = null;
        public RecoilControl RecoilControl { get { return recoilControl; } set { recoilControl = value; } }
        private RecoilControl recoilControl = null;

        private float horizontalInput = 0f;

        private void Awake()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Start()
        {
            activated = true;
            playerMovement = GameManager.playerMovement;
            recoilControl = GameManager.recoilControl;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            if (activated)
            {
                // tracking horizontal inputs
                horizontalInput = 0f;
                horizontalInput += Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
                if (horizontalInput > deadZone || horizontalInput < -deadZone)
                {
                    playerMovement.Move(horizontalInput);
                }
                // tracking jump button press
                if (Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON))
                {
                    playerMovement.Jump();
                }
                // tracking fire button press
                if(Input.GetButtonDown(GlobalStringVars.FIRE_1))
                {
                    recoilControl.CallRecoil();
                }
            }
            else
            {
                horizontalInput = 0f;
            }
        }

        private void DisableInput()
        {
            activated = false;
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
                    DisableInput();
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