using UnityEngine;
namespace Run_n_gun.Space
{
    public class PlayerInput : MonoBehaviour
    {
        public bool Activated { get { return activated; } set { activated = value; } }
        private bool activated = false;
        [SerializeField] private float deadZone = 0.01f;
        public PlayerMovement PlayerMovement { get { return playerMovement; } set { playerMovement = value; } }
        private PlayerMovement playerMovement = null;
        private float horizontalInput = 0f;
        private bool jumpButtonPressed = false;

        private void Update()
        {
            if (activated)
            {
                // tracking horizontal inputs
                horizontalInput = 0f;
                horizontalInput += Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
                if (horizontalInput > deadZone || horizontalInput < -deadZone)
                {
                    playerMovement.Move(horizontalInput);
                }
                // tracking jump button press
                jumpButtonPressed = false;
                if (Input.GetButtonDown(GlobalStringVars.JUMP_BUTTON))
                {
                    jumpButtonPressed = true;
                    playerMovement.Jump();
                }
            }
            else
            {
                horizontalInput = 0f;
                jumpButtonPressed = false;
            }
        }
    }
}