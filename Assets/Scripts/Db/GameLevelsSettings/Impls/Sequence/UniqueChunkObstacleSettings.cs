using System;
using Game.Utils.Obstacles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    public struct UniqueChunkObstacleSettings
    {
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [SerializeField]
        public GameObject ObstaclePrefab;

        public Vector3 Offset;
        public Vector3 Rotation;
        public EObstacleType ObstacleType;
        
        [Space]
        [LabelText("Player respawn point relative to current obstacle")]
        public CustomRespawnParameters RespawnParameters;
    }
}