using UnityEngine;
namespace Run_n_gun.Space
{
    [RequireComponent(typeof(SphereCollider))]
    public class IsGroundedControl : MonoBehaviour
    {
        public bool IsGrounded { get { return isGrounded; } }
        [SerializeField] bool isGrounded = false;
        [SerializeField] private LayerMask groundedMask = 0;
        private LayerMask actualMask = 0;
        private SphereCollider sphereCollider;
        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics.OverlapSphere(this.transform.position, sphereCollider.radius, groundedMask).Length > 0;
        }
    }
}