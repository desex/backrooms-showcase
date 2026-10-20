using System;
using DG.Tweening;
using Ecs.Action.Commands.Revive;
using Ecs.Utils;
using Ecs.Utils.Revive;
using Game.Services.General;
using Game.Services.Player;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.ColorsUtils;
using Game.Utils.Movement;
using Game.Utils.Revive;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Revive
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 90, nameof(EFeatures.Common))]
    public class StartReviveSystem : ForEachCommandUpdateSystem<StartReviveCommand>
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IPlayerMovementTypeService _movementTypeService;

        private Sequence _sequence;

        public StartReviveSystem(
            ICommandBuffer commandBuffer,
            IPlayerMovementTypeService movementTypeService,
            GameContext game
        ) : base(commandBuffer)
        {
            _game = game;
            _commandBuffer = commandBuffer;
            _movementTypeService = movementTypeService;
        }

        protected override void Execute(ref StartReviveCommand command)
        {
            var player = _game.PlayerEntity;

            if (!player.HasRevive)
            {
                DebugUtility.LogError(
                    $"In {nameof(StartReviveSystem)} player hasn't ReviveComponent",
                    UtilsColors.ErrorColor
                );

                return;
            }

            var reviveData = player.Revive.Value;
            var reviveType = reviveData.ReviveType;

            switch (reviveType)
            {
                case EReviveType.Hole:
                    MoveToPoints(player, reviveData);
                    break;

                case EReviveType.Teleport:
                    Teleport(player, reviveData);
                    break;

                default:
                    throw new ArgumentOutOfRangeException($"Revive type is not supported: {reviveType}");
            }
        }

        private void MoveToPoints(GameEntity player, ReviveData reviveData)
        {
            var transform = player.Transform.Value;
            var points = reviveData.MovePoints;
            var startPosition = points[0].Point.position;
            SetStartPosition(startPosition);
            UpdateBodyMoveType(player);

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            foreach (var point in points)
                _sequence.Append(transform.DOMove(point.Point.position, point.Duration).SetEase(point.Ease));

            _sequence.AppendCallback(OnEnd);
        }

        private void Teleport(GameEntity player, ReviveData reviveData)
        {
            var currentChunkId = _game.PlayerEntity.CurrentChunkId.Value;
            var currentChunk = _game.GetEntityWithUid(currentChunkId);
            var currentChunkAnchor = currentChunk.ChunkAnchors.Value;

            var respawnPosition = reviveData.ReviveTransform.position;

            var safeRespawnRotation = currentChunk.ChunkNode.Value.ChunkType == EChunkType.Straight
                ? GetDirection(currentChunkAnchor.StartAnchor.position, currentChunkAnchor.EndOnlyAnchor.position)
                : reviveData.ReviveTransform.rotation;

            player.CharacterController.Value.enabled = false;
            player.IsEnableCharacterTrigger = false;
            player.ReplacePosition(respawnPosition);
            player.ReplaceRotation(safeRespawnRotation);
            OnEnd();

            DelayedInvocationService.Invoke(
                () =>
                {
                    player.CharacterController.Value.enabled = true;
                    player.IsEnableCharacterTrigger = true;

                    var chunk = _game.GetEntityWithUid(player.CurrentChunkId.Value);
                    var type = chunk.ChunkNode.Value.ChunkType;

                    if (type.HasFlag(EChunkType.Down))
                        _commandBuffer.StartSlide(player.Uid.Value);
                },
                10
            );

            return;

            Quaternion GetDirection(Vector3 startAnchor, Vector3 endAnchor)
            {
                var direction = endAnchor - startAnchor;

                return direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            }
        }

        private void SetStartPosition(Vector3 startPosition)
        {
            var player = _game.PlayerEntity;
            startPosition.x = player.Transform.Value.position.x;

            player.ReplacePosition(startPosition);
            player.IsCanSwipe = false;
        }

        private void OnEnd()
        {
            var player = _game.PlayerEntity;

            player.IsCanSwipe = true;
            player.IsFallingInHole = false;
            _movementTypeService.ChangeBodyMoveType(player.Uid.Value, EBodyMoveType.Run);

            ResetPlayerReviveComponents(player);
        }

        private void UpdateBodyMoveType(GameEntity entity)
        {
            var reviveType = entity.Revive.Value.ReviveType;

            var bodyMoveType = reviveType switch
            {
                EReviveType.Hole => EBodyMoveType.ComebackInHole,
                _ => EBodyMoveType.Run,
            };

            _movementTypeService.ChangeBodyMoveType(entity.Uid.Value, bodyMoveType);
        }

        private void ResetPlayerReviveComponents(GameEntity player)
        {
            if (player.HasReadyToObstaclePass)
                player.RemoveReadyToObstaclePass();

            if (player.HasReviveObstacle)
                player.RemoveReviveObstacle();

            if (player.HasRevive)
                player.RemoveRevive();
        }
    }
}