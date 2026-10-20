using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Render;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 850, nameof(EFeatures.Move))]
    public class AccelerationDurationUpdateSystem : APausableCharacterGameStateConditionUpdateSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly ISpeedEffectService _speedEffectService;
        private readonly ITimeProvider _timeProvider;

        public AccelerationDurationUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            ISpeedEffectService speedEffectService,
            ICommandBuffer commandBuffer
        ) : base(game, gameGroupUtils)
        {
            _game = game;
            _timeProvider = timeProvider;
            _speedEffectService = speedEffectService;
            _commandBuffer = commandBuffer;
        }

        protected override bool GameStateCondition(EGameState gameState)
        {
            return gameState == EGameState.Game;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasSpeedAcceleration
                   && !entity.IsDead
                   && !entity.IsDestroyed
                   && !_game.TutorialEntity.IsInTutorial;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            var accelerationDuration = entity.SpeedAcceleration.Duration;

            if (accelerationDuration <= 0)
            {
                if (entity.IsPlayer)
                    _speedEffectService.EnableSpeedEffect();

                _commandBuffer.CancelAcceleration(entity.Uid.Value);
                entity.RemoveSpeedAcceleration();

                return;
            }

            if (entity.HasSpeedInAnimationBoost)
                entity.ReplaceSpeedInAnimationBoost(entity.SpeedInAnimationBoost.Value);

            accelerationDuration -= _timeProvider.DeltaTime;
            entity.ReplaceSpeedAcceleration(accelerationDuration);
        }
    }
}