using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemySpotter enemySpotter = null;
        public EnemySpotter EnemySpotter { get { return enemySpotter; } set { enemySpotter = value; } }
        [SerializeField] private EnemyMovement enemyMovement = null;
        public EnemyMovement EnemyMovement { get { return enemyMovement; } set { enemyMovement = value; } }
        [SerializeField] private Transform parentTransform = null;
        public Transform ParentTransform { get { return parentTransform; } set { parentTransform = value; } }
        [SerializeField] private EnemyAnimator enemyAnimator = null;
        public EnemyAnimator EnemyAnimator { get { return enemyAnimator; } set { enemyAnimator = value; } }
        [SerializeField] private float lostTime = 5f;
        [SerializeField] private float distanceToInteract = 1f;
        private Vector3 originalPosition = Vector3.zero;
        private float lostTimer;
        private float distanceToTarget;
        private float distanceToOriginalPost;
        private float xDifference;
        private bool lookLeft = true;

        private void Start()
        {
            originalPosition = transform.position;
            lookLeft = true;
        }

        private void Update()
        {
            if (enemySpotter.TargetIsSpotted)
            {
                distanceToTarget = Vector3.Distance(transform.position, enemySpotter.TargetTransform.position);
                if(distanceToTarget > distanceToInteract) // move towards target
                {
                    MoveTowards(enemySpotter.TargetTransform.position);
                }
                else // start attacking when reached
                {
                    AttackingTarget();
                }
                lostTimer = lostTime;
            }
            else if (lostTimer > 0.01f)
            {
                distanceToTarget = Vector3.Distance(transform.position, enemySpotter.LastKnownPosition);
                if (distanceToTarget > distanceToInteract) // move towards last know position
                {
                    MoveTowards(enemySpotter.LastKnownPosition);
                }
                else // when reached idle
                {
                    IdleAtLastKnowPosition();
                }
                lostTimer -= Time.deltaTime;
            }
            else // return to original post
            {
                ReturnToOriginalPost();
            }
        }

        private void IdleAtLastKnowPosition()
        {
            // stop movement
            enemyMovement.StopMoving();
            // animate idle2
            enemyAnimator.AnimateIdle2();
        }

        private void AttackingTarget()
        {
            // stop movement
            enemyMovement.StopMoving();
            // animate attacking, target is already reached
            enemyAnimator.AnimateAttack();
        }

        private void ReturnToOriginalPost()
        {
            // animate Idle when reached original post, else make move
            distanceToOriginalPost = Vector3.Distance(originalPosition, parentTransform.position);
            if (distanceToOriginalPost < distanceToInteract)
            {
                enemyAnimator.AnimateIdle2();
            }
            else
            {
                MoveTowards(originalPosition);
            }
        }

        private void MoveTowards(Vector3 destination)
        {
            // check facing and flip
            xDifference = destination.x - parentTransform.position.x;
            if (xDifference > 0.01f && lookLeft)
            {
                parentTransform.Rotate(0, 180, 0);
                //parentTransform.eulerAngles = new Vector3(parentTransform.rotation.x, parentTransform.rotation.y + 180, parentTransform.rotation.z);
                lookLeft = false;
            }
            else if (xDifference < -0.01f && !lookLeft)
            {
                parentTransform.Rotate(0, 180, 0);
                //parentTransform.eulerAngles = new Vector3(parentTransform.rotation.x, parentTransform.rotation.y + 180, parentTransform.rotation.z);
                lookLeft = true;
            }
            // stop animating states other than movement
            enemyAnimator.StopAnimateAttack();
            enemyAnimator.StopAnimateIdle2();
            // make move
            enemyMovement.Moving();
        }
    }
}