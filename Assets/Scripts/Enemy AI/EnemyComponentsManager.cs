using System;
using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyAIControl))]
    public class EnemyComponentsManager : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;
        public Rigidbody Rigidbody { get { return _rigidbody; } }
        private EnemyMovement enemyMovement = null;
        public EnemyMovement EnemyMovement { get { return enemyMovement; } set { enemyMovement = value; } }
        private Animator animator = null;
        public Animator Animator { get { return animator; } set { animator = value; } }
        private TargetSpotter targetSpotter = null;
        public TargetSpotter TargetSpotter { get { return targetSpotter; } set { targetSpotter = value; } }
        private EnemyAnimator enemyAnimator;
        public EnemyAnimator EnemyAnimator { get { return enemyAnimator; } set { enemyAnimator = value; } }

        public event Action<EnemySpotState> OnSpotStateChanged;
        public event Action<float, float> OnHealthPointsChanged;
        public event Action<float> OnTakeDamage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {

        }

        public void UpdateSpotState(EnemySpotState enemySpotState)
        {
            OnSpotStateChanged?.Invoke(enemySpotState);
        }

        public void UpdateHealthPoints(float value1, float value2)
        {
            GameManager.Instance.EnemyHealthBar_UI.MaxFill = value2;
            GameManager.Instance.EnemyHealthBar_UI.CurrentFill = value1;
            OnHealthPointsChanged?.Invoke(value1, value2);
        }

        public void TakeDamage(float damageValue)
        {
            OnTakeDamage?.Invoke(damageValue);
        }
    }
}