using UnityEngine;

namespace RunAndGun.Space
{
    public class WeaponReload_UI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer crossHairSprite;
        [SerializeField] private SpriteRenderer reloadingSprite;

        private void Awake()
        {
            GameManager.OnPlayerWeaponReloadStart += ReloadWeaponStart;
            GameManager.OnPlayerWeaponReloadEnd += ReloadWeaponEnd;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerWeaponReloadStart -= ReloadWeaponStart;
            GameManager.OnPlayerWeaponReloadEnd -= ReloadWeaponEnd;
        }

        private void ReloadWeaponStart()
        {
            crossHairSprite.enabled = false;
            reloadingSprite.enabled = true;
        }

        private void ReloadWeaponEnd()
        {
            crossHairSprite.enabled = true;
            reloadingSprite.enabled = false;
        }
    }
}