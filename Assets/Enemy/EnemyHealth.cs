using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyRoot))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHitPoints = 5;
        
        [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
        [SerializeField] private int difficultyRamp = 1;

        private int _currentHitPoints = 5;
        private EnemyRoot _enemyRoot;

        void OnEnable()
        {
            _currentHitPoints = maxHitPoints;
        }
        
        private void Start()
        {
            _enemyRoot = GetComponent<EnemyRoot>();
        }
        
        private void OnParticleCollision(GameObject other)
        {
            ProcessHit();
        }

        void ProcessHit()
        {
            _currentHitPoints--;

            if (_currentHitPoints <= 0)
            {
                gameObject.SetActive(false);
                maxHitPoints += difficultyRamp;
                _enemyRoot.RewardGold();
            }
        }
    }
}
