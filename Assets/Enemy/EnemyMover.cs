using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyRoot))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> path = new List<Waypoint>();
        [SerializeField] [Range(0f,5f)] private float speed;

        private EnemyRoot enemyRoot;
        
        void OnEnable()
        {
            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void Start()
        {
            enemyRoot = GetComponent<EnemyRoot>();
        }

        void FindPath()
        {
            path.Clear();
            
            GameObject parent = GameObject.FindGameObjectWithTag("Path");

            foreach (Transform child in parent.transform)
            {
                Waypoint waypoint = child.GetComponent<Waypoint>();
                if (waypoint != null)
                {
                    path.Add(waypoint);   
                }
            }
        }

        void ReturnToStart()
        {
            transform.position = path[0].transform.position;
        }

        void FinishPath()
        {
            enemyRoot.StealGold();
            gameObject.SetActive(false);
        }

        IEnumerator FollowPath()
        {
            foreach (Waypoint waypoint in path)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = waypoint.transform.position;
                float travelPercent = 0f;
                
                transform.LookAt(endPos);

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
            FinishPath();
        }
    }
}
