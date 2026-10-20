using System.Collections.Generic;
using Game.Utils.Chunks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerTurningParametersBase), 
                     fileName = nameof(PlayerTurningParametersBase))]
    public class PlayerTurningParametersBase : SerializedScriptableObject, IPlayerTurningParametersBase
    {
        [field: SerializeField] public float EndTiltTime { get; private set; }
        [field: SerializeField] public float TurningAngle { get; private set; }
        [field: SerializeField] public AnimationCurve TurningCurve { get; private set; }
        
        [field: Title("TurnDuration")]
        [field: SerializeField] public TurnDurationData DefaultTurningDuration { get; private set; } = new();
        [field: OdinSerialize] public Dictionary<EChunkType, TurnDurationData> ChunkDurations { get; private set; } = new();
    }
}