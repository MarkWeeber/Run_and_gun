using UnityEngine;

namespace Run_n_gun.Space
{
    public class FlipFaceControl : MonoBehaviour
    {
        [SerializeField] private Transform aimTarget = null;
        public Transform AimTarget { get { return aimTarget; } set { aimTarget = value; } }
        [SerializeField] private Transform parentTransform = null;
        public Transform ParentTransform { get { return parentTransform; } set { parentTransform = value; } }

        private float xDifference = 0f;
        private bool lookLeft = false;

        private void Start()
        {
            if(parentTransform != null && aimTarget != null)
            {
                xDifference = aimTarget.position.x - parentTransform.position.x;
                if (xDifference < 0)
                {
                    lookLeft = true;
                }
            }
            
        }

        private void Update()
        {
            if (parentTransform != null && aimTarget != null)
            {
                xDifference = aimTarget.position.x - parentTransform.position.x;
                if (xDifference < -0.01f && !lookLeft)
                {
                    parentTransform.Rotate(0, 180, 0);
                    lookLeft = true;
                }
                else if (xDifference > 0.01f && lookLeft)
                {
                    parentTransform.Rotate(0, 180, 0);
                    lookLeft = false;
                }
            }
        }
    }
}