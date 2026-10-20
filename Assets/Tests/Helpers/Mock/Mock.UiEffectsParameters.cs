using System.Collections.Generic;
using Db.Effects;
using Ecs.Utils;

namespace Tests.Helpers.Mock
{
    public static partial class Mock
    {
        public class UiEffectsParameters : IUiEffectsParameters
        {
            public BloodEffectVo BloodEffect { get; set; } = new()
            {
                AppearDuration = 0.5f,
                BlinkMinAlpha = 0.5f,
                BlinkFadeDurationToMinAlpha = 0.75f,
                BlinkMaxAlpha = 1f,
                BlinkFadeDurationToMaxAlpha = 0.3f,
                NoBlinkingDuration = 4f,
                DisappearDuration = 0.9f,
            };

            public IReadOnlyDictionary<ESpeedEffectAxis, SpeedEffectVo> SpeedEffects { get; }

            public SpeedEffectVo SpeedEffect { get; set; } = new()
            {
                AppearDuration = 0.5f,
                MaxAlpha = 1f,
            };
        }
    }
}