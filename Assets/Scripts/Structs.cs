using UnityEngine;

namespace RunAndGun.Space
{
    [System.Serializable]
    public struct TargetSpotData
    {
        public EnemySpotState enemySpotState;
        public Transform targetTransform;
        public Vector3 lastKnownPosition;
        public float spotTime;
        public TargetSpotData(EnemySpotState enemySpotState = EnemySpotState.NoTarget, Transform targetTransform = null)
        {
            this.enemySpotState = EnemySpotState.NoTarget;
            this.targetTransform = null;
            this.lastKnownPosition = Vector3.zero;
            this.spotTime = 0;
        }
    }
    [System.Serializable]
    public struct GamePoints
    {
        public int Points;
        public int CurrentAmmoCount;
        public int EnemiesKilled;
        public float CurrentHealth;
    }

    public struct ProjectileSettings
    {
        public float damageDealSphereRadius;
        public float damageDealValue;
        public float speed;
        public LayerMask targetMask;
        public ProjectileSettings(float damageDealSphereRadius, float damageDealValue, float speed, LayerMask targetMask)
        {
            this.damageDealSphereRadius = damageDealSphereRadius;
            this.damageDealValue = damageDealValue;
            this.speed = speed;
            this.targetMask = targetMask;
        }
    }
}