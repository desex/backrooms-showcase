using System;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    public class SequenceObstacleForWalls
    {
        [field: SerializeField, PreviewField(60, ObjectFieldAlignment.Left)]
        [field: ValidateInput("@SequenceUtils.ValidateSequenceObstacleForWalls(this)")]
        public GameObject ObstaclePrefab { get; set; }
        [field: SerializeField] public EObstacleType ObstacleType { get; set; }

        public void Reset()
        {
            ObstacleType = EObstacleType.None;
            ObstaclePrefab = null;
        }
    }
}