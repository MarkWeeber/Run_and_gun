using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemySpotter : MonoBehaviour
    {
        public Transform dummy = null;
        [SerializeField] private LayerMask targetMask = 0;
        [SerializeField] private LayerMask obstructionMask = 0;
        [SerializeField] private EnemyGroupSpotter enemyGroupSpotter = null;
        [SerializeField] private float groupUpdateRateInSeconds = 1f;
        [SerializeField] private bool targetIsSpotted = false;
        public bool TargetIsSpotted { get { return targetIsSpotted; } }
        private bool targetIsSpottedIndividually = false;
        private Transform targetTransform = null;
        public Transform TargetTransform { get { return targetTransform; } }
        private Vector3 lastKnownPosition = Vector3.zero;
        public Vector3 LastKnownPosition { get { return lastKnownPosition; } }
        private Ray ray;
        private Vector3 directionToTarget;
        private float distanceToTarget;
        private float nextUpdate = 0f;

        private void Start()
        {
            enemyGroupSpotter = GetComponentInParent<EnemyGroupSpotter>();
        }

        private void Update()
        {
            targetIsSpotted = false;
            if (!targetIsSpotted && targetIsSpottedIndividually)
            {
                targetIsSpotted = true;
            }
            if (Time.time > nextUpdate)
            {
                nextUpdate = Time.time + groupUpdateRateInSeconds;
                UpdateGroupSpotter();
            }
            if (
                enemyGroupSpotter != null &&
                !targetIsSpotted &&
                enemyGroupSpotter.EnemySpotted &&
                enemyGroupSpotter.EnemySpotter != this
                ) // check group spotter for any found targets
            {
                targetIsSpotted = true;
                targetTransform = enemyGroupSpotter.TargetTransform;
                lastKnownPosition = enemyGroupSpotter.LastKnownPosition;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                CheckIfTargetIsVisible(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0) // target left vision
            {
                if (targetIsSpottedIndividually) // target was seen before leaving vision
                {
                    UpdateLastKnowPosition();
                }
            }
        }

        private void CheckIfTargetIsVisible(Transform potentialTarget)
        {
            directionToTarget = potentialTarget.transform.position - transform.position;
            distanceToTarget = directionToTarget.magnitude;
            Debug.DrawRay(transform.position, directionToTarget);
            directionToTarget = directionToTarget.normalized;
            ray = new Ray(transform.position, directionToTarget);
            targetTransform = potentialTarget.transform;
            if (!Physics.Raycast(ray, distanceToTarget, obstructionMask)) // check if no obstructions are seen via raycast
            {
                SpotTheTarget();
            }
            else if (targetIsSpottedIndividually) // target was seen before and now some obstructions appear
            {
                UpdateLastKnowPosition();
            }
        }

        private void SpotTheTarget()
        {
            targetIsSpottedIndividually = true;
            UpdateGroupSpotter(); // imediatelly allert others
        }

        private void UpdateLastKnowPosition()
        {
            targetIsSpottedIndividually = false;
            lastKnownPosition = targetTransform.position;
            if (dummy != null)
            {
                dummy.position = lastKnownPosition;
            }
        }

        private void UpdateGroupSpotter()
        {
            if (enemyGroupSpotter != null)
            {
                if (targetIsSpottedIndividually)
                {
                    enemyGroupSpotter.SetTarget(this, targetTransform);
                }
                else
                {
                    enemyGroupSpotter.ResetTargeting(this);
                }
            }
        }
    }
}