using UnityEngine;

namespace Enemy
{
    public class EnemyRoot : MonoBehaviour
    {
        [SerializeField] private int goldReward = 25;
        [SerializeField] private int goldPenalty = 25;

        private Bank.Bank _bank;
    
        void Start()
        {
            _bank = FindObjectOfType<Bank.Bank>();
        }

        public void RewardGold()
        {
            if(_bank == null) return;
            _bank.Deposit(goldReward);
        }

        public void StealGold()
        {
            if (_bank == null) return;
            _bank.Withdraw(goldPenalty);
        }

    }
}
