using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float startingHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        [SerializeField] private LayerMask hitDetectFromPlayerLayer = 0;
        [SerializeField] private LayerMask playerAttackLayer = 0;
        [SerializeField] private Transform heathBarLocationTransform;
        [SerializeField] private EnemyAIControl enemyAIcontrol;
        public EnemyAIControl EnemyAIControl { get { return enemyAIcontrol; } set { enemyAIcontrol = value; } }

        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                GameManager.Instance.EnemyHealthBar_UI.FollowTarget = heathBarLocationTransform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                GameManager.Instance.EnemyHealthBar_UI.FollowTarget = null;
                if (GameManager.Instance.EnemyHealthBar_UI.FollowTarget == heathBarLocationTransform)
                {
                    GameManager.Instance.EnemyHealthBar_UI.FollowTarget = null;
                }
            }
        }
    }
}