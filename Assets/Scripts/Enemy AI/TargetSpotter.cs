using UnityEngine;

namespace Run_n_gun.Space
{
    [System.Serializable]
    public struct TargetSpotter
    {
        public EnemySpotState enemySpotState;
        public Transform targetTransform;
        public Vector3 lastKnowPosition;

        public TargetSpotter(EnemySpotState enemySpotState = EnemySpotState.NoTarget, Transform targetTransform = null)
        {
            this.enemySpotState = EnemySpotState.NoTarget;
            this.targetTransform = null;
            this.lastKnowPosition = Vector3.zero;
        }
    }
}