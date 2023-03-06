using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemySpotter : MonoBehaviour
    {
        [SerializeField] private LayerMask targetMask = 0;
        [SerializeField] private LayerMask obstructionMask = 0;
        private bool targetIsSpotted = false;
        private Transform targetObject = null;
        private Vector3 lastKnownPosition = Vector3.zero;
        private Ray ray;
        private Vector3 directionToTarget;
        private float distanceToTarget;

        private void Update()
        {
            if(targetIsSpotted)
            {

            }
        }

        private void OnTriggerStay(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                directionToTarget = other.transform.position - transform.position;
                distanceToTarget = directionToTarget.magnitude;
                Debug.DrawRay(transform.position, directionToTarget);
                directionToTarget = directionToTarget.normalized;
                ray = new Ray(transform.position, directionToTarget);
                targetObject = other.transform;
                if (!Physics.Raycast(ray, distanceToTarget, obstructionMask)) // check if no obstructions are seen
                {
                    targetIsSpotted = true;
                }
                else
                {
                    targetIsSpotted = false;
                    lastKnownPosition = targetObject.position;
                }
            }   
        }

        private void OnTriggerExit(Collider other)
        {
            if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                targetIsSpotted = false;
            }
        }   
    }
}