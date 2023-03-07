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
        private bool targetIsSpotted = false;
        public bool TargetIsSpotted { get { return targetIsSpotted; } set { targetIsSpotted = value; } }
        private Transform targetTransform = null;
        public Transform TargetTransform { get { return targetTransform; } set { targetTransform = value; } }
        private Vector3 lastKnownPosition = Vector3.zero;
        public Vector3 LastKnownPosition { get { return lastKnownPosition; } }
        private Ray ray;
        private Vector3 directionToTarget;
        private float distanceToTarget;

        private void OnTriggerStay(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                directionToTarget = other.transform.position - transform.position;
                distanceToTarget = directionToTarget.magnitude;
                Debug.DrawRay(transform.position, directionToTarget);
                directionToTarget = directionToTarget.normalized;
                ray = new Ray(transform.position, directionToTarget);
                targetTransform = other.transform;
                if (!Physics.Raycast(ray, distanceToTarget, obstructionMask)) // check if no obstructions are seen
                {
                    targetIsSpotted = true;
                }
                else if(targetIsSpotted) // target was seen before and now some obstructions appear
                {
                    targetIsSpotted = false;
                    lastKnownPosition = targetTransform.position;
                    if(dummy != null)
                    {
                        dummy.position = lastKnownPosition;
                    }
                }
            }   
        }

        private void OnTriggerExit(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0) // target left vision
            {
                if(targetIsSpotted) // target was seen before leaving vision
                {
                    targetIsSpotted = false;
                    lastKnownPosition = targetTransform.position;
                    if (dummy != null)
                    {
                        dummy.position = lastKnownPosition;
                    }
                }
            }
        }   
    }
}