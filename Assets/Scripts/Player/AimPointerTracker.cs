using UnityEngine;
using Cinemachine;

namespace Run_n_gun.Space {
    public class AimPointerTracker : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachine = null;
        [SerializeField] private LayerMask hitmask = 0;
        [SerializeField] private Transform dummy = null;
        private Vector3 mousePointerPosition = Vector3.zero;
        public Vector3 MousePointerPosition { get { return mousePointerPosition; } }
        private Camera mainCamera = null;
        private Ray ray;
        private RaycastHit hitInfo;
        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (cinemachine != null && cinemachine.isActiveAndEnabled)
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, 99999f, hitmask))
                {
                    mousePointerPosition = hitInfo.point;
                    if (dummy != null)
                    {
                        dummy.transform.position = hitInfo.point;
                    }
                }
            }
        }
    }
}