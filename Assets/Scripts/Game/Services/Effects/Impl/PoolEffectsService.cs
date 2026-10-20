using System.Collections.Generic;
using Db.Obstacles;
using Game.Utils.Obstacles;
using UnityEngine;
using Zenject;

namespace Game.Services.Effects.Impl
{
    public class PoolEffectsService : IInitializable, IPoolEffectsService
    {
        private readonly Dictionary<EObstacleEffectType, Stack<DestructableEffect>> _effectsPool =
            new Dictionary<EObstacleEffectType, Stack<DestructableEffect>>();

        private readonly IReactionObstacleEffectBase _obstacleEffectBase;

        private GameObject _container;

        private PoolEffectsService(IReactionObstacleEffectBase obstacleEffectBase)
        {
            _obstacleEffectBase = obstacleEffectBase;
        }

        public void Initialize()
        {
            // Более показать не могу
        }

        public DestructableEffect GetEffect(EObstacleEffectType effectType, Vector3 position, Quaternion rotation)
        {
            // Более показать не могу
            return default;
        }

        public void ReturnEffect(EObstacleEffectType effectType, DestructableEffect gameObject)
        {
            // Более показать не могу
        }

        private DestructableEffect CreateEffect(EObstacleEffectType effectType)
        {
            // Более показать не могу
            return default;
        }
    }
}