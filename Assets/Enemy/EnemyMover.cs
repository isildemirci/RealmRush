using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Tiles;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyRoot))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] [Range(0f,5f)] private float speed;
        
        private List<Node> _path = new List<Node>();

        private EnemyRoot _enemyRoot;
        private GridManager _gridManager;
        private Pathfinder _pathfinder;
        
        void OnEnable()
        {
            ReturnToStart();
            RecalculatePath(true);
        }

        private void Awake()
        {
            _enemyRoot = GetComponent<EnemyRoot>();
            _gridManager = FindObjectOfType<GridManager>();
            _pathfinder = FindObjectOfType<Pathfinder>();
        }

        
        void RecalculatePath(bool resetPath)
        {
            Vector2Int coordinates = new Vector2Int();
            
            if (resetPath)
            {
                coordinates = _pathfinder.StartCoordinates;
            }
            else
            {
                coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            }
            
            StopAllCoroutines();
            _path.Clear();
            _path = _pathfinder.GetNewPath(coordinates);
            StartCoroutine(FollowPath());
        }
        

        void ReturnToStart()
        {
            transform.position = _gridManager.GetPositionFromCoordinates(_pathfinder.StartCoordinates);
        }

        void FinishPath()
        {
            _enemyRoot.StealGold();
            gameObject.SetActive(false);
        }

        IEnumerator FollowPath()
        {
            for (int i = 1 ; i < _path.Count; i++)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = _gridManager.GetPositionFromCoordinates(_path[i].coordinates);
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
