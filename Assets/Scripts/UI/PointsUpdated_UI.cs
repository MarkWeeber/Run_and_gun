using UnityEngine;
using TMPro;

namespace RunAndGun.Space
{
    public class PointsUpdated_UI : MonoBehaviour
    {
        [SerializeField] private GameResultValueType valueType;
        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
            ManageSubscription();
        }

        private void ManageSubscription()
        {
            switch (valueType)
            {
                case GameResultValueType.Points:
                    GameManager.OnPointsUpdated.AddListener(UpdatePoints);
                    break;
                case GameResultValueType.EnemiesKilled:
                    GameManager.OnEnemiesKilledCountUpdated.AddListener(UpdatePoints);
                    break;
                case GameResultValueType.CurrentHealthPoints:
                    GameManager.OnHealthPointsUpdated.AddListener(UpdatePoints);
                    break;
                case GameResultValueType.CurrentAmmoRounds:
                    GameManager.OnAmmoUpdated.AddListener(UpdatePoints);
                    break;
                default:
                    break;
            }
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            float value = 0f;
            switch (valueType)
            {
                case GameResultValueType.Points:
                    value = GameManager.GamePoints.Points;
                    break;
                case GameResultValueType.EnemiesKilled:
                    value = GameManager.GamePoints.EnemiesKilled;
                    break;
                case GameResultValueType.CurrentHealthPoints:
                    value = GameManager.GamePoints.CurrentHealth;
                    break;
                case GameResultValueType.CurrentAmmoRounds:
                    value = GameManager.GamePoints.CurrentAmmoCount;
                    break;
                default:
                    break;
            }
            text.text = ((int)Mathf.Clamp(value, 0, 9999f)).ToString();
        }
    }
}