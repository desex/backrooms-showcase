using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Utils;
using Ecs.Utils.Groups;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 920, nameof(EFeatures.Move))]
    public class PlayerGravityUpdateSystem : APausableCharacterGameStateConditionUpdateSystem
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParameters;

        public PlayerGravityUpdateSystem(
            IGameGroupUtils gameGroupUtils, 
            ITimeProvider timeProvider, 
            GameContext game,
            IPlayerBasicMovementParametersBase playerBasicMovementParameters
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _playerBasicMovementParameters = playerBasicMovementParameters;
        }

        protected override bool GameStateCondition(EGameState gameState) => gameState is 
            EGameState.Game or EGameState.RevivingChoice;

        protected override bool EntityFilter(GameEntity entity)
            => entity.IsUseGravity && (!entity.IsDead || !entity.IsGrounded);

        protected override void HandleUpdate(GameEntity entity)
        {
            var gravityForce = 0f;

            if (entity.IsGravityDisabled)
                return;
            
            if (entity.IsUseExtraGravity)
            {
                gravityForce = _playerBasicMovementParameters.ExtraGravityForce;
                entity.ReplaceVerticalSpeed(gravityForce);
                return;
            }

            if (entity.IsGrounded)
                return;
            
            if (entity.IsInWater && entity.HasInWaterVerticalSpeed)
            {
                gravityForce = entity.InWaterVerticalSpeed.Value;
                entity.ReplaceVerticalSpeed(gravityForce);
                return;
            }

            gravityForce = _playerBasicMovementParameters.GravityForce;

            var verticalSpeed = entity.HasVerticalSpeed 
                ? entity.VerticalSpeed.Value 
                : 0f;

            verticalSpeed += gravityForce * _timeProvider.DeltaTime;

            entity.ReplaceVerticalSpeed(verticalSpeed);
        }
    }
}