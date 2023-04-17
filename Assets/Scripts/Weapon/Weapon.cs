using UnityEngine;

namespace RunAndGun.Space
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private int ammoCapacity = 15;
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private bool infiniteAmmo = false;
        [SerializeField] private float bulletSpeed = 300f;
        [SerializeField] private float fireRateInSeconds = 0.2f;
        [SerializeField] private float reloadSpeed = 1f;
        private RecoilControl recoilControl;
        private Transform[] ammo;
        private SimpleBullet[] bulletRefernce;
        private int ammoLeft;
        private int ammoIndex;
        private float fireRateTimer = 0f;
        private float reloadTimer = 0f;
        private bool reloading = false;

        private void Awake()
        {
            ammoLeft = ammoCapacity;
            GameManager.GamePoints.CurrentAmmoCount = ammoLeft;
        }

        private void Start()
        {
            GameManager.weapon = this;
            recoilControl = GameManager.recoilControl;
            InstantiateAmmoCapacity();
        }

        private void Update()
        {
            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
            if (reloadTimer > 0)
            {
                reloadTimer -= Time.deltaTime;
                if(reloadTimer < 0)
                {
                    ReloadWeaponEnd();
                }
            }
        }

        private void InstantiateAmmoCapacity()
        {
            ammo = new Transform[ammoCapacity];
            bulletRefernce = new SimpleBullet[ammoCapacity];
            ammoIndex = 0;
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
            if (ammoLeft > 0 && !reloading )
            {
                Shoot();
            }
            else
            {
                ReloadWeaponStart();
            }
        }

        private void Shoot()
        {
            if (fireRateTimer <= 0)
            {
                fireRateTimer = fireRateInSeconds;
                ammoLeft--;
                ammo[ammoIndex].position = firePoint.position;
                bulletRefernce[ammoIndex].SendBullet(firePoint.transform.forward, bulletSpeed);
                ammoIndex++;
                if(ammoIndex > ammoCapacity - 1)
                {
                    ammoIndex = 0;
                }
                recoilControl.CallRecoil();
                GameManager.GamePoints.CurrentAmmoCount = ammoLeft;
                GameManager.UpdateAmmo();
            }
        }

        public void ReloadWeaponStart()
        {
            if(!reloading && ammoLeft != ammoCapacity)
            {
                reloadTimer = reloadSpeed;
                reloading = true;
                GameManager.ReloadWeaponStart();
            }
        }

        private void ReloadWeaponEnd()
        {
            reloadTimer = 0f;
            ammoLeft = ammoCapacity;
            reloading = false;
            GameManager.GamePoints.CurrentAmmoCount = ammoLeft;
            GameManager.UpdateAmmo();
            GameManager.ReloadWeaponEnd();
        }
    }
}