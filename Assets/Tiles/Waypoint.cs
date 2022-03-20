using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Tiles
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private TowerRoot towerPrefab;
        [SerializeField] private bool isPlaceable;
        public bool IsPlaceable { get { return isPlaceable; } }
        
        private void OnMouseDown()
        {
            if (isPlaceable)
            {
                bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
                isPlaceable = !isPlaced;
            }
        }
    }
}
