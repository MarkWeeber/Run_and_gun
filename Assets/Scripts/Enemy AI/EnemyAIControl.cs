using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyAIControl : MonoBehaviour
    {
        [SerializeField]
        private TargetSpotter spotter;
        public TargetSpotter Spotter { get { return spotter; } set { spotter = value; } }
        [SerializeField]
        private EnemyMovement enemyMovement = null;
        public EnemyMovement EnemyMovement { get { return enemyMovement; } set { enemyMovement = value; } }
        [SerializeField]
        private Transform parentTransform = null;
        public Transform ParentTransform { get { return parentTransform; } set { parentTransform = value; } }
        [SerializeField]
        private EnemyAnimator enemyAnimator = null;
        public EnemyAnimator EnemyAnimator { get { return enemyAnimator; } set { enemyAnimator = value; } }

        [SerializeField] private float lostDuration = 30f;
        [SerializeField] private float distanceToInteract = 1f;
        [SerializeField] private Vector3 originalPosition;

        private float _distanceToTarget;
        private float _distanceToLastKnownPosition;
        private float _distanceToOriginalPost;
        private float _xDifference;
        private bool _lookLeft = true;

        private void Start()
        {
            originalPosition = transform.position;
        }

        private void Update()
        {
            ManageSpotData();
        }

        private void ManageSpotData()
        {
            switch (spotter.SpotData.enemySpotState)
            {
                case EnemySpotState.NoTarget:
                    ReturnToOriginalPost();
                    break;
                case EnemySpotState.TargetIsVisible:
                    ActionOnTarget();
                    break;
                case EnemySpotState.AlertedOnTarget:
                    GoToTargetsLastKnowPosition();
                    break;
                case EnemySpotState.TargetLost:
                    GoToTargetsLastKnowPosition();
                    break;
                default: break;
            }
        }

        private void DropDead()
        {
            enemyMovement.StopMoving();
            enemyAnimator.StopAnimateAttack();
            enemyAnimator.AnimateDeath();
        }

        private void ReturnToOriginalPost()
        {
            _distanceToOriginalPost = Mathf.Abs(originalPosition.x - parentTransform.position.x);
            if (_distanceToOriginalPost > distanceToInteract)
            {
                MoveTowards(originalPosition);
            }
            else
            {
                enemyMovement.StopMoving();
                enemyAnimator.StopAnimateAttack();
                enemyAnimator.AnimateIdle();
            }
        }

        private void ActionOnTarget()
        {
            _distanceToTarget = Mathf.Abs(transform.position.x - spotter.SpotData.targetTransform.position.x);
            if (_distanceToTarget > distanceToInteract)
            {
                MoveTowards(spotter.SpotData.targetTransform.position);
            }
            else
            {
                enemyMovement.StopMoving();
                enemyAnimator.AnimateAttack();
            }
        }

        private void GoToTargetsLastKnowPosition()
        {
            if (spotter.SpotData.spotTime + lostDuration > Time.time)
            {
                _distanceToLastKnownPosition = Mathf.Abs(transform.position.x - spotter.SpotData.lastKnownPosition.x);
                if (_distanceToLastKnownPosition > distanceToInteract)
                {
                    MoveTowards(spotter.SpotData.lastKnownPosition);
                }
                else
                {
                    enemyMovement.StopMoving();
                    enemyAnimator.StopAnimateAttack();
                    enemyAnimator.AnimateIdle2();
                }
            }
            else
            {
                spotter.ForgetTheTarget();
            }
        }

        private void MoveTowards(Vector3 destination)
        {
            _xDifference = destination.x - parentTransform.position.x;
            if (_xDifference > 0.01f && _lookLeft)
            {
                parentTransform.Rotate(0, 180, 0);
                _lookLeft = false;
            }
            else if (_xDifference < -0.01f && !_lookLeft)
            {
                parentTransform.Rotate(0, 180, 0);
                _lookLeft = true;
            }
            enemyAnimator.StopAnimateAttack();
            enemyAnimator.StopAnimateIdle2();
            enemyMovement.Moving();
        }
    }
}
