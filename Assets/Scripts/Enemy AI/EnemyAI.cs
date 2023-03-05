using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyTarget enemyTarget;
        public EnemyTarget EnemyTarget { get { return enemyTarget; } set { enemyTarget = value; OnEnemyTargetSet(); } }
        private void Start()
        {
            enemyTarget.IsFound = false;
        }


        private void Update()
        {

        }

        private void OnEnemyTargetSet()
        {

        }
    }
}