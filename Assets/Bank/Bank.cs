using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bank
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] private int startingBalance = 150;
        [SerializeField] private int currentBalance;
        public int CurrentBalance => currentBalance;

        [SerializeField] private TextMeshProUGUI displayBalance;
    
        private void Awake()
        {
            currentBalance = startingBalance;
            UpdateDisplay();
        }

        public void Deposit(int amount)
        {
            currentBalance += Mathf.Abs(amount);
            UpdateDisplay(); 
        }

        public void Withdraw(int amount)
        {
            currentBalance -= Mathf.Abs(amount);
            UpdateDisplay();
        
            if (currentBalance < 0)
            {
                ReloadScene();
            }
        }

        void UpdateDisplay()
        {
            displayBalance.text = "Gold: " + currentBalance;
        }

        void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}
