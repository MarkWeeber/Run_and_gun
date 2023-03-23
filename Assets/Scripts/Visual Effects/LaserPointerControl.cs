using UnityEngine;

namespace Run_n_gun.Space
{
    public class LaserPointerControl : MonoBehaviour
    {
        [SerializeField] private float maxDistance = 50f;
        [SerializeField] private LayerMask hitMask = 0;
        [SerializeField] private Transform laserBeamDirectionalScaler = null;
        [SerializeField] private Transform laserDotSprite = null;
        [SerializeField] private string hitName = "";
        private Ray ray;
        private RaycastHit hitInfo;

        private void Start()
        {
            ray = new Ray(transform.position, Vector3.left);
            laserDotSprite.gameObject.SetActive(false);
        }


        private void LateUpdate()
        {
            RayCastLaserBeam();
        }

        private void RayCastLaserBeam()
        {
            ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
            hitName = "";
            if (Physics.Raycast(ray, out hitInfo, maxDistance, hitMask))
            {
                RayHasHit();
            }
            else
            {
                RayHasNoHit();
            }
        }

        private void RayHasHit()
        {
            laserDotSprite.gameObject.SetActive(true);
            laserDotSprite.transform.position = hitInfo.point;
            hitName = hitInfo.transform.name;
            laserBeamDirectionalScaler.transform.localScale = new Vector3(hitInfo.distance,
                                                                            laserBeamDirectionalScaler.transform.localScale.y,
                                                                            laserBeamDirectionalScaler.transform.localScale.z);
        }

        private void RayHasNoHit()
        {
            laserDotSprite.transform.position = this.transform.position;
            laserDotSprite.gameObject.SetActive(false);
            laserBeamDirectionalScaler.transform.localScale = new Vector3(maxDistance,
                                                                            laserBeamDirectionalScaler.transform.localScale.y,
                                                                            laserBeamDirectionalScaler.transform.localScale.z);
        }
    }
}