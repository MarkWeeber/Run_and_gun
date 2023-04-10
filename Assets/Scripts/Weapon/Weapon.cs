using UnityEngine;

namespace Run_n_gun.Space
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private int ammoCapacity = 15;
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private bool infiniteAmmo = false;
        [SerializeField] private float bulletSpeed = 300f;
        [SerializeField] private float fireRateInSeconds = 0.2f;

        private RecoilControl recoilControl;
        private Transform[] ammo;
        private SimpleBullet[] bulletRefernce;
        private int ammoLeft;
        private float fireRateTimer = 0f;

        private void Start()
        {
            GameManager.weapon = this;
            recoilControl = GameManager.recoilControl;
            InstantiateAmmoCapacity();
            ammoLeft = ammoCapacity;
        }

        private void Update()
        {
            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
        }

        private void InstantiateAmmoCapacity()
        {
            ammo = new Transform[ammoCapacity];
            bulletRefernce = new SimpleBullet[ammoCapacity];
            for (int i = 0; i < ammoCapacity; i++)
            {
                ammo[i] = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                bulletRefernce[i] = ammo[i].GetComponent<SimpleBullet>();
            }
        }

        public void TryShoot()
        {
            if (infiniteAmmo)
            {
                if (ammoLeft <= 0)
                {
                    ammoLeft = ammoCapacity;
                }
            }
            if (ammoLeft > 0)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (fireRateTimer <= 0)
            {
                fireRateTimer = fireRateInSeconds;
                ammoLeft--;
                ammo[ammoLeft].position = firePoint.position;
                bulletRefernce[ammoLeft].SendBullet(firePoint.transform.forward, bulletSpeed);
                recoilControl.CallRecoil();
            }
        }
    }
}