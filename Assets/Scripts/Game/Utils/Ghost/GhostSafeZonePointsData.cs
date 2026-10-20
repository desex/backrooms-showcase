using System;
using UnityEngine;

namespace Game.Utils.Ghost
{
    [Serializable]
    public class GhostSafeZonePointsData
    {
        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;

        public void IsValid(string name)
        {
            // Более показать не могу
        }
        
        public Vector3 LeftPoint => _leftPoint.position;
        public Vector3 RightPoint => _rightPoint.position;
    }
}