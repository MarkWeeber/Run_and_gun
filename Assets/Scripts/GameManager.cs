using System;
using UnityEngine;
using UnityEngine.Events;

namespace RunAndGun.Space
{
    public class GameManager : MonoBehaviour
    {
        // singleton
        public static GameManager Instance;
        public GameState State;
        // state to see on inspector
        [SerializeField] private GameState stateExposed;
        [SerializeField] private GameState StartingState;
        [SerializeField] private GamePoints gamePointsExposed;
        // game data
        public float PlayerStartingHealthPoints;
        public GamePoints GamePoints;
        // events
        public UnityEvent<GameState> OnGameStateChanged;
        public UnityEvent OnPlayerWeaponReloadStart;
        public UnityEvent OnPlayerWeaponReloadEnd;

        public UnityEvent<float> OnPlayerHealthPointsAdded = new UnityEvent<float>();
        public UnityEvent OnAmmoUpdated = new UnityEvent();
        public UnityEvent OnPointsUpdated = new UnityEvent();
        public UnityEvent OnEnemiesKilledCountUpdated = new UnityEvent();
        public UnityEvent OnHealthPointsUpdated = new UnityEvent();

        // necessary components for player related scripts
        public PlayerMovement playerMovement;
        public IsGroundedControl isGroundedControl;
        public Transform aimTarget;
        public RecoilControl recoilControl;
        public Weapon weapon;

        // necessary components for other classes
        public Transform playerTransform;

        // reference to selected enemy
        public EnemyHealthBar_UI EnemyHealthBar_UI;

        private void Awake()
        {
            Instance = this;
            stateExposed = StartingState;
            GamePoints = new GamePoints();
        }

        private void Start()
        {
            EnemyHealthBar_UI = GameObject.FindObjectOfType<EnemyHealthBar_UI>();
            UpdateGameState(StartingState);
        }

        public static void UpdateGameState(GameState newState)
        {
            Instance.State = newState;
            switch (Instance.State)
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
            Instance.OnGameStateChanged?.Invoke(newState);
        }

        public static void PlayerHealthPointsAdded(float value)
        {
            Instance.OnPlayerHealthPointsAdded?.Invoke(value);
            Instance.GamePoints.CurrentHealth += value;
            UpdateHealthPoints();
        }

        public static void PointsAdded(int value)
        {
            Instance.GamePoints.Points += value;
            UpdatePoints();
        }


        private void Update()
        {
            stateExposed = State;
            gamePointsExposed = GamePoints;
        }

        public static void ReloadWeaponStart()
        {
            Instance.OnPlayerWeaponReloadStart?.Invoke();
        }

        public static void ReloadWeaponEnd()
        {
            Instance.OnPlayerWeaponReloadEnd?.Invoke();
        }

        public static void UpdateAmmo()
        {
            Instance.OnAmmoUpdated?.Invoke();
        }

        public static void UpdatePoints()
        {
            Instance.OnPointsUpdated?.Invoke();
        }
        public static void UpdateKilledCount()
        {
            Instance.OnEnemiesKilledCountUpdated?.Invoke();
        }
        private static void UpdateHealthPoints()
        {
            Instance.OnHealthPointsUpdated?.Invoke();
        }
    }
}