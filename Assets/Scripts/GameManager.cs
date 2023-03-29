using System;
using UnityEngine;

namespace Run_n_gun.Space
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
        
        // necessary components for player related scripts
        public static PlayerMovement playerMovement;
        public static IsGroundedControl isGroundedControl;
        public static Transform aimTarget;
        public static RecoilControl recoilControl;

        // reference to selected enemy
        public static Transform enemyHealthBarLocationTransform;

        private void Awake()
        {
            Instance = this;
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

        private void Update ()
        {
            state = State;
        }
    }
}