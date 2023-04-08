using UnityEngine;

namespace Run_n_gun.Space
{
    [RequireComponent(typeof(SphereCollider))]
    public class SimpleBullet : MonoBehaviour
    {
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstructionMask;
        [SerializeField] private float targetDealDamage = 15f;
        public float TargetDealDamage { get { return targetDealDamage; } set { targetDealDamage = value; } }
        private Rigidbody rbody;
        private SphereCollider sphereCollider;
        private TrailRenderer trailRenderer;
        private bool activeBullet = false;
        private void Start()
        {
            rbody = GetComponent<Rigidbody>();
            trailRenderer = GetComponent<TrailRenderer>();
            sphereCollider = GetComponent<SphereCollider>();
            Disappear();
        }

        public void SendBullet(Vector3 direction, float speed)
        {
            Appear();
            rbody.velocity = direction * speed;
        }

        private void Appear()
        {
            rbody.velocity = Vector3.zero;
            trailRenderer.enabled = true;
            activeBullet = true;
        }

        private void Disappear()
        {
            rbody.velocity = Vector3.zero;
            trailRenderer.enabled = false;
            activeBullet = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(activeBullet)
            {
                if ((targetMask.value & (1 << other.transform.gameObject.layer)) > 0)
                {
                    Disappear();
                }
                if ((obstructionMask.value & (1 << other.transform.gameObject.layer)) > 0)
                {
                    Disappear();
                }
            }
        }
    }
}