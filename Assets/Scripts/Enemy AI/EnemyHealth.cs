using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private float startingHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        [SerializeField] private LayerMask hitDetectFromPlayerLayer = 0;
        [SerializeField] private LayerMask playerAttackLayer = 0;
        [SerializeField] private Transform heathBarLocationTransform;

        private EnemyComponentsManager enemyComponentsManager;
        private IDamager damager;

        private void Awake()
        {
            enemyComponentsManager = GetComponentInParent<EnemyComponentsManager>();
            enemyComponentsManager.OnHealthPointsChanged += HealthPointsUpdated;
            enemyComponentsManager.OnTakeDamage += OnTakeDamage;
        }

        private void OnDestroy()
        {
            enemyComponentsManager.OnHealthPointsChanged -= HealthPointsUpdated;
            enemyComponentsManager.OnTakeDamage -= OnTakeDamage;
        }

        private void HealthPointsUpdated(float value1, float value2)
        {
            currentHealth = value1;
            startingHealth = value2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                GameManager.Instance.EnemyHealthBar_UI.FollowTarget = heathBarLocationTransform;
                GameManager.Instance.EnemyHealthBar_UI.CurrentFill = currentHealth;
                GameManager.Instance.EnemyHealthBar_UI.MaxFill = startingHealth;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                GameManager.Instance.EnemyHealthBar_UI.FollowTarget = null;
            }
        }

        private void OnTakeDamage(float damageValue)
        {
            currentHealth -= damageValue;
        }
        public void TakeDamage(float damageValue)
        {
            enemyComponentsManager.TakeDamage(damageValue);
        }
    }
}