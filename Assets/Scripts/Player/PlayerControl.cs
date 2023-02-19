using UnityEngine;

namespace Run_n_gun.Space { 
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput= null;
        [SerializeField] private PlayerMovement playerMovement = null;
        [SerializeField] private IsGroundedControl isGroundedControl = null;
        [SerializeField] private AimPointerTracker aimPointerTracker = null;
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            isGroundedControl = GetComponentInChildren<IsGroundedControl>();
        }
        private void Start()
        {
            playerInput.Activated = true;
            playerInput.PlayerMovement = playerMovement;
            playerMovement.IsGroundedControl = isGroundedControl;
        }
        private void Update()
        {

        }
    }
}