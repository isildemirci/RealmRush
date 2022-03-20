using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyRoot))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHitPoints = 5;
        [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
        [SerializeField] private int difficultyRamp = 1;
        private int currentHitPoints = 5;

        private EnemyRoot enemyRoot;

        void OnEnable()
        {
            currentHitPoints = maxHitPoints;
        }
        
        private void Start()
        {
            enemyRoot = GetComponent<EnemyRoot>();
        }
        
        private void OnParticleCollision(GameObject other)
        {
            ProcessHit();
        }

        void ProcessHit()
        {
            currentHitPoints--;

            if (currentHitPoints <= 0)
            {
                gameObject.SetActive(false);
                maxHitPoints += difficultyRamp;
                enemyRoot.RewardGold();
            }
        }
    }
}
