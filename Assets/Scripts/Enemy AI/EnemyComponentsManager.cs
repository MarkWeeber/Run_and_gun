using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemyAIControl))]
    public class EnemyComponentsManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private EnemyMovement enemyMovement = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private EnemyAnimator enemyAnimator = null;
        [SerializeField] private TargetSpotter targetSpotter = null;
        [SerializeField] private EnemyAIControl enemyAIControl = null;
        [SerializeField] private EnemyHealth enemyHealth = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            enemyMovement = GetComponent<EnemyMovement>();
            animator = GetComponentInChildren<Animator>();
            enemyAnimator = GetComponent<EnemyAnimator>();
            targetSpotter = GetComponentInChildren<TargetSpotter>();
            enemyAIControl = GetComponent<EnemyAIControl>();
            enemyHealth = GetComponentInChildren<EnemyHealth>();
        }

        private void Start()
        {
            enemyMovement.Rigidbody = _rigidbody;
            enemyAnimator.Animator = animator;
            enemyAnimator.Rigidbody = _rigidbody;
            enemyAIControl.Spotter = targetSpotter;
            enemyAIControl.EnemyMovement = enemyMovement;
            enemyAIControl.ParentTransform = this.transform;
            enemyAIControl.EnemyAnimator = enemyAnimator;
            enemyHealth.EnemyAIControl = enemyAIControl;
        }
    }
}