using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Player;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 900, nameof(EFeatures.Move))]
    public class ClimbSystem : APausableConditionUpdateSystem
    {
        private readonly IPlayerBasicMovementParametersBase _playerBasicMovementParametersBase;
        private readonly IPlayerClimbParametersBase _playerClimbParametersBase;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ITimeProvider _timeProvider;

        public ClimbSystem(
            GameContext game,
            ITimeProvider timeProvider,
            IPlayerMovementTypeService playerMovementTypeService,
            IPlayerClimbParametersBase playerClimbParametersBase,
            IPlayerBasicMovementParametersBase playerBasicMovementParametersBase
        ) : base(game)
        {
            _timeProvider = timeProvider;
            _playerMovementTypeService = playerMovementTypeService;
            _playerClimbParametersBase = playerClimbParametersBase;
            _playerBasicMovementParametersBase = playerBasicMovementParametersBase;
        }

        protected override bool Filter()
        {
            var player = Game.PlayerEntity;

            return player.HasClimbParameters && player.IsClimbing;
        }

        protected override void UpdateSystem()
        {
            var player = Game.PlayerEntity;

            var animationDuration = _playerClimbParametersBase.AnimationDuration;
            var fastMoveRatio = _playerBasicMovementParametersBase.FastMoveRatio;
            var fullDuration = player.IsFastActionMove ? animationDuration / fastMoveRatio : animationDuration;

            var duration = player.HasMoveAnimationDuration ? player.MoveAnimationDuration.Value : fullDuration;

            var parameters = player.ClimbParameters.Value;
            var startPoint = parameters.StartPoint;
            var endPoint = parameters.EndPoint;
            var high = parameters.High;

            var direction = endPoint - startPoint;
            var durationPercent = 1 - duration / fullDuration;
            var newPlayerPos = startPoint + direction * durationPercent;
            newPlayerPos.y += high * durationPercent;

            player.ReplacePosition(newPlayerPos);
            duration -= _timeProvider.DeltaTime;

            if (duration <= 0)
            {
                player.ReplacePosition(endPoint + Vector3.up * high);
                player.CharacterController.Value.enabled = true;
                player.IsEnableCharacterTrigger = true;
                player.RemoveMoveAnimationDuration();
                player.RemoveClimbParameters();
                CancelClimb(player);

                return;
            }

            player.ReplaceMoveAnimationDuration(duration);
        }

        private void CancelClimb(GameEntity player)
        {
            player.IsUseGravity = true;
            player.IsCanBeDamaged = true;
            player.IsCanSwipe = true;
            player.IsClimbing = false;

            _playerMovementTypeService.UpdateBodyType(player.Uid.Value);
        }
    }
}