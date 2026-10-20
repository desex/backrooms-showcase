using Db.Player;
using Ecs.Core.Systems;
using Ecs.Utils;
using Ecs.Utils.Chunks;
using Ecs.Utils.Groups;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Movement
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 590, nameof(EFeatures.Move))]
    public class OutOfBoundsKillPlayerSystem : APausableCharacterConditionUpdateSystem
    {
        private readonly IChunkGenerationServiceProvider _chunkGenerationServiceProvider;
        private readonly ICommandBuffer _commandBuffer;
        private readonly GameContext _game;
        private readonly IOutOfBoundsKillPlayerParameters _outBoundsKillPlayerParameters;

        public OutOfBoundsKillPlayerSystem(
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            IOutOfBoundsKillPlayerParameters outBoundsKillPlayerParameters,
            IChunkGenerationServiceProvider chunkGenerationServiceProvider,
            ICommandBuffer commandBuffer
        ) : base(game, gameGroupUtils)
        {
            _game = game;
            _outBoundsKillPlayerParameters = outBoundsKillPlayerParameters;
            _chunkGenerationServiceProvider = chunkGenerationServiceProvider;
            _commandBuffer = commandBuffer;
        }

        protected override bool EntityFilter(GameEntity entity)
        {
            return !entity.IsOutOfBoundsKillPlayer
                   && !entity.IsDead
                   && !entity.IsFallingInHole
                   && !entity.IsGrounded;
        }

        protected override void HandleUpdate(GameEntity character)
        {
            if (!TryGetChunkPositionY(character, out var lastChunkPositionY))
                return;

            if (!IsKillPosition(character, lastChunkPositionY))
                return;

            character.IsOutOfBoundsKillPlayer = true;

            if (character.IsPlayer)
                _commandBuffer.KillPlayer(character.Uid.Value, EDeathReason.OutOfBounds, true);
        }

        private bool TryGetChunkPositionY(GameEntity character, out float chunkPositionY)
        {
            if (character.HasCurrentChunkId)
            {
                var currentChunk = _game.GetEntityWithUid(character.CurrentChunkId.Value);

                if (currentChunk != null)
                {
                    chunkPositionY = currentChunk.Transform.Value.position.y;
                    return true;
                }
            }

            var lastChunk = _game.GetEntityWithUid(
                _chunkGenerationServiceProvider.ChunkGenerationService.LastChunkUid
            );

            if (lastChunk != null && lastChunk.HasChunkAnchors)
            {
                chunkPositionY = GetLastChunkPositionY(lastChunk);
                return true;
            }

            chunkPositionY = default;
            return false;
        }

        private static float GetLastChunkPositionY(GameEntity lastChunk)
        {
            var chunkAnchors = lastChunk.ChunkAnchors.Value;

            var lastChunkPositionY = chunkAnchors.EndLeftAnchor
                ? chunkAnchors.EndLeftAnchor.position.y
                : chunkAnchors.EndOnlyAnchor.position.y;

            return lastChunkPositionY;
        }

        private bool IsKillPosition(GameEntity character, float lastChunkPositionY)
        {
            var characterPositionY = character.Position.Value.y;
            var killDistance = _outBoundsKillPlayerParameters.KillDistance;
            var killPositionY = lastChunkPositionY - killDistance;

            return characterPositionY < killPositionY;
        }
    }
}