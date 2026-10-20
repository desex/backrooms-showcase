using DG.Tweening;
using Ecs.Action.Commands.Movement.ObstaclePass;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Game.Services.Player;
using Game.Services.Render;
using Game.Services.Revive;
using Game.Services.Score;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Movement.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 270, nameof(EFeatures.Move))]
    public class TryDashWhenInObstacleSystem : ForEachCommandUpdateSystem<TryDashWhenInObstacleCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IReviveObstacleService _reviveObstacleService;
        private readonly IScoreService _scoreService;
        private readonly ISpeedEffectService _speedEffectService;

        public TryDashWhenInObstacleSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IScoreService scoreService,
            IPlayerMovementTypeService playerMovementTypeService,
            IReviveObstacleService reviveObstacleService,
            ISpeedEffectService speedEffectService
        ) : base(commandBuffer)
        {
            _commandBuffer = commandBuffer;
            _game = game;
            _scoreService = scoreService;
            _playerMovementTypeService = playerMovementTypeService;
            _reviveObstacleService = reviveObstacleService;
            _speedEffectService = speedEffectService;
        }

        protected override void Execute(ref TryDashWhenInObstacleCommand command)
        {
            var character = _game.GetEntityWithUid(command.CharacterUid);

            if (!character.HasReadyToObstaclePass)
                return;

            if (character.HasQteType)
                return;

            if (!character.IsCanDash)
                return;

            if (_reviveObstacleService.CanDashFromHoleObstacle(character))
                return;

            var passObstacleType = command.PassObstacleType;
            character.ReplaceChosenPassObstacleType(passObstacleType);

            character.IsCanDash = false;

            if (character.HasSlideDuration)
                character.RemoveSlideDuration();

            var obstaclePassType = character.ReadyToObstaclePass.Value.PassType;

            if (character.IsGrounded)
                UpdateBodyTypeOnGround(obstaclePassType, passObstacleType, character);
            else
                UpdateBodyTypeInAir(obstaclePassType, passObstacleType, character);

            if (!CanDash(obstaclePassType, character))
                return;

            PrepareToDash(character, obstaclePassType);
            var playerTransform = character.Transform.Value;
            var dashData = character.DashWhenInObstacle.Value;

            var playerPosition = playerTransform.position;
            var dashDestinationPosition = dashData.DashEndPosition;

            var endDashToPlayerVector = playerPosition - dashDestinationPosition;
            var expectedDashDirectionVector = dashDestinationPosition - dashData.CenterStartReadyTriggerPosition;

            var isPlayerAheadDashDestinationPoint = Vector3.Dot(endDashToPlayerVector, expectedDashDirectionVector) > 0;

            var dashSideOffset = CalculateDashSideOffset(endDashToPlayerVector, expectedDashDirectionVector);

            dashDestinationPosition += dashSideOffset;

            var dashDuration = dashData.DashDuration;

            var dashDurationRatio = CalculateDashDurationRatio(
                dashData,
                dashDestinationPosition,
                playerPosition,
                expectedDashDirectionVector
            );

            dashDuration *= dashDurationRatio;

            if (character.IsReadyForFastActionMove)
                dashDuration *= dashData.ComboMoveMultiplier;

            if (character.IsPlayer)
                _scoreService.HandleSuccessAction(
                    character.IsReadyForFastActionMove
                        ? EScoreActionType.FastZoneAction
                        : EScoreActionType.NormalZoneAction,
                    command.CharacterUid
                );

            character.ReplaceObstaclePassReact(character.IsReadyForFastActionMove);

            var isDashEnabled = character.ReadyToObstaclePass.Value.IsDashEnabled;
            _commandBuffer.CancelReviveCameraFocus();

            if (isDashEnabled && !isPlayerAheadDashDestinationPoint)
            {
                playerTransform.DOMove(dashDestinationPosition, dashDuration).SetEase(dashData.DashEase)
                    .OnComplete(() => StartObstaclePass(character));

                return;
            }

            StartObstaclePass(character);
        }

        private static bool CanDash(EObstaclePassType obstaclePassType, GameEntity character)
        {
            switch (obstaclePassType)
            {
                case EObstaclePassType.Swing:
                case EObstaclePassType.Destructible:
                case EObstaclePassType.HoleComeback:
                case EObstaclePassType.MonkeyJump:
                case EObstaclePassType.JumpAndSlide:
                case EObstaclePassType.JumpInWallWithHole:
                    return true;
            }

            return character.IsGrounded;
        }

        private void UpdateBodyTypeInAir(
            EObstaclePassType obstaclePassType,
            EPassObstacleType passObstacleType,
            GameEntity character
        )
        {
            _speedEffectService.SetSpeedEffectAxis(passObstacleType);

            switch (obstaclePassType, passObstacleType)
            {
                case (EObstaclePassType.Swing, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.Swing);
                    break;

                case (EObstaclePassType.MonkeyJump, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.MonkeyJump);
                    break;
                
                case (EObstaclePassType.JumpInWallWithHole, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.ForwardJump);
                    break;
                    
                case (EObstaclePassType.JumpAndSlide, EPassObstacleType.Up):
                    character.ReplaceBodyMoveType(EBodyMoveType.Climb);
                    character.IsClimbing = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;

                case (EObstaclePassType.HoleComeback, EPassObstacleType.Up):
                    character.IsDashInHole = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;

                case (EObstaclePassType.Climb, EPassObstacleType.Up):
                    character.ReplaceBodyMoveType(EBodyMoveType.Climb);
                    character.IsClimbing = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;

                case (EObstaclePassType.JumpAndSlide, EPassObstacleType.Down):
                    _commandBuffer.InterruptFall(character.Uid.Value);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.JumpInObstacleCut);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.Down):
                    if (character.HasJumping)
                        character.RemoveJumping();

                    character.ReplaceSliding(ESlideType.Short);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.NoReact):
                    if (character.HasJumping)
                        character.RemoveJumping();

                    _game.PlayerEntity.IsHitOnObstacle = true;
                    _game.PlayerEntity.ReplaceBodyMoveType(EBodyMoveType.HitOnObstacle);
                    break;
            }
        }

        private void UpdateBodyTypeOnGround(
            EObstaclePassType obstaclePassType,
            EPassObstacleType passObstacleType,
            GameEntity character
        )
        {
            _speedEffectService.SetSpeedEffectAxis(passObstacleType);

            switch (obstaclePassType, passObstacleType)
            {
                case (EObstaclePassType.JumpAndSlide, EPassObstacleType.Down):
                    character.ReplaceSliding(ESlideType.Short);
                    break;

                case (EObstaclePassType.JumpAndSlide, EPassObstacleType.Up):
                    character.ReplaceBodyMoveType(EBodyMoveType.Climb);
                    character.IsClimbing = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;

                case (EObstaclePassType.JumpInWallWithHole, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.ForwardJump);
                    break;

                case (EObstaclePassType.LongSlide, EPassObstacleType.Down):
                    character.ReplaceSliding(ESlideType.Short);
                    break;

                case (EObstaclePassType.MonkeyJump, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.MonkeyJump);
                    break;

                case (EObstaclePassType.Swing, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.Swing);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.Up):
                    character.ReplaceJumping(EJumpType.JumpInObstacle);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.Down):
                    character.ReplaceSliding(ESlideType.Short);
                    break;

                case (EObstaclePassType.Destructible, EPassObstacleType.NoReact):
                    character.IsHitOnObstacle = true;
                    character.ReplaceBodyMoveType(EBodyMoveType.HitOnObstacle);
                    break;

                case (EObstaclePassType.HoleComeback, EPassObstacleType.Up):
                    character.IsDashInHole = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;

                case (EObstaclePassType.Climb, EPassObstacleType.Up):
                    character.ReplaceBodyMoveType(EBodyMoveType.Climb);
                    character.IsClimbing = true;
                    _playerMovementTypeService.UpdateBodyType(character.Uid.Value);
                    break;
            }
        }

        private void StartObstaclePass(GameEntity player)
        {
            _commandBuffer.TryStartObstaclePass(player.Uid.Value, player.ReadyToObstaclePass.Value.Uid);
        }

        private static void PrepareToDash(GameEntity player, EObstaclePassType passObstacleType)
        {
            if (passObstacleType != EObstaclePassType.LongSlide && passObstacleType != EObstaclePassType.JumpAndSlide)
                player.ReplaceVerticalSpeed(0f);

            player.IsUseGravity = false;
            player.IsCanSwipe = false;
            player.IsCanBeDamaged = false;
            player.CharacterController.Value.enabled = false;
            player.IsEnableCharacterTrigger = false;
        }

        private static Vector3 CalculateDashSideOffset(Vector3 endDashToPlayerVector, Vector3 dashDirectionVector)
        {
            var projectDashVector = Vector3.Project(endDashToPlayerVector, dashDirectionVector);
            var offset = endDashToPlayerVector - projectDashVector;
            offset.y = 0;

            return offset;
        }

        private static float CalculateDashDurationRatio(
            DashData dashData,
            Vector3 actualDashDestinationPosition,
            Vector3 playerPosition,
            Vector3 expectedDashDirectionVector
        )
        {
            var rawEndDashPosition = dashData.DashEndPosition;
            var maxDashVector = rawEndDashPosition - dashData.PossibleStartDashPosition;
            maxDashVector = Vector3.Project(maxDashVector, expectedDashDirectionVector);
            maxDashVector.y = 0;

            var actualDashVector = actualDashDestinationPosition - playerPosition;
            actualDashVector.y = 0;

            var actualDashDistanceSqr = actualDashVector.sqrMagnitude;
            var maxDashVectorSqr = maxDashVector.sqrMagnitude;

            var actualRatio = Mathf.Sqrt(actualDashDistanceSqr / maxDashVectorSqr);

            return Mathf.Clamp01(actualRatio);
        }
    }
}