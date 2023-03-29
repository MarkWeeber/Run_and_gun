using UnityEngine;

namespace Run_n_gun.Space
{
    public class EnemyHealth : MonoBehaviour
    {

        [SerializeField] private float startingHealth = 100f;
        [SerializeField] private float currentHealth = 100f;
        [SerializeField] private LayerMask hitDetectFromPlayerLayer = 0;
        [SerializeField] private Transform heathBarLocationTransform;
        private void Start()
        {

        }


        private void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                GameManager.enemyHealthBarLocationTransform = heathBarLocationTransform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((hitDetectFromPlayerLayer.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                if(GameManager.enemyHealthBarLocationTransform == heathBarLocationTransform)
                {
                    GameManager.enemyHealthBarLocationTransform = null;
                }
            }
        }
    }
}