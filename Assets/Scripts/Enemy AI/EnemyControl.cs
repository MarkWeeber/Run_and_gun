using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private EnemyMovement enemyMovement = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private EnemyAnimator enemyAnimator = null;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            enemyMovement = GetComponent<EnemyMovement>();
            animator = GetComponentInChildren<Animator>();
            enemyAnimator = GetComponent<EnemyAnimator>();
        }

        private void Start()
        {
            enemyMovement.Rigidbody = _rigidbody;
            enemyAnimator.Animator = animator;
            enemyAnimator.Rigidbody = _rigidbody;
        }

    }
}