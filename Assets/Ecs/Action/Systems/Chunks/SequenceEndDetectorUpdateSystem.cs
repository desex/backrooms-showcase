using Ecs.Core.Systems;
using Ecs.Utils;
using Game.Services.Chunks;
using Game.Services.DistanceCalculation;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using UnityEngine;

namespace Ecs.Action.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 852, nameof(EFeatures.Chunk))]
    public class SequenceEndDetectorUpdateSystem : APausableConditionUpdateSystem
    {
        private const float END_SEQUENCE_THRESHOLD = 3f;
        
        private readonly GameContext _game;
        private readonly IChunkCurvedService _chunkCurvedService;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        
        public SequenceEndDetectorUpdateSystem(
            GameContext game,
            IDistanceCalculationService distanceCalculationService,
            IChunkCurvedService chunkCurvedService,
            IChunkSegmentService chunkSegmentService
        ) : base(game)
        {
            _game = game;
            _distanceCalculationService = distanceCalculationService;
            _chunkCurvedService = chunkCurvedService;
            _chunkSegmentService = chunkSegmentService;
        }
        
        protected override bool ConditionFilter()
        {
            var entity = _game.PlayerEntity;
            
            return entity.HasSequenceCompletion
                && !entity.IsDead
                && !entity.IsDestroyed
                && !_game.TutorialEntity.IsInTutorial;
        }
        
        protected override void UpdateSystem()
        {
            var player = _game.PlayerEntity;
            
            var chunkEntity = _game.GetEntityWithUid(player.SequenceCompletion.Value);
            
            var playerDistance = _distanceCalculationService.GetPlayerDistance(_chunkSegmentService.Segments);
            
            var chunkEndDistance =
                _distanceCalculationService.GetDistance(chunkEntity.ChunkAnchors.Value.EndOnlyAnchor.position, _chunkSegmentService.Segments);
            
            if (Mathf.Abs(playerDistance - chunkEndDistance) <= END_SEQUENCE_THRESHOLD)
            {
                _chunkCurvedService.CompleteSequence(chunkEntity.Uid.Value);
            }
        }
    }
}