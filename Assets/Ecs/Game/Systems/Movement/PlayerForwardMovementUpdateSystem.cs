using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.Player;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 900, nameof(EFeatures.Move))]
    public sealed class PlayerForwardMovementUpdateSystem : APausableCharacterGameStateConditionUpdateSystem
    {
        private readonly IPlayerSpeedService _playerSpeedService;

        public PlayerForwardMovementUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            IPlayerSpeedService playerSpeedService
        ) : base(game, gameGroupUtils)
        {
            _playerSpeedService = playerSpeedService;
        }

        protected override bool GameStateCondition(EGameState gameState)
        {
            return gameState == EGameState.Game;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return !entity.IsDestroyed;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            if (entity.IsForwardSpeedOverriden)
                return;

            var speed = _playerSpeedService.GetForwardSpeed(entity);

            var currentForwardSpeed = entity.HasForwardSpeed ? entity.ForwardSpeed.Value.Current : 0f;
            entity.ReplaceForwardSpeed(new ForwardSpeed(speed, currentForwardSpeed));
        }
    }
}