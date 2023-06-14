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
        [SerializeField] private GameState stateExposed;
        [SerializeField] private GameState StartingState;
        [SerializeField] private GamePoints gamePointsExposed;
        // game data
        public static float PlayerStartingHealthPoints;
        public static GamePoints GamePoints;
        // events
        public static event Action<GameState> OnGameStateChanged;
        public static event Action OnPlayerWeaponReloadStart;
        public static event Action OnPlayerWeaponReloadEnd;

        public static UnityEvent<float> OnPlayerHealthPointsAdded = new UnityEvent<float>();
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
            stateExposed = StartingState;
            UpdateGameState(StartingState);
            GamePoints = new GamePoints();
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
                    Cursor.visible = true;
                    break;
                case GameState.InGamePaused:
                    Cursor.visible = true;
                    break;
                case GameState.InGameActive:
                    Cursor.visible = false;
                    break;
                case GameState.PlayerDead:
                    Cursor.visible = false;
                    break;
                case GameState.LevelVictory:
                    Cursor.visible = false;
                    break;
                case GameState.LevelGameOver:
                    Cursor.visible = false;
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

        public static void PointsAdded(int value)
        {
            GamePoints.Points += value;
            UpdatePoints();
        }


        private void Update()
        {
            stateExposed = State;
            gamePointsExposed = GamePoints;
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