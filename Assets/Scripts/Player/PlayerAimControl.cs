using UnityEngine;
namespace Run_n_gun.Space
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAimControl : MonoBehaviour
    {
        private Animator animator = null;
        [SerializeField] private float deadZoneRadius = 3f;
        [SerializeField] private AimPointerTracker aimPointerTracker = null;
        public AimPointerTracker AimPointerTracker { get { return aimPointerTracker; } set { aimPointerTracker = value; } }
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        
        private void Update()
        {

        }
    }
}