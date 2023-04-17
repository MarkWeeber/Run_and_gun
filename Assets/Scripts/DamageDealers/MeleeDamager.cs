using UnityEngine;

namespace RunAndGun.Space
{
    public class MeleeDamager : MonoBehaviour
    {
        [SerializeField] private float damageDealSphereRadius = 1f;
        [SerializeField] private float damageDealValue = 5f;
        [SerializeField] private LayerMask targetMask = 0;

        private void Awake()
        {
            GameManager.OnPlayerHealthPointsAdded += OnPlayerHealthPointsAdded;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerHealthPointsAdded -= OnPlayerHealthPointsAdded;
        }

        public void InstantiateMeleeDamage()
        {
            if (Physics.OverlapSphere(this.transform.position, damageDealSphereRadius, targetMask).Length > 1)
            {
                GameManager.PlayerHealthPointsAdded(-damageDealValue);
                GameManager.UpdateHealthPoints();
            }
        }

        private void OnPlayerHealthPointsAdded(float value)
        {

        }

    }
}