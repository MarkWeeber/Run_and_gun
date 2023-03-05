using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;
        public Animator Animator { get { return animator; } set { animator = value; } }
        [SerializeField] private Rigidbody _rigidbody = null;
        public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }

        private void Update()
        {
            animator.SetFloat("MoveSpeed", Mathf.Abs(Mathf.Round(_rigidbody.velocity.x * 100f) / 100f) );
        }
    }
}