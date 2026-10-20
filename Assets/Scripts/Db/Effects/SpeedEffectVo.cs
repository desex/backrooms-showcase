using System;

namespace Db.Effects
{
    [Serializable]
    public class SpeedEffectVo
    {
        public float AppearDuration = 0.5f;
        public float DisappearDuration = 0.6f;
        public float MaxAlpha = 1f;
        public float DurationTimer = 1f;
    }
}