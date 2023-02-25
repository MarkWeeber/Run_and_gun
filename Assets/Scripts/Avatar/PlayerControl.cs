using UnityEngine;

namespace Run_n_gun.Space { 
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FlipFaceControl))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput= null;
        [SerializeField] private PlayerMovement playerMovement = null;
        [SerializeField] private IsGroundedControl isGroundedControl = null;
        [SerializeField] private PlayerAnimatorControl playerAnimatorControl = null;
        [SerializeField] private Transform aimTarget = null;
        [SerializeField] private FlipFaceControl flipFaceControl = null;
        [SerializeField] private RecoilControl recoilControl = null;
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            flipFaceControl = GetComponent<FlipFaceControl>();
            isGroundedControl = GetComponentInChildren<IsGroundedControl>();
            playerAnimatorControl = GetComponentInChildren<PlayerAnimatorControl>();
            recoilControl = GetComponent<RecoilControl>();
        }
        private void Start()
        {
            playerInput.Activated = true;
            playerInput.PlayerMovement = playerMovement;
            playerInput.RecoilControl = recoilControl;
            flipFaceControl.AimTarget = aimTarget;
            flipFaceControl.ParentTransform = this.transform;
            playerMovement.IsGroundedControl = isGroundedControl;
            playerAnimatorControl.PlayerMovement = playerMovement;
            playerAnimatorControl.IsGroundedControl = isGroundedControl;
        }
    }
}