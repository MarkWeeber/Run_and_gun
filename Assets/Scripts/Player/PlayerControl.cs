using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Run_n_gun.Space { 
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FlipFaceControl))]
[RequireComponent(typeof(PlayerHealth))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput= null;
        [SerializeField] private PlayerMovement playerMovement = null;
        [SerializeField] private IsGroundedControl isGroundedControl = null;
        [SerializeField] private PlayerAnimatorControl playerAnimatorControl = null;
        [SerializeField] private Transform aimTarget = null;
        [SerializeField] private FlipFaceControl flipFaceControl = null;
        [SerializeField] private RecoilControl recoilControl = null;
        [SerializeField] private PlayerHealth playerHealth = null;
        [SerializeField] private RigBuilder rigBuilder = null;
        [SerializeField] private Animator animator = null;
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            flipFaceControl = GetComponent<FlipFaceControl>();
            isGroundedControl = GetComponentInChildren<IsGroundedControl>();
            playerAnimatorControl = GetComponentInChildren<PlayerAnimatorControl>();
            recoilControl = GetComponent<RecoilControl>();
            playerHealth = GetComponent<PlayerHealth>();
            rigBuilder = GetComponentInChildren<RigBuilder>();
            animator = GetComponentInChildren<Animator>();
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
            playerHealth.Animator = animator;
            playerHealth.RigBuilder = rigBuilder;
            playerHealth.PlayerInput = playerInput;
            playerHealth.FlipFaceControl = flipFaceControl;
        }
    }
}