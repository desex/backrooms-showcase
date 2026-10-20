using System.Collections.Generic;
using Game.Utils.Rebound;
using PdUtils.Interfaces;
using R3;
using UnityEngine;

namespace Game.Utils.Collisions.Impl
{
    public class CollisionService : ICollisionService, IUiInitializable
    {
        private readonly Dictionary<int, ReboundSetup> _reboundSetups = new Dictionary<int, ReboundSetup>();
        private readonly ReactiveProperty<bool> _isPlayerCollisionEnabled = new ReactiveProperty<bool>(true);

        public ReadOnlyReactiveProperty<bool> IsPlayerCollisionEnabled => _isPlayerCollisionEnabled;

        public void Initialize()
        {
            EnablePlayerCollisions();
        }

        public void EnablePlayerCollisions()
        {
            if (_isPlayerCollisionEnabled.Value)
                return;

            Physics.IgnoreLayerCollision(Layers.PlayerLayer, Layers.ObstacleLayer, false);
            Physics.IgnoreLayerCollision(Layers.PlayerTriggerLayer, Layers.ObstacleLayer, false);
            _isPlayerCollisionEnabled.Value = true;
        }

        public void DisablePlayerCollisions()
        {
            if (!_isPlayerCollisionEnabled.Value)
                return;

            Physics.IgnoreLayerCollision(Layers.PlayerLayer, Layers.ObstacleLayer, true);
            Physics.IgnoreLayerCollision(Layers.PlayerTriggerLayer, Layers.ObstacleLayer, true);
            _isPlayerCollisionEnabled.Value = false;
        }

        public void Register(int hashCode, ReboundSetup reboundSetup)
        {
            _reboundSetups.TryAdd(hashCode, reboundSetup);
        }

        public bool TryGet(int hashCode, out ReboundSetup reboundSetup)
        {
            return _reboundSetups.TryGetValue(hashCode, out reboundSetup);
        }
    }
}