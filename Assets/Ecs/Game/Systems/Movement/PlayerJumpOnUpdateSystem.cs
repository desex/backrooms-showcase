using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 910, nameof(EFeatures.Move))]
    public class PlayerJumpOnUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly IPlayerJumpOnParametersBase _jumpOnParametersBase;
        private readonly IPlayerBasicMovementParametersBase _parameterBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ITimeProvider _timeProvider;

        public PlayerJumpOnUpdateSystem(
            GameContext game,
            ITimeProvider timeProvider,
            IPlayerBasicMovementParametersBase parameterBase,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerJumpOnParametersBase jumpOnParametersBase
        ) : base(game)
        {
            _timeProvider = timeProvider;
            _parameterBase = parameterBase;
            _playerMovementTypeService = playerMovementTypeService;
            _jumpOnParametersBase = jumpOnParametersBase;
        }

        protected override void UpdateSystem()
        {
            var player = Game.PlayerEntity;

            if (!player.HasJumpOnMove)
                return;

            var jumpOnParameters = player.JumpOnMove.Value;
            var playerStartPosition = jumpOnParameters.PlayerStartPosition;
            var endPoint = jumpOnParameters.JumpEndPoint;

            var jumpDuration = player.IsFastActionMove
                ? _jumpOnParametersBase.JumpDuration / _parameterBase.FastMoveRatio
                : _jumpOnParametersBase.JumpDuration;

            var curve = _jumpOnParametersBase.JumpCurve;

            var time = player.HasJumpOnDuration ? player.JumpOnDuration.Value : 0;
            var timeNormalized = time / jumpDuration;

            var yPos = curve.Evaluate(timeNormalized) * (endPoint.y - playerStartPosition.y);
            var playerPosition = playerStartPosition + (endPoint - playerStartPosition) * timeNormalized;
            playerPosition.y = playerStartPosition.y + yPos;

            if (time >= jumpDuration)
            {
                player.IsUseGravity = true;
                player.ReplacePosition(jumpOnParameters.JumpEndPoint);
                player.CharacterController.Value.enabled = true;
                player.IsEnableCharacterTrigger = true;

                player.RemoveJumpOnMove();
                player.RemoveJumpOnDuration();
                player.RemoveJumping();
                _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
                return;
            }

            time += _timeProvider.DeltaTime;

            player.ReplacePosition(playerPosition);
            player.ReplaceJumpOnDuration(time);
        }
    }
}