using System;
using System.Collections;
using UnityEngine;

namespace Tower
{
    public class TowerRoot : MonoBehaviour
    {
        [SerializeField] private int cost = 75;
        [SerializeField] private float buildDelay = 1f;

        private void Start()
        {
            StartCoroutine(Build());
        }

        public bool CreateTower(TowerRoot tower, Vector3 position)
        {
            Bank.Bank bank = FindObjectOfType<Bank.Bank>();

            if (bank == null)
            {
                return false;
            }

            if (bank.CurrentBalance >= cost)
            {
                Instantiate(tower.gameObject, position, Quaternion.identity);
                bank.Withdraw(cost);
                return true;
            }

            return false;
        }

        IEnumerator Build()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                foreach (Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(false);
                }
            }
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(buildDelay);
                foreach (Transform grandchild in child)
                {
                    grandchild.gameObject.SetActive(true);
                }
            }
        }
    }
}
