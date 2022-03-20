using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] [Range(0, 50)]private int poolSize;
        [SerializeField] [Range(0.1f, 30f)] private float spawnTimer;

        private GameObject[] pool;

        private void Awake()
        {
            PopulatePool();
        }

        void Start()
        {
            StartCoroutine(SpawnEnemy());
        }
      

        void PopulatePool()
        {
            pool = new GameObject[poolSize];

            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = Instantiate(enemyPrefab, transform);
                pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    return;
                }
            }
        }

        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(spawnTimer);
            }
        }
    }
}
