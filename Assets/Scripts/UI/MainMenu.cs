using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunAndGun.Space
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
            }
            Cursor.visible = true;
        }

        public void PauseGameButtonPress()
        {
            GameManager.Instance.UpdateGameState(GameState.InGamePaused);
        }

        public void ResumeGameButtonPress()
        {
            GameManager.Instance.UpdateGameState(GameState.InGameActive);
        }

        public void GoToMainMenuButtonPress()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGivenScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void RestartButtonPress()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            if(GameManager.Instance.State != GameState.PlayerDead || GameManager.Instance.State != GameState.LevelGameOver || GameManager.Instance.State != GameState.LevelVictory)
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