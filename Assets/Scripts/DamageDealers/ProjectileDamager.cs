using UnityEngine;

namespace RunAndGun.Space
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileDamager : MonoBehaviour
    {
        public ProjectileSettings ProjectileSettings;

        private IDamagable damagable;
        private GameObject distinctObject = null;
        private Rigidbody rbody;

        private void Start()
        {
            rbody = GetComponent<Rigidbody>();
            rbody.useGravity = false;
            Debug.Log("START CALLED");
        }

        public void SendProjectile(Vector3 target)
        {
            Vector3 _distance = target - this.transform.position;
            float _height = _distance.y;
            Vector3 _halfRange = new Vector3(_distance.x, 0, _distance.z);
            float _Vy = Mathf.Sqrt(-2 * Physics.gravity.y * _height);
            Vector3 _VXZ = -(_halfRange*Physics.gravity.y) / _Vy;
            rbody.useGravity = true;
            rbody.velocity = new Vector3(_VXZ.x, _Vy, _VXZ.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((ProjectileSettings.targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                BlowUpProjectile();
                Destroy(this);
            }
        }

        private void BlowUpProjectile()
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, ProjectileSettings.damageDealSphereRadius, ProjectileSettings.targetMask);
            if (colliders.Length > 1)
            {
                foreach (Collider item in colliders)
                {
                    if (distinctObject != item.gameObject)
                    {
                        distinctObject = item.gameObject;
                    }
                    else
                    {
                        continue;
                    }
                    damagable = item.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(ProjectileSettings.damageDealValue);
                    }
                }
                distinctObject = null;
            }
        }

    }
}