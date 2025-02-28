using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace RunAndGun.Space
{
    public class GameManager : MonoBehaviour
    {
        // singleton
        public static GameManager Instance;
        public GameState State;
        // state to see on inspector
        [SerializeField] private GameState StartingState;
        [SerializeField] private GamePoints gamePointsExposed;
        // game data
        public float PlayerStartingHealthPoints;
        // events
        public UnityEvent<GameState> OnGameStateChanged;
        public UnityEvent OnPlayerWeaponReloadStart;
        public UnityEvent OnPlayerWeaponReloadEnd;

        public UnityEvent<float> OnPlayerHealthPointsAdded = new UnityEvent<float>();
        public UnityEvent OnAmmoUpdated = new UnityEvent();
        public UnityEvent OnPointsUpdated = new UnityEvent();
        public UnityEvent OnEnemiesKilledCountUpdated = new UnityEvent();
        public UnityEvent OnHealthPointsUpdated = new UnityEvent();
        public UnityEvent<string> OnAnnounce = new UnityEvent<string>();
        public UnityEvent OnAllEnemiesKilled = new UnityEvent();

        // necessary components for player related scripts
        public PlayerMovement playerMovement;
        public IsGroundedControl isGroundedControl;
        public Transform aimTarget;
        public RecoilControl recoilControl;
        public Weapon weapon;

        // necessary components for other classes
        public Transform playerTransform;
        private List<Transform> enemies = new List<Transform>();

        // reference to selected enemy
        public EnemyHealthBar_UI EnemyHealthBar_UI;

        private void Awake()
        {
            Instance = this;
            GlobalBuffer.Reset();
        }

        private void Start()
        {
            EnemyHealthBar_UI = GameObject.FindObjectOfType<EnemyHealthBar_UI>();
            UpdateGameState(StartingState);
        }

        public void UpdateGameState(GameState newState)
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
                    Cursor.visible = true;
                    GlobalBuffer.CalculateTimeSpent();
                    GlobalBuffer.failed = true;
                    //GoToEndScene();
                    break;
                case GameState.LevelVictory:
                    Cursor.visible = true;
                    GlobalBuffer.CalculateTimeSpent();
                    GoToEndScene();
                    break;
                case GameState.LevelGameOver:
                    Cursor.visible = false;
                    break;
                default: break;
            }
            Instance.OnGameStateChanged?.Invoke(newState);
        }

        public void PlayerHealthPointsAdded(float value)
        {
            Instance.OnPlayerHealthPointsAdded?.Invoke(value);
            GlobalBuffer.gamePoints.CurrentHealth += value;
            Instance.UpdateHealthPoints();
        }

        public void PointsAdded(int value)
        {
            GlobalBuffer.gamePoints.Points += value;
            Instance.UpdatePoints();
        }


        private void Update()
        {
            gamePointsExposed = GlobalBuffer.gamePoints;
        }

        public void ReloadWeaponStart()
        {
            Instance.OnPlayerWeaponReloadStart?.Invoke();
        }

        public void ReloadWeaponEnd()
        {
            Instance.OnPlayerWeaponReloadEnd?.Invoke();
        }

        public void UpdateAmmo()
        {
            Instance.OnAmmoUpdated?.Invoke();
        }

        public void UpdatePoints()
        {
            Instance.OnPointsUpdated?.Invoke();
        }

        public void UpdateKilledCount()
        {
            Instance.OnEnemiesKilledCountUpdated?.Invoke();
        }
        private void UpdateHealthPoints()
        {
            Instance.OnHealthPointsUpdated?.Invoke();
        }

        public void GoToEndScene()
        {
            SceneManager.LoadScene(2);
        }

        public void LevelVictory()
        {
            Instance.UpdateGameState(GameState.LevelVictory);
        }

        public void AnnounceText(string text)
        {
            Instance.OnAnnounce?.Invoke(text);
        }

        public void AddEnemy(Transform enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(Transform enemy)
        {
            enemies.Remove(enemy);
            if (!enemies.Any())
            {
                OnAllEnemiesKilled?.Invoke();
            }
        }
    }
}