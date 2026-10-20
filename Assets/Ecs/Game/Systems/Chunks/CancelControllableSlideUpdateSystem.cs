using Ecs.Core.Systems;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Game.Services.DistanceCalculation;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Generated.Commands;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 945, nameof(EFeatures.Move))]
    public class CancelControllableSlideUpdateSystem : APausableConditionUpdateSystem
    {
        private readonly ICommandBuffer _commandBuffer;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IGameGroupUtils _gameGroupUtils;

        public CancelControllableSlideUpdateSystem(
            IGameGroupUtils gameGroupUtils,
            IDistanceCalculationService distanceCalculationService,
            ICommandBuffer commandBuffer,
            GameContext game,
            IChunkSegmentService chunkSegmentService
        )
            : base(game)
        {
            _gameGroupUtils = gameGroupUtils;
            _distanceCalculationService = distanceCalculationService;
            _commandBuffer = commandBuffer;
            _chunkSegmentService = chunkSegmentService;
        }

        protected override void UpdateSystem()
        {
            using var _ = _gameGroupUtils.GetCharacters(out var characters, e => e.HasSlideChunk);

            foreach (var character in characters)
            {
                var characterUid = character.Uid.Value;
                var slideChunk = Game.GetEntityWithUid(character.SlideChunk.Value);

                if (slideChunk == null)
                {
                    CancelSlide(character, characterUid);
                    continue;
                }

                var endAnchorPosition = slideChunk.ChunkAnchors.Value.EndOnlyAnchor.position;
                var endDistance = _distanceCalculationService.GetDistance(endAnchorPosition, _chunkSegmentService.Segments);
                var playerDistance = _distanceCalculationService.GetCharacterDistance(characterUid, _chunkSegmentService.Segments);

                if (playerDistance < endDistance)
                    continue;

                var nodes = slideChunk.ChunkNode.Value.NextNodes;

                if (nodes[0] == null)
                {
                    CancelSlide(character, characterUid);
                    continue;
                }

                if (nodes[0].ChunkType == EChunkType.Down)
                    continue;

                CancelSlide(character, characterUid);
            }
        }

        private void CancelSlide(GameEntity character, Uid characterUid)
        {
            _commandBuffer.SetReadyControllableSlide(characterUid, false, 0f);
            character.RemoveSlideChunk();
            character.RemoveVerticalSpeed();
        }
    }
}