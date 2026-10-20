using Db.Player;
using Db.SoundFxBase;
using DG.Tweening;
using Ecs.Action.Commands.Movement.ObstaclePass;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.ObstaclePass;
using Game.Services.Player;
using Game.Services.Render;
using Game.Services.Sound;
using Game.Utils.Movement;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Movement.ObstaclePass
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 971, nameof(EFeatures.Move))]
    public class TryStartObstaclePassSystem : ForEachCommandUpdateSystem<TryStartObstaclePassCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly IPlayerTiltingService _playerTiltingService;
        private readonly IPlayerWaterInHoleParametersBase _playerWaterInHoleParameters;
        private readonly ISoundService _soundService;
        private readonly ISpeedEffectService _speedEffectService;

        public TryStartObstaclePassSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerMovementTypeService playerMovementTypeService,
            ISoundService soundService,
            IPlayerTiltingService playerTiltingService,
            ISpeedEffectService speedEffectService,
            IPlayerWaterInHoleParametersBase playerWaterInHoleParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _playerMovementTypeService = playerMovementTypeService;
            _soundService = soundService;
            _commandBuffer = commandBuffer;
            _playerTiltingService = playerTiltingService;
            _speedEffectService = speedEffectService;
            _playerWaterInHoleParameters = playerWaterInHoleParameters;
        }

        protected override void Execute(ref TryStartObstaclePassCommand command)
        {
            var characterUid = command.CharacterUid;
            var character = _game.GetEntityWithUid(characterUid);

            if (!character.HasReadyToObstaclePass)
                return;

            if (!character.HasObstaclePassReact)
                return;

            if (!character.HasChosenPassObstacleType)
                return;
            
            if (character.HasQteType)
                return;

            if (character.IsPlayer && character.IsDash)
                _commandBuffer.Dash(false);

            _playerTiltingService.CancelTilting(character);

            character.CharacterController.Value.enabled = false;
            character.IsEnableCharacterTrigger = false;

            var playerTransform = character.Transform.Value;
            var passObstacleData = character.PassObstacleMovePoints.Value;
            var chosenPassObstacleType = character.ChosenPassObstacleType.Value;
            var obstacleEntity = _game.GetEntityWithUid(command.ObstacleId);

            if (obstacleEntity == null)
                return;

            var obstacleUid = obstacleEntity.Owner.Uid;

            var movePoints = GetMovePoints(passObstacleData, chosenPassObstacleType);

            if (movePoints == null)
            {
                _commandBuffer.FinishPassObstacle(character.Uid.Value, false);
                return;
            }

            var passObstacleMultiplier = character.ObstaclePassReact.Value ? passObstacleData.ComboMoveMultiplier : 1;

            var obstacleTypeForAcceleration = character.ReadyToObstaclePass.Value.ObstacleTypeForAcceleration;

            HandleObstacle(obstacleTypeForAcceleration, character, obstacleEntity);

            var sequence = DOTween.Sequence();

            var playerPosition = playerTransform.position;
            var centerStartReadyTriggerPosition = passObstacleData.CenterStartReadyTriggerPosition;

            for (var i = 0; i < movePoints.Length; i++)
            {
                var index = i;
                var point = movePoints[index];
                var pointMoveTime = point.moveTime;
                var pointEase = point.MovementEase == Ease.Unset ? Ease.Linear : point.MovementEase;

                var destinationPointPosition = point.movePoint.position;

                destinationPointPosition += CalculatePointsOffset(
                    playerPosition,
                    destinationPointPosition,
                    centerStartReadyTriggerPosition
                );

                var moveTween = playerTransform.DOMove(destinationPointPosition, pointMoveTime * passObstacleMultiplier)
                    .SetEase(pointEase);

                sequence.Append(moveTween).AppendCallback(() =>
                    {
                        TryObstaclePointCommand(obstacleUid, characterUid, index, movePoints[index].ActionType);
                    }
                );
            }

            if (character.IsDashInHole)
            {
                character.IsDashInHole = false;
                character.IsComebackInHole = true;
            }

            if (character.IsFallingInHole)
                character.IsFallingInHole = false;

            var multiplayerAnimationSpeed = _playerWaterInHoleParameters.InWaterAnimationSpeedMultiplier;
            var multiplier = character.IsInWater ? multiplayerAnimationSpeed : 1;

            foreach (var data in passObstacleData.MovePoints)
                character.ReplaceAnimationSpeedUpRatio(data.PassObstacleAnimationSpeedMultiplayer * multiplier);

            _playerMovementTypeService.UpdateBodyType(character.Uid.Value);

            sequence.Play().OnComplete(() =>
                {
                    _commandBuffer.FinishPassObstacle(character.Uid.Value, true);
                    _soundService.PlaySound(ESoundFxName.BreathingWhenOvercomingAnObstacle);
                }
            );
        }

        private static PassObstacleMovePoint[] GetMovePoints(
            PassObstacleData passObstacleData,
            EPassObstacleType chosenPassObstacleType
        )
        {
            foreach (var obstacleMovePointsData in passObstacleData.MovePoints)
            {
                if (obstacleMovePointsData.PassObstacleType != chosenPassObstacleType)
                    continue;

                return obstacleMovePointsData.MovePoints;
            }

            return null;
        }

        private void TryObstaclePointCommand(
            Uid obstacleUid,
            Uid characterUid,
            int index,
            PassObstacleMovePoint.EActionType actionType
        )
        {
            if (actionType != PassObstacleMovePoint.EActionType.None)
                _commandBuffer.ObstaclePointReached(obstacleUid, characterUid, index);
        }

        private static Vector3 CalculatePointsOffset(
            Vector3 playerPosition,
            Vector3 dashDestinationPosition,
            Vector3 centerStartReadyTriggerPosition
        )
        {
            var endDashToPlayerVector = dashDestinationPosition - playerPosition;
            var dashVector = dashDestinationPosition - centerStartReadyTriggerPosition;

            endDashToPlayerVector.y = 0;
            dashVector.y = 0;

            var projectDashVector = Vector3.Project(endDashToPlayerVector, dashVector);
            var offset = endDashToPlayerVector - projectDashVector;

            return -offset;
        }

        private void HandleObstacle(
            EObstacleTypeForAcceleration obstacleTypeForAcceleration,
            GameEntity player,
            GameEntity obstacleEntity
        )
        {
            if (!player.IsReadyForFastActionMove)
                return;

            obstacleEntity.IsFastMoveColor = true;
            HandleFastActionMove(obstacleTypeForAcceleration, player);
        }

        private void HandleFastActionMove(EObstacleTypeForAcceleration obstacleTypeForAcceleration, GameEntity player)
        {
            if (obstacleTypeForAcceleration == EObstacleTypeForAcceleration.Destructible)
                HandleDestructibleObstacle(player);
            else
                _commandBuffer.StartAcceleration(obstacleTypeForAcceleration);
        }

        private void HandleDestructibleObstacle(GameEntity player)
        {
            if (player.HasObstacleFeedbackAfterTimer)
                player.RemoveObstacleFeedbackAfterTimer();

            if (_game.PlayerEntity.IsPlayer)
                _speedEffectService.DisableSpeedEffect();
        }
    }
}