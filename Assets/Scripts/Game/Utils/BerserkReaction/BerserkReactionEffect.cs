using System;
using Game.Utils.Obstacles;

namespace Game.Utils.BerserkReaction
{
    [Serializable]
    public struct BerserkReactionEffect
    {
        public EObstacleEffectType EffectType;
        public float DelayBefore;
    }
}