using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Run_n_gun.Space
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints { get { return healthPoints; } set { OnHealthPointsAdded(value); } }
        public float StartingHealthPoints;
        [SerializeField] private Animator animator = null;
        public Animator Animator { get { return animator; } set { animator = value; } }

        [SerializeField] private RigBuilder rigBuilder = null;
        public RigBuilder RigBuilder { get { return rigBuilder; } set { rigBuilder = value; } }

        [SerializeField] private PlayerInput playerInput = null;
        public PlayerInput PlayerInput { get { return playerInput; } set { playerInput = value; } }
        [SerializeField] private FlipFaceControl flipFaceControl = null;
        public FlipFaceControl FlipFaceControl { get { return flipFaceControl; } set { flipFaceControl = value; } }

        private bool isDead = false;
        public bool IsDead { get { return isDead; } set { isDead = value; } }

        private void Awake()
        {
            GameManager.OnPlayerDeath += OnPlayerDeath;
            GameManager.OnPlayerHealthPointsAdded += OnHealthPointsAdded;
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerDeath -= OnPlayerDeath;
            GameManager.OnPlayerHealthPointsAdded += OnHealthPointsAdded;
        }

        private void Start()
        {
            StartingHealthPoints = healthPoints;
            GameManager.PlayerStartingHealthPoints = StartingHealthPoints;
            // testing death event
            InvokeRepeating(nameof(PlayDead),1.5f, 1f);
        }

        public void AddHealthPoints(float addHealthPoints)
        {
            GameManager.PlayerHealthPointsAdded(addHealthPoints);
        }

        private void OnHealthPointsAdded(float addedHealthPoints)
        {
            if (!isDead)
            {
                healthPoints += addedHealthPoints;
                if (healthPoints <= 0)
                {
                    GameManager.CallPlayerDeath();
                }
            }
        }

        private void OnPlayerDeath()
        {
            flipFaceControl.enabled = false;// disable flip face control
            foreach (var item in rigBuilder.layers) // disable all rig builder layers to animate death freely
            {
                item.active = false;
            }
            isDead = true;  // mark as dead in current class
        }
        private void PlayDead()
        {
            AddHealthPoints(-25);
        }
    }
}