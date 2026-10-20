#if !DISABLE_GHOSTS
using Db.Ghost;
using Db.SoundFxBase;
using Ecs.Action.Commands.Revive;
using Ecs.Managers;
using Ecs.Utils;
using Game.Providers;
using Game.Services.DistanceCalculation;
using Game.Services.General;
using Game.Services.Ghosts.Sound;
using Game.Services.Player;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Game.Utils.Movement;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using PdUtils.RandomProvider;
using UnityEngine;

namespace Ecs.Action.Systems.Revive
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 90, nameof(EFeatures.Common))]
    public class StartReviveGhostSystem : ForEachCommandUpdateSystem<StartGhostReviveCommand>
    {
        private readonly IPlayerMovementTypeService _movementTypeService;
        private readonly GameContext _game;
        private readonly IRandomProvider _randomProvider;
        private readonly IGhostSoundDispatcher _ghostSoundDispatcher;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IGhostsParameters _ghostsParameters;
        private readonly IGameFieldProvider _gameFieldProvider;

        public StartReviveGhostSystem(
            GameContext game,
            ICommandBuffer commandBuffer,
            IPlayerMovementTypeService movementTypeService,
            IRandomProvider randomProvider,
            IGhostSoundDispatcher ghostSoundDispatcher,
            IDistanceCalculationService distanceCalculationService,
            IGhostsParameters ghostsParameters,
            IGameFieldProvider gameFieldProvider
        ) : base(commandBuffer)
        {
            _game = game;
            _movementTypeService = movementTypeService;
            _randomProvider = randomProvider;
            _ghostSoundDispatcher = ghostSoundDispatcher;
            _distanceCalculationService = distanceCalculationService;
            _ghostsParameters = ghostsParameters;
            _gameFieldProvider = gameFieldProvider;
        }

        protected override void Execute(ref StartGhostReviveCommand command)
        {
            ReviveWithCooldown(command.CharacterUid);
        }

        private void ReviveWithCooldown(Uid characterUid)
        {
            var player = _game.PlayerEntity;

            if (!TryCalculatePointOnDistance(player, out var spawnPoint))
            {
                var cooldown = _ghostsParameters.CooldownAfterFailRespawn;
                DelayedInvocationService.Invoke(() => ReviveWithCooldown(characterUid), cooldown);
                return;
            }

            var ghost = _game.GetEntityWithUid(characterUid);

            ApplyAndRandomPositionAndRotation(ghost, spawnPoint);
            ghost.Link.View.Transform.parent = _gameFieldProvider.GameField.GhostsRoot.transform;

            ghost.IsAiEnabled = false;
            ghost.CharacterController.Value.enabled = false;

            DelayedInvocationService.Invoke(EnableCharacterController, 10);

            ghost.IsEnableCharacterTrigger = false;
            
            DelayedInvocationService.Invoke(EnableCharacterController , 10);
            
            return;

            void EnableCharacterController()
            {
                ghost.IsActive = true;
                ghost.IsAiEnabled = true;
                ghost.CharacterController.Value.enabled = true;
                ghost.IsEnableCharacterTrigger = true;
                _movementTypeService.ChangeBodyMoveType(ghost.Uid.Value, EBodyMoveType.Run);
                _ghostSoundDispatcher.PlaySound(characterUid, ESoundFxName.GhostAppereance);
            }
        }

        private bool TryCalculatePointOnDistance(GameEntity player, out SpawnPoint spawnPoint)
        {
            var forwardDeltaRange = _ghostsParameters.ForwardReviveDeltaRange;
            var forwardDelta = _randomProvider.Range(forwardDeltaRange.x, forwardDeltaRange.y);
            var targetForwardDistance = player.PassedDistance.Value + forwardDelta;

            if (!_distanceCalculationService.TryGetPositions(targetForwardDistance, out var spawnPoints))
            {
                spawnPoint = default;
                return false;
            }

            var randomSpawnPointIndex = _randomProvider.Range(0, spawnPoints.Count);
            spawnPoint = spawnPoints[randomSpawnPointIndex];
            return true;
        }

        private void ApplyAndRandomPositionAndRotation(GameEntity ghost, SpawnPoint spawnPoint)
        {
            Vector3 rightDirection;
            Vector3 centerPosition;
            Quaternion rotation;

            var currentChunk = _game.GetEntityWithUid(spawnPoint.ChunkUid);
            var chunkAnchors = currentChunk.ChunkAnchors.Value;
            var chunkType = spawnPoint.ChunkType;

            switch (chunkType)
            {
                case EChunkType.TurnBothSides or EChunkType.TurnBothSidesBody:
                    SetToTurnBothSidesEndAnchorsWithForwardOffset();
                    break;

                case EChunkType.TurnLeft
                    or EChunkType.TurnRight
                    or EChunkType.TurnLeftBody
                    or EChunkType.TurnRightBody:
                    SetToEndAnchorPointWithForwardOffset();
                    break;

                case EChunkType.Down
                    or EChunkType.Curved:
                    SetToEndAnchorPoint();
                    break;

                default:
                    SetToSelectedSpawnPoint();
                    break;
            }

            var sidesDeltaRange = _ghostsParameters.SidesReviveDeltaRange;
            var sideVectorSign = _randomProvider.Value > 0.5 ? 1 : -1;

            var randomDeltaRight = _randomProvider.Range(sidesDeltaRange.x, sidesDeltaRange.y) * sideVectorSign;

            var randomPosition = centerPosition + randomDeltaRight * rightDirection;

            ghost.ReplacePosition(randomPosition);
            ghost.ReplaceRotation(rotation);

            ghost.ReplaceCurrentChunkId(spawnPoint.ChunkUid);

            return;

            void SetToTurnBothSidesEndAnchorsWithForwardOffset()
            {
                var turnRight = _randomProvider.Range(0f, 1f) > 0.5f;
                var endAnchor = turnRight ? chunkAnchors.EndRightAnchor : chunkAnchors.EndLeftAnchor;
                rightDirection = endAnchor.right;

                var forwardOffset = _ghostsParameters.TurnChunksForwardReviveOffset;
                centerPosition = endAnchor.position + endAnchor.forward * forwardOffset;

                rotation = endAnchor.rotation;
            }

            void SetToEndAnchorPoint()
            {
                var endOnlyAnchor = chunkAnchors.EndOnlyAnchor;
                rightDirection = endOnlyAnchor.right;
                centerPosition = endOnlyAnchor.position;
                rotation = endOnlyAnchor.rotation;
            }

            void SetToEndAnchorPointWithForwardOffset()
            {
                var endOnlyAnchor = chunkAnchors.EndOnlyAnchor;
                rightDirection = endOnlyAnchor.right;
                var forwardOffset = _ghostsParameters.TurnChunksForwardReviveOffset;
                centerPosition = endOnlyAnchor.position + endOnlyAnchor.forward * forwardOffset;
                rotation = endOnlyAnchor.rotation;
            }

            void SetToSelectedSpawnPoint()
            {
                centerPosition = spawnPoint.Position;
                rotation = spawnPoint.Rotation;
                rightDirection = rotation * Vector3.right;
            }
        }
    }
}
#endif