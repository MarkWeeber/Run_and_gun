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

        private void Awake()
        {
            rbody = GetComponent<Rigidbody>();
            rbody.useGravity = false;
        }

        public void SendProjectile(Vector3 target)
        {
            // Vector3 _direction = target - this.transform.position;
            // float _height = _direction.y;
            // _direction.y = 0;
            // float _horizontalDistance = _direction.magnitude;
            // float _g = Physics.gravity.y; // 9.81f;
            // float _speedSqr = ProjectileSettings.speed;
            // float _root = (_speedSqr * _speedSqr) - _g * (_g * _horizontalDistance* _horizontalDistance + 2 * _height * _speedSqr);
            float angle = 0.5f * (Mathf.Asin((Physics.gravity.y * Vector3.Distance(target, this.transform.position)) / (ProjectileSettings.speed * ProjectileSettings.speed)) * Mathf.Rad2Deg);
            Quaternion angleRotation =  Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 shootDirection = angleRotation * this.transform.forward;
            Debug.Log(shootDirection);
            rbody.useGravity = true;
            //rbody.velocity = Vector3.right + Vector3.up;
            rbody.velocity = shootDirection * ProjectileSettings.speed;
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