using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyGroupSpotter : MonoBehaviour
    {
        [SerializeField] bool enemySpotted = false;
        public bool EnemySpotted { get { return enemySpotted; } }
        [SerializeField] private Transform targetTransform = null;
        public Transform TargetTransform { get { return targetTransform; } }
        public Vector3 LastKnownPosition = Vector3.zero;
        [SerializeField] private EnemySpotter enemySpotter = null;
        public EnemySpotter EnemySpotter { get { return enemySpotter; } }

        public void SetTarget(EnemySpotter enemySpotter, Transform targetTransform)
        {
            this.enemySpotter = enemySpotter;
            this.targetTransform = targetTransform;
            enemySpotted = true;
        }

        public void ResetTargeting(EnemySpotter enemySpotter)
        {
            if (enemySpotter == this.enemySpotter)
            {
                Debug.Log("CHECK");
                this.enemySpotter = null;
                LastKnownPosition = targetTransform.position;
                this.targetTransform = null;
                this.enemySpotted = false;
            }
        }
    }
}