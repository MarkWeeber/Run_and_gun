using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Run_n_gun.Space { 
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FlipFaceControl))]
[RequireComponent(typeof(PlayerHealth))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private Transform aimTarget = null;
        [SerializeField] private FlipFaceControl flipFaceControl = null;
        private void Awake()
        {
            flipFaceControl = GetComponent<FlipFaceControl>();
        }
        private void Start()
        {
            flipFaceControl.AimTarget = aimTarget;
            flipFaceControl.ParentTransform = this.transform;
        }
    }
}