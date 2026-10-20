using System;
using Cysharp.Threading.Tasks;
using Db.Tutorial;
using Ecs.Action.Commands.Tutorial;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Utils.Tutorial;
using Game.Utils.UniTaskExtensions;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Action.Systems.Tutorial
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 85, nameof(EFeatures.Tutorial))]
    public class TeleportPlayerToTheSequenceStartSystem
        : ForEachCommandUpdateSystem<TeleportPlayerToTutorialStartCommand>
    {
        private readonly GameContext _game;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ITutorialParameters _tutorialParameters;

        public TeleportPlayerToTheSequenceStartSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ITutorialParameters tutorialParameters
        ) : base(commandBuffer)
        {
            _game = game;
            _gameGroupUtils = gameGroupUtils;
            _tutorialParameters = tutorialParameters;
        }

        protected override void Execute(ref TeleportPlayerToTutorialStartCommand command)
        {
            var tutorial = _game.TutorialEntity;
            var currentState = tutorial.TutorialState.Value;

            using var _ = _gameGroupUtils.GetChunks(out var chunks, entity => IsIndex(entity, currentState));

            if (chunks.Count <= 0)
                throw new ArgumentOutOfRangeException(
                    $"Tutorial sequence chunk ({currentState}) not found. Maybe it was deleted :("
                );

            var player = _game.PlayerEntity;
            var startTutorialChunk = chunks[0];
            var anchors = startTutorialChunk.ChunkAnchors.Value;
            var startAnchor = anchors.StartAnchor;
            TeleportPlayerToPoint(player, startAnchor).ForgetAndWrapExceptions();
        }

        private bool IsIndex(GameEntity entity, ETutorialState currentState)
        {
            if (!entity.HasTutorialChunkData)
                return false;

            var chunkData = entity.TutorialChunkData.Value;

            if (chunkData.State != currentState)
                return false;

            return chunkData.Index == 0;
        }

        private async UniTask TeleportPlayerToPoint(GameEntity player, Transform targetPoint)
        {
            var playerCharacterController = player.CharacterController.Value;

            player.IsCanSwipe = false;
            playerCharacterController.enabled = false;
            player.IsEnableCharacterTrigger = false;

            var forwardOffset = targetPoint.forward * _tutorialParameters.FailTeleportForwardOffset;
            var targetPosition = targetPoint.position + forwardOffset;
            player.ReplacePosition(targetPosition);
            player.ReplaceRotation(targetPoint.rotation);

            await UniTask.DelayFrame(10);

            playerCharacterController.enabled = true;
            player.IsEnableCharacterTrigger = true;
            player.IsCanSwipe = true;
            player.IsTutorialFailTeleporting = false;
            player.IsReboundState = true;
        }
    }
}