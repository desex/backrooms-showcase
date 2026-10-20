#if !DISABLE_GHOSTS
using System.Collections.Generic;
using Db.Ghost;
using Db.Player;
using Ecs.Action.Commands.Ghosts;
using Ecs.Game.Extensions;
using Ecs.Utils;
using Game.Providers;
using Game.Services.Ghosts.Sound;
using Game.Services.Player;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;
using UnityEngine.Pool;

namespace Ecs.Action.Systems.Ghost
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 941, nameof(EFeatures.Ghost))]
    public class SpawnGhostsSystem : ForEachCommandUpdateSystem<SpawnGhostsCommand>
    {
        private const float FIRST_CHUNK_WIDTH = 9f;
        
        private readonly GameContext _game;
        private readonly IPlayerSpeedProgressionParameters _playerSpeedProgressionParameters;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IGhostsParameters _ghostsParameters;
        private readonly IPlayerMovementTypeService _playerMovementTypeService;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;

        public SpawnGhostsSystem(
            ICommandBuffer commandBuffer,
            GameContext game,
            IPlayerSpeedProgressionParameters playerSpeedProgressionParameters,
            IGameFieldProvider gameFieldProvider,
            IGhostsParameters ghostsParameters,
            IGhostSoundDispatcher ghostSoundDispatcher,
            IPlayerMovementTypeService playerMovementTypeService
        ) : base(commandBuffer)
        {
            _game = game;
            _playerSpeedProgressionParameters = playerSpeedProgressionParameters;
            _gameFieldProvider = gameFieldProvider;
            _ghostsParameters = ghostsParameters;
            _playerMovementTypeService = playerMovementTypeService;
            _commandBuffer = commandBuffer;
            _ghostSoundDispatcher = ghostSoundDispatcher;
        }

        protected override void Execute(ref SpawnGhostsCommand command)
        {
            var playerViewTransform = _gameFieldProvider.GameField.PlayerView.transform;

            var ghostRotation = playerViewTransform.rotation;

            var initialForwardSpeedMultiplier = _playerSpeedProgressionParameters.InitialForwardSpeedMultiplier;
            
            var ghostsCount = _ghostsParameters.GhostCount;
            
            using var _ = ListPool<Vector3>.Get(out var spawnPositions);
            CalculateSpawnPoints(spawnPositions, playerViewTransform);

            for (var i = 0; i < ghostsCount; i++)
            {
                var ghostPosition = spawnPositions[i];
                
                var ghost = _game.CreateGhost(
                    ghostPosition, 
                    ghostRotation,
                    initialForwardSpeedMultiplier,
                    _gameFieldProvider.GameField.GhostsRoot
                );
                
                var ghostUid = ghost.Uid.Value;
                _ghostSoundDispatcher.AddGhostSoundService(ghostUid);
                _commandBuffer.RandomNewGhostChances(ghostUid);
                _playerMovementTypeService.UpdateBodyType(ghostUid);
            }
        }

        private void CalculateSpawnPoints(in List<Vector3> spawnPoints, Transform playerTransform)
        {
            var ghostsCount = _ghostsParameters.GhostCount;

            if (ghostsCount == 0)
                return;

            var ghostHeight = _ghostsParameters.GhostHeight;
            var frontDelta = _ghostsParameters.StartForwardDeltaFromPlayer;
            
            const float leftChunkBorder = -FIRST_CHUNK_WIDTH / 2;
            
            if (ghostsCount == 1)
            {
                const float singleGhostHorizontalPosition = leftChunkBorder + FIRST_CHUNK_WIDTH / 2;
                var targetSpawnPoint = new Vector3(singleGhostHorizontalPosition, ghostHeight / 2, frontDelta);
                spawnPoints.Add(targetSpawnPoint);
                return;
            }
            
            var lastGhostIndex = ghostsCount - 1;
            var positionDelta = FIRST_CHUNK_WIDTH / lastGhostIndex;

            var playerForward = playerTransform.forward;
            var playerRight = playerTransform.right;
            var playerPosition = playerTransform.position;
            
            for (var i = 0; i < ghostsCount; i++)
            {
                var targetHorizontal = leftChunkBorder + i * positionDelta;
                var targetSpawnPoint = playerPosition;
                targetSpawnPoint += frontDelta * playerForward;
                targetSpawnPoint += targetHorizontal * playerRight;
                spawnPoints.Add(targetSpawnPoint); 
            }
        }
    }
}
#endif