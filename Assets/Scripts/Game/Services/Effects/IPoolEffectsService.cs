using Game.Utils.Obstacles;
using UnityEngine;

namespace Game.Services.Effects
{
    public interface IPoolEffectsService
    {
        DestructableEffect GetEffect(EObstacleEffectType effectType, Vector3 position, Quaternion rotation);
        void ReturnEffect(EObstacleEffectType effectType, DestructableEffect gameObject);
    }
}