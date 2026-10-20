using System;

namespace Db.Effects
{
    [Serializable]
    public class BloodEffectVo
    {
        public float AppearDuration = 0.5f;

        public float BlinkMinAlpha = 0.5f;
        public float BlinkFadeDurationToMinAlpha = 0.7f;

        public float BlinkMaxAlpha = 1f;
        public float BlinkFadeDurationToMaxAlpha = 0.3f;

        public float NoBlinkingDuration = 4;

        public float DisappearDuration = 0.6f;
    }
}