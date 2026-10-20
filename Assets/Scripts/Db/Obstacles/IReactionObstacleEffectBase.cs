using System.Collections.Generic;
using Game.Utils.Obstacles;

namespace Db.Obstacles
{
    public interface IReactionObstacleEffectBase
    {
        float OffsetY { get; }
        float MultiplierZ { get; }
        float MultiplierZForDashMin { get; }
        float MultiplierZForDashMax { get; }
        float IncreaseMultiplierDashMax { get; }
        float PossibleCapPlayerSpeed { get; }
        IReadOnlyDictionary<EObstacleEffectType, DestructableEffectParameters> EffectsList { get; }
    }
}