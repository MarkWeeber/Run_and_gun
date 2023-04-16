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
            GameManager.OnPlayerHealthPointsSubtracted += OnPlayerHealthPointsSubtracted;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerHealthPointsSubtracted -= OnPlayerHealthPointsSubtracted;
        }

        public void InstantiateMeleeDamage()
        {
            if (Physics.OverlapSphere(this.transform.position, damageDealSphereRadius, targetMask).Length > 1)
            {
                GameManager.PlayerHealthPointsSubtracted(-damageDealValue);
            }
        }

        private void OnPlayerHealthPointsSubtracted(float value)
        {

        }

    }
}