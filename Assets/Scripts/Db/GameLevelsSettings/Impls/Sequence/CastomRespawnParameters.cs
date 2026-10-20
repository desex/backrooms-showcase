using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    public struct CustomRespawnParameters
    {
        public bool IsSetUpRespawn;
        
        [ShowIf(nameof(IsSetUpRespawn))]
        public Vector3 Position;
    }
}