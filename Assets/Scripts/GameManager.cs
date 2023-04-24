using System;
using UnityEngine;
using UnityEngine.Events;

namespace RunAndGun.Space
{
    public class GameManager : MonoBehaviour
    {
        // singleton
        public static GameManager Instance;
        public static GameState State;
        // state to see on inspector
        [SerializeField] private GameState state;
        // game data
        public static float PlayerStartingHealthPoints;
        public static GamePoints GamePoints;
        // events
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<float> OnPlayerHealthPointsAdded;
        public static event Action OnPlayerWeaponReloadStart;
        public static event Action OnPlayerWeaponReloadEnd;
        public static UnityEvent OnAmmoUpdated = new UnityEvent();
        public static UnityEvent OnPointsUpdated = new UnityEvent();
        public static UnityEvent OnEnemiesKilledCountUpdated = new UnityEvent();
        public static UnityEvent OnHealthPointsUpdated = new UnityEvent();

        // necessary components for player related scripts
        public static PlayerMovement playerMovement;
        public static IsGroundedControl isGroundedControl;
        public static Transform aimTarget;
        public static RecoilControl recoilControl;
        public static Weapon weapon;

        // necessary components for other classes
        public static Transform playerTransform;

        // reference to selected enemy
        public EnemyHealthBar_UI EnemyHealthBar_UI;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            EnemyHealthBar_UI = GameObject.FindObjectOfType<EnemyHealthBar_UI>();
        }

        public static void UpdateGameState(GameState newState)
        {
            State = newState;
            switch (State)
            {
                case GameState.OnMainMenu:

                    break;
                case GameState.InGamePaused:

                    break;
                case GameState.InGameActive:

                    break;
                case GameState.PlayerDead:

                    break;
                case GameState.LevelVictory:

                    break;
                case GameState.LevelGameOver:

                    break;
                default: break;
            }
            OnGameStateChanged?.Invoke(newState);
        }

        public static void PlayerHealthPointsAdded(float value)
        {
            OnPlayerHealthPointsAdded?.Invoke(value);
            GamePoints.CurrentHealth += value;
            UpdateHealthPoints();
        }


        private void Update()
        {
            state = State;
        }

        public static void ReloadWeaponStart()
        {
            OnPlayerWeaponReloadStart?.Invoke();
        }

        public static void ReloadWeaponEnd()
        {
            OnPlayerWeaponReloadEnd?.Invoke();
        }

        public static void UpdateAmmo()
        {
            OnAmmoUpdated?.Invoke();
        }

        public static void UpdatePoints()
        {
            OnPointsUpdated?.Invoke();
        }
        public static void UpdateKilledCount()
        {
            OnEnemiesKilledCountUpdated?.Invoke();
        }
        private static void UpdateHealthPoints()
        {
            OnHealthPointsUpdated?.Invoke();
        }
    }
}