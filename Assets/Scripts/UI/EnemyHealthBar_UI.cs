using UnityEngine;
using UnityEngine.UI;

namespace Run_n_gun.Space
{
    public class EnemyHealthBar_UI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private Transform followTarget;
        private Canvas canvas;
        public Transform FollowTarget { get { return followTarget; } set { if (value == null) { DeactivateSelf(); } else { ActivateSelf(); } followTarget = value; } }
        private float maxFill;
        public float MaxFill { get { return maxFill; } set { maxFill = value; UpdateFill(); } }
        private float currentFill;
        public float CurrentFill { get { return currentFill; } set { currentFill = value; } }
        
        private void Start()
        {
            canvas = GetComponent<Canvas>();
            DeactivateSelf();
        }

        private void Update()
        {
            FollowSetTarget();
        }

        private void FollowSetTarget()
        {
            if (followTarget != null)
            {
                this.transform.position = followTarget.position;
            }
        }

        private void DeactivateSelf()
        {
            canvas.enabled = false;
        }

        private void ActivateSelf()
        {
            canvas.enabled = true;
        }

        private void UpdateFill()
        {
            healthImage.fillAmount = Mathf.Clamp(currentFill, 0, maxFill )/ maxFill;
        }
    }
}