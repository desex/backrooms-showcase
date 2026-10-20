using System.Collections.Generic;
using Ecs.Utils;

namespace Db.Effects
{
    public interface IUiEffectsParameters
    {
        BloodEffectVo BloodEffect { get; }
        
        IReadOnlyDictionary<ESpeedEffectAxis, SpeedEffectVo> SpeedEffects { get; }
    }
}