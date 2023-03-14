using System;
using UnityEngine;

namespace Run_n_gun.Space
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static GameState State;
        public GameState state;

        public static float PlayerStartingHealthPoints;

        public static event Action<GameState> OnGameStateChanged;

        public static event Action<float> OnPlayerHealthPointsAdded;
        // necessary components for player related scripts
        public static PlayerMovement playerMovement;
        public static IsGroundedControl isGroundedControl;
        public static Transform aimTarget;
        public static RecoilControl recoilControl;

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

        private void Update ()
        {
            state = State;
        }
    }
}