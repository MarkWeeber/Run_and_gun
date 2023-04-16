using System;
using UnityEngine;

namespace RunAndGun.Space
{
    public class GameManager : MonoBehaviour
    {
        // singleton
        public static GameManager Instance;
        public static GameState State;
        // state to see on inspector
        [SerializeField] private GameState state;
        public static float PlayerStartingHealthPoints;
        // events
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<float> OnPlayerHealthPointsAdded;
        public static event Action<float> OnPlayerHealthPointsSubtracted;
        public static event Action OnPlayerWeaponReloadStart;
        public static event Action OnPlayerWeaponReloadEnd;
        public static event Action<float, float> OnAmmoChanged;

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
        }

        public static void PlayerHealthPointsSubtracted(float value)
        {
            OnPlayerHealthPointsSubtracted?.Invoke(value);
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

        public static void RefreshAmmoCount(float currentCount, float maxCount)
        {
            OnAmmoChanged?.Invoke(currentCount, maxCount);
        }
    }
}