using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Run_n_gun.Space
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints { get { return healthPoints; } set { OnHealthPointsAdded(value); } }
        public float StartingHealthPoints;
        private bool isDead = false;
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        private void Awake()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
            GameManager.OnPlayerHealthPointsAdded += OnHealthPointsAdded;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
            GameManager.OnPlayerHealthPointsAdded += OnHealthPointsAdded;
        }

        private void Start()
        {
            StartingHealthPoints = healthPoints;
            GameManager.PlayerStartingHealthPoints = StartingHealthPoints;
            // testing death event
            //InvokeRepeating(nameof(TestPlayeDeath),1.5f, 1f);
        }

        public void AddHealthPoints(float addHealthPoints)
        {
            GameManager.PlayerHealthPointsAdded(addHealthPoints);
        }

        private void OnHealthPointsAdded(float addedHealthPoints)
        {
            if (!isDead)
            {
                healthPoints += addedHealthPoints;
                if (healthPoints <= 0)
                {
                    GameManager.UpdateGameState(GameState.PlayerDead);
                }
            }
        }

        private void OnPlayerDeath()
        {
            isDead = true;  // mark as dead in current class
        }
        private void TestPlayeDeath()
        {
            AddHealthPoints(-25);
        }

        private void OnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnMainMenu:

                    break;
                case GameState.InGamePaused:

                    break;
                case GameState.InGameActive:

                    break;
                case GameState.PlayerDead:
                    OnPlayerDeath();
                    break;
                case GameState.LevelVictory:

                    break;
                case GameState.LevelGameOver:

                    break;
                default: break;
            }
        }
    }
}