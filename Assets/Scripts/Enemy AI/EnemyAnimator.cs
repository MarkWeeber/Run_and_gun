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
            animator.SetFloat("MoveSpeed", Mathf.Abs(Mathf.Round(_rigidbody.velocity.x * 100f) / 100f));
        }

        public void AnimateAttack()
        {
            animator.SetBool("Attacking", true);
        }

        public void StopAnimateAttack()
        {
            animator.SetBool("Attacking", false);
        }

        public void AnimateIdle()
        {
            animator.SetBool("Idle2", false);
            animator.SetBool("Attacking", false);
        }

        public void AnimateIdle2()
        {
            animator.SetBool("Idle2", true);
        }

        public void StopAnimateIdle2()
        {
            animator.SetBool("Idle2", false);
        }

        public void AnimateDeath()
        {
            animator.SetTrigger("Die");
        }
    }
}