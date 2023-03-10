using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Run_n_gun.Space
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Start()
        {

        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }

        public void PauseGameButtonPress()
        {
            GameManager.UpdateGameState(GameState.InGamePaused);
        }

        public void ResumeGameButtonPress()
        {
            GameManager.UpdateGameState(GameState.InGameActive);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnMainMenu:

                    break;
                case GameState.InGamePaused:
                    PauseGame();
                    break;
                case GameState.InGameActive:
                    ResumeGame();
                    break;
                case GameState.PlayerDead:

                    break;
                case GameState.LevelVictory:

                    break;
                case GameState.LevelGameOver:

                    break;
                default: break;
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
        }

        private void ResumeGame()
        {
            if(GameManager.State != GameState.PlayerDead || GameManager.State != GameState.LevelGameOver || GameManager.State != GameState.LevelVictory)
            {
                Time.timeScale = 1f;    
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}