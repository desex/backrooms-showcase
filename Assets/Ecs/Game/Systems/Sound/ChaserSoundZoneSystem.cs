using System;
using Db.ChasePlayer;
using Ecs.Game.Components.Chaser;
using Ecs.Utils;
using Game.Services.Sound.Chaser;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;
using UnityEngine;

namespace Ecs.Game.Systems.Sound
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 755, nameof(EFeatures.Sound))]
    public class ChaserSoundZoneSystem : ComponentReplacedReactiveSystem<GameEntity, DistanceToPlayerComponent, float>
    {
        private readonly IChasePlayerParameters _chasePlayerParameters;
        private readonly IChaserSoundZoneService _chaserSoundZoneService;

        public ChaserSoundZoneSystem(
            IChaserSoundZoneService chaserSoundZoneService,
            IChasePlayerParameters chasePlayerParameters,
            GameContext game
        ) : base(game)
        {
            _chaserSoundZoneService = chaserSoundZoneService;
            _chasePlayerParameters = chasePlayerParameters;
        }

        protected override Type[] GetComponentTypes()
        {
            // Более показать не могу
            return global::System.Array.Empty<Type>();
        }

        protected override float GetValue(DistanceToPlayerComponent component)
        {
            // Более показать не могу
            return default;
        }

        protected override void OnReplaced(GameEntity entity, float last, float next)
        {
            // Более показать не могу
        }
    }
}