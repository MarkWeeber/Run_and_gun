using System;
using UnityEngine;

namespace Run_n_gun.Space
{
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState State;

        public static float PlayerStartingHealthPoints;

        public static event Action<GameState> OnGameStateChanged;
        public static event Action OnPlayerDeath;

        public static event Action<float> OnPlayerHealthPointsAdded;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateGameState(GameState newState)
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

        public static void CallPlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }

        public static void PlayerHealthPointsAdded(float value)
        {
            OnPlayerHealthPointsAdded?.Invoke(value);
        }
    }
}