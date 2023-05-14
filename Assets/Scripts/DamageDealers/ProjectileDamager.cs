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
        public Vector3 target;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody>();
            rbody.useGravity = false;
        }

        public void SendProjectile(Vector3 target)
        {
            this.target = target;
            float? angle = CalculateAnge(target, true);
            if (angle != null)
            {
                float _angle = (float)angle;
                Debug.Log(_angle);
                this.transform.localEulerAngles = new Vector3(360f - _angle, 0f, 0f);
                //Quaternion angleRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
                //Vector3 shootDirection = angleRotation * this.transform.forward;
                //Debug.Log(shootDirection);
                //rbody.useGravity = true;
                //rbody.velocity = this.transform.forward * ProjectileSettings.speed;
            }
            else
            {
                Debug.Log("COULDN'T CALCULATE");
            }
        }

        private float? CalculateAnge(Vector3 target, bool low = false)
        {
            Vector3 targetDirection = target - this.transform.position;
            float y = targetDirection.y;
            targetDirection.y = 0f;
            float x = targetDirection.magnitude;
            float gravity = Physics.gravity.y * -1;
            float speedSqr = ProjectileSettings.speed;
            float underTheSqrRoot = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 * y * speedSqr);
            if (underTheSqrRoot >= 0f)
            {
                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = speedSqr + root;
                float lowAngle = speedSqr - root;
                if (low)
                {
                    return Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg;
                }
                else
                {
                    return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
                }
            }
            else
            {
                return null;
            }
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