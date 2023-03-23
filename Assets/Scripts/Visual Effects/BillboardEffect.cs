using UnityEngine;

namespace Run_n_gun.Space
{
    public class BillboardEffect : MonoBehaviour
    {
        private Camera mainCam;
        private void Start()
        {
            mainCam = Camera.main;
        }
        private void LateUpdate()
        {
            // rotate as camera rotates
            transform.rotation = mainCam.transform.rotation;
            // remove rotation on X and Z
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }
}