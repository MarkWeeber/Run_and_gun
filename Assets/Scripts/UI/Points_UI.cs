using UnityEngine;
using TMPro;

namespace RunAndGun.Space
{
    public class Points_UI : MonoBehaviour
    {
        [SerializeField] private GameResultValueType valueType;
        private TMP_Text text;
        private float value;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            switch (valueType)
            {
                case GameResultValueType.Points:
                    value = GlobalBuffer.gamePoints.Points;
                    break;
                case GameResultValueType.EnemiesKilled:
                    value = GlobalBuffer.gamePoints.EnemiesKilled;
                    break;
                case GameResultValueType.CurrentHealthPoints:
                    value = GlobalBuffer.gamePoints.CurrentHealth;
                    break;
                case GameResultValueType.CurrentAmmoRounds:
                    value = GlobalBuffer.gamePoints.CurrentAmmoCount;
                    break;
                default:
                    break;
            }
            text.text = value.ToString();
        }
    }
}