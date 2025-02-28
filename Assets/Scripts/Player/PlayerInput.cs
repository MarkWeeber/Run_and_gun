using UnityEngine;
namespace RunAndGun.Space
{
    public class PlayerInput : MonoBehaviour
    {
        public bool Activated { get { return activated; } set { activated = value; } }
        private bool activated = false;
        [SerializeField] private float deadZone = 0.01f;
        private PlayerMovement playerMovement = null;
        private RecoilControl recoilControl = null;
        private Weapon weapon;
        private float horizontalInput = 0f;

        private void Awake()
        {
            GameManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void Start()
        {
            activated = true;
            playerMovement = GameManager.Instance.playerMovement;
            recoilControl = GameManager.Instance.recoilControl;
            weapon = GameManager.Instance.weapon;
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
                if (Input.GetButtonDown(GlobalStringVars.FIRE_1))
                {
                    weapon.TryShoot();
                }
                // tracking reload button pressed
                if (Input.GetKeyDown(GlobalStringVars.WEAPON_RELOAD_KEY))
                {
                    weapon.ReloadWeaponStart();
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

        private void EnableInput()
        {
            activated = true;
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnMainMenu:

                    break;
                case GameState.InGamePaused:
                    DisableInput();
                    break;
                case GameState.InGameActive:
                    EnableInput();
                    break;
                case GameState.PlayerDead:
                    DisableInput();
                    break;
                case GameState.LevelVictory:
                    DisableInput();
                    break;
                case GameState.LevelGameOver:
                    DisableInput();
                    break;
                default: break;
            }
        }
    }
}