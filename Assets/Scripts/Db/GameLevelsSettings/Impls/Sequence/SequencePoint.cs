using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    public struct SequencePoint
    {
        [SerializeField] public bool IsNeedOffset;
        
        [field: SerializeField]
        [field: ShowIf(nameof(IsNeedOffset))]
        public Vector3 Offset { get; set; }
        
        [field: SerializeField]
        [field: ShowIf(nameof(IsNeedOffset))]
        public Vector3 Rotation { get; set; }
    }
}