using System;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    public class SequenceObstacle
    {
        [field: SerializeField, PreviewField(60, ObjectFieldAlignment.Left)]
        [field: ValidateInput("@SequenceUtils.ValidateSequenceObstacle(this)")]
        public GameObject ObstaclePrefab { get; set; }

        [field: SerializeField] public EObstacleType ObstacleType { get; set; }
        [field: SerializeField] public SequencePoint PointsSettings { get; set; }
        
        [field: Space]
        [field: SerializeField] 
        public CustomRespawnParameters RespawnParameters { get; set; }

        public void Reset()
        {
            ObstacleType = EObstacleType.None;
            ObstaclePrefab = null;
        }
    }
}