using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Run_n_gun.Space
{
    public class RecoilControl : MonoBehaviour
    {
        [SerializeField] private float returnRate = 2f;
        [SerializeField] private float recoilRate = 5f;
        [SerializeField] private float backRecoilPerShot = 0.1f;
        [SerializeField] private float upRecoilPerShot = 0.1f;
        [SerializeField] private float recoilSpeed = 1f;
        [SerializeField] private float maxBackDistance = 0.05f;
        [SerializeField] private float maxRecoilWeightRate = 1f;
        [SerializeField] private Transform weaponTransform = null;
        [SerializeField] private Transform holderTransform = null;
        [SerializeField] private Transform recoilPointTransform = null;
        [SerializeField] private MultiAimConstraint rightHandAimConstraint = null;
        private Vector3 holderOrigin = Vector3.zero;
        private Vector3 holderToWeaponRotation = Vector3.zero;
        private float currentBackRecoilDistance = 0f;
        private float currentRecoilWeightRate = 0f;
        private float targetBackRecoilDistance = 0f;
        private float targetRecoilWeightRate = 0f;
        private float distanceToBackRecoil = 0f;
        private float distanceToRecoilWeightRate = 0f;
        private bool recoilNow = false;
        private bool recoilReached = false;

        private void Start()
        {
            holderOrigin = holderTransform.localPosition;
            holderToWeaponRotation = weaponTransform.position - holderTransform.position;
        }

        private void Update()
        {
            // making recoil shot
            if (recoilNow)
            {
                if(!recoilReached)
                {
                    distanceToBackRecoil = targetBackRecoilDistance - currentBackRecoilDistance;
                    distanceToRecoilWeightRate = targetRecoilWeightRate - currentRecoilWeightRate;
                    if (distanceToBackRecoil <= 0 || distanceToRecoilWeightRate <= 0)
                    {
                        recoilReached = true;
                    }
                    else
                    {
                        currentBackRecoilDistance += backRecoilPerShot * Time.deltaTime;
                        currentRecoilWeightRate += upRecoilPerShot * Time.deltaTime;
                    }
                }
                else
                {
                    currentBackRecoilDistance += backRecoilPerShot * Time.deltaTime;
                    currentRecoilWeightRate += upRecoilPerShot * Time.deltaTime;
                    if (currentBackRecoilDistance <= 0 || currentRecoilWeightRate <= 0)
                    {

                    }
                }
            }
        }

        public void Recoil()
        {
            recoilNow = true;
            targetBackRecoilDistance = Mathf.Clamp(targetBackRecoilDistance * (1 + recoilRate), 0, maxBackDistance);
            targetRecoilWeightRate = Mathf.Clamp(targetRecoilWeightRate * (1 + recoilRate), 0, maxRecoilWeightRate);
            recoilReached = false;

        }
    }
}