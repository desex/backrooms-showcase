using System.Collections.Generic;
using Ecs.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Db.Effects.Impl
{
    [CreateAssetMenu(menuName = "Settings/UI/" + nameof(UiEffectsParametersBase), fileName = nameof(UiEffectsParametersBase))]
    public class UiEffectsParametersBase : SerializedScriptableObject, IUiEffectsParameters
    {
        [field: SerializeField] public BloodEffectVo BloodEffect { get; private set; }
       
        [OdinSerialize] private Dictionary<ESpeedEffectAxis, SpeedEffectVo> _speedEffects;
        public IReadOnlyDictionary<ESpeedEffectAxis, SpeedEffectVo> SpeedEffects => _speedEffects;
    }
}