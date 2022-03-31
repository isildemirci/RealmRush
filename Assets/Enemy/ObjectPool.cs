using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] [Range(0, 50)]private int poolSize;
        [SerializeField] [Range(0.1f, 30f)] private float spawnTimer;

        private GameObject[] _pool;

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
            _pool = new GameObject[poolSize];

            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(enemyPrefab, transform);
                _pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                if (_pool[i].activeInHierarchy == false)
                {
                    _pool[i].SetActive(true);
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
