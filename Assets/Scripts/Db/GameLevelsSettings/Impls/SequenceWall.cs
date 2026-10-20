using System;
using Game.Utils.Chunks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls
{
    [Serializable]
    public class SequenceWall
    {
        [field: SerializeField, PreviewField(60, ObjectFieldAlignment.Left)]
        [field: ValidateInput("@SequenceUtils.ValidateSequenceWall(this)")]
        public GameObject WallPrefab { get; set; }

        [field: SerializeField] public EChunkWallType WallType { get; set; }

        public void Reset()
        {
            WallPrefab = null;
            WallType = EChunkWallType.None;
        }
    }
}