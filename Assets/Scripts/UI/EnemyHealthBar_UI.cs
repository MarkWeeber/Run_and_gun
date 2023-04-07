using UnityEngine;
using UnityEngine.UI;

namespace Run_n_gun.Space
{
    public class EnemyHealthBar_UI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private Transform followTarget;
        public Transform FollowTarget { get { return followTarget; } set { if (value == null) { DeactivateSelf(); } else { ActivateSelf(); } followTarget = value; } }
        [SerializeField] private Vector2 healthBarValues = Vector2.one;
        public Vector3 HeathBarValues { set { healthBarValues = value; SetHealthBarValues(value); } get { return healthBarValues; } }
        [SerializeField] private Canvas canvas;
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

        private void SetHealthBarValues(Vector2 values)
        {
            healthImage.fillAmount = values.x / values.y;
        }
    }
}