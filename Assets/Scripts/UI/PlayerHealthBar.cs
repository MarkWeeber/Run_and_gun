using UnityEngine;

namespace Run_n_gun.Space
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform canvas = null;
        private float originalWidth = 0f;
        private void Awake()
        {
            GameManager.OnPlayerHealthPointsAdded += OnHealthPointsAdded;
        }

        private void Start()
        {
            canvas = GetComponent<RectTransform>();
            originalWidth = canvas.sizeDelta.x;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerHealthPointsAdded -= OnHealthPointsAdded;
        }

        private void OnHealthPointsAdded(float addedHealthPoints)
        {
            canvas.sizeDelta = new Vector2(
                canvas.sizeDelta.x + originalWidth * (addedHealthPoints / GameManager.PlayerStartingHealthPoints),
                canvas.sizeDelta.y);
        }

    }
}