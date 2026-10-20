using System.Collections.Generic;
using Ecs.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Effects.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SpeedEffectParameters), fileName = nameof(SpeedEffectParameters))]
    public class SpeedEffectParameters : SerializedScriptableObject, ISpeedEffectParameters
    {
        [field: SerializeField] public float DefaultFov { get; private set; }
        [field: SerializeField] public float TargetFov { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Color TargetColor { get; private set; }
        [field: SerializeField] public Color DefaultColor { get; private set; }
        [field: OdinSerialize] public Dictionary<ESpeedEffectAxis, SpeedShaderParameters> SpeedShaderParameters { get; private set; }
    }
}