using System;
using Db.Player;
using Ecs.Core.Interfaces;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 110, nameof(EFeatures.Move))]
    public class PlayerTurnUpdateSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        private readonly IPlayerTurningParametersBase _playerTurningParametersBase;
        private readonly ITimeProvider _timeProvider;

        public PlayerTurnUpdateSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITimeProvider timeProvider,
            IPlayerTurningParametersBase playerTurningParametersBase,
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters
        ) : base(game, gameGroupUtils)
        {
            _timeProvider = timeProvider;
            _playerTurningParametersBase = playerTurningParametersBase;
            _playerSpeedProgressionParameters = playerSpeedProgressionParameters;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return entity.HasTurning;
        }

        protected override void HandleUpdate(GameEntity entity)
        {
            ref readonly var turningValues = ref entity.Turning.Value;

            var newTime = GetNewTime(in turningValues, entity);

            var isCameraTilting = TryEndTilting(in turningValues, newTime);

            if (TryEndTurning(entity, in turningValues, newTime))
                return;

            var newRotation = GetNewRotation(entity, in turningValues, newTime);

            entity.ReplaceRotation(newRotation);
            RewriteTurnParameters(entity, in turningValues, newTime, isCameraTilting);
        }

        private float GetNewTime(in TurningValues turningValues, GameEntity character)
        {
            var lastTime = turningValues.CurrentTurningTime;
            var newTime = lastTime / GetTurnDuration(character) + _timeProvider.DeltaTime;
            return newTime;
        }

        private bool TryEndTilting(in TurningValues turningValues, float newTime)
        {
            var isCameraTilting = turningValues.IsCameraTilting;
            var endTiltTime = _playerTurningParametersBase.EndTiltTime;

            switch (isCameraTilting)
            {
                case true when newTime < endTiltTime:
                    return true;

                case false:
                    return false;
            }

            return false;
        }

        private bool TryEndTurning(GameEntity player, in TurningValues turningValues, float newTime)
        {
            var turnDuration = GetTurnDuration(player);

            if (newTime < turnDuration)
                return false;

            var targetAngle = turningValues.TargetAngle;
            var targetRotation = Quaternion.Euler(new Vector3(0f, targetAngle, 0f));
            player.ReplaceRotation(targetRotation);
            player.RemoveTurning();

            return true;
        }

        private Quaternion GetNewRotation(IRotationEntity player, in TurningValues turningValues, float newTime)
        {
            var deltaAngle = GetDeltaAngle(in turningValues, newTime);
            var playerRotation = player.Rotation.Value;
            var playerAngle = playerRotation.eulerAngles.y;
            var newRotation = Quaternion.Euler(new Vector3(0, playerAngle + deltaAngle, 0));

            return newRotation;
        }

        private float GetDeltaAngle(in TurningValues turningValues, float newTime)
        {
            var turningCurve = _playerTurningParametersBase.TurningCurve;
            var turningAngle = _playerTurningParametersBase.TurningAngle;

            var lastTime = turningValues.CurrentTurningTime;
            var isRightTurning = turningValues.IsRightTurning;

            var lastAngleRatio = turningCurve.Evaluate(lastTime);
            var newAngleRatio = turningCurve.Evaluate(newTime);
            var deltaAngle = (newAngleRatio - lastAngleRatio) * turningAngle;
            deltaAngle = isRightTurning ? deltaAngle : -deltaAngle;

            return deltaAngle;
        }

        private static void RewriteTurnParameters(
            ITurningEntity player,
            in TurningValues turningValues,
            float newTime,
            bool isCameraTilting
        )
        {
            var targetAngle = turningValues.TargetAngle;
            var isRightTurning = turningValues.IsRightTurning;

            var newParameters =
                new TurningValues(targetAngle, newTime, isRightTurning, isCameraTilting);

            player.Turning.Value = newParameters;
        }

        private float GetTurnDuration(GameEntity character)
        {
            var minSpeedProgressionMultiplier = _playerSpeedProgressionParameters.InitialForwardSpeedMultiplier;

            var percent = (_playerSpeedProgressionParameters.MaxProgressionMultiply - minSpeedProgressionMultiplier)
                          / 100f;

            var currentPercent = (character.ForwardSpeedMultiplier.Value - minSpeedProgressionMultiplier) / percent;

            var minMaxDuration = GetMinMaxDuration(character);
            var minDuration = minMaxDuration.x;
            var maxDuration = minMaxDuration.y;
            var durationPercent = (maxDuration - minDuration) / 100f;
            var duration = maxDuration - durationPercent * currentPercent;
            duration = Mathf.Clamp(duration, minDuration, maxDuration);

            return duration;
        }

        private Vector2 GetMinMaxDuration(GameEntity character)
        {
            var currentChunk = Game.GetEntityWithUid(character.CurrentChunkId.Value);

            if (currentChunk == null)
                return _playerTurningParametersBase.DefaultTurningDuration.Duration;

            if (currentChunk.ChunkNode == null)
                throw new NullReferenceException($"{nameof(PlayerTurnUpdateSystem)} Current chunk node is null");

            var chunkType = currentChunk.ChunkNode.Value.ChunkType;

            if (_playerTurningParametersBase.ChunkDurations.TryGetValue(chunkType, out var data))
                return data.Duration;

            return _playerTurningParametersBase.DefaultTurningDuration.Duration;
        }
    }
}