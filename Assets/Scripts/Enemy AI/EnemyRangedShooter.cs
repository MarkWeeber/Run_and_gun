using UnityEngine;

namespace RunAndGun.Space
{
    public class EnemyRangedShooter : MonoBehaviour
    {
        [SerializeField] private float damageDealSphereRadius = 1f;
        [SerializeField] private float damageDealValue = 5f;
        [SerializeField] private float speed = 25f;
        [SerializeField] private LayerMask targetMask = 0;
        [SerializeField] private Transform projectilePrefab;

        private EnemyComponentsManager enemyComponentsManager;
        private Vector3 target;

        private void Awake()
        {
            enemyComponentsManager = GetComponentInParent<EnemyComponentsManager>();
            enemyComponentsManager.EnemyRangedShooter = this;
        }

        public void ShootAtTarget()
        {
            Transform _projectile = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation);
            ProjectileDamager _projectileDamager = _projectile.GetComponent<ProjectileDamager>();
            _projectileDamager.ProjectileSettings = new ProjectileSettings(damageDealSphereRadius, damageDealValue, speed, targetMask);
            _projectileDamager.SendProjectile(target);
        }

        public void SetTarget(Vector3 target)
        {
            this.target = target;
        }
    }
}