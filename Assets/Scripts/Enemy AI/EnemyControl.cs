using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyControl : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private EnemyMovement enemyMovement = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            enemyMovement = GetComponent<EnemyMovement>();
        }

        private void Start()
        {
            enemyMovement.Rigidbody = _rigidbody;
        }

    }
}