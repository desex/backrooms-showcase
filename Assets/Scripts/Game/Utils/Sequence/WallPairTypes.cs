using System;
using Db.GameLevelsSettings.Impls;
using Db.GameLevelsSettings.Impls.Sequence;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Utils.Sequence
{
    [Serializable]
    public class WallPairTypes
    {
        public SequenceWall LeftWall;
        public SequenceWall RightWall;
        
        [Space]
        public bool IsReplaceWallsWithObstacles;

        [field: SerializeField]
        [field: ShowIf(nameof(IsReplaceWallsWithObstacles))]
        [field: Title("Replace obstacles:")]
        [field: ListDrawerSettings(ShowIndexLabels = true, HideAddButton = false, HideRemoveButton = false)]
        public SequenceObstacleForWalls ReplaceObstacle { get; set; }
    }
}