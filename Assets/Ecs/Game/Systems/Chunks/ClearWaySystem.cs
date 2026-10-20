using System;
using System.Collections.Generic;
using Ecs.Action.Commands.Chunks;
using Ecs.Utils;
using Ecs.Utils.Groups;
using Ecs.Views.Linkable.Impl.Chunk;
using Ecs.Views.Linkable.Impl.Collectables;
using Game.Services.CollectablesItem;
using Game.Services.DistanceCalculation;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux.Commands;
using UnityEngine;

namespace Ecs.Game.Systems.Chunks
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 4, nameof(EFeatures.Chunk))]
    public class ClearWaySystem : ForEachCommandUpdateSystem<ClearWayCommand>
    {
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly IGameGroupUtils _gameGroupUtils;
        private readonly ICollectablesPool _collectablesPool;
        private readonly GameContext _game;

        public ClearWaySystem(
            ICommandBuffer commandBuffer,
            IDistanceCalculationService distanceCalculationService,
            GameContext game,
            IGameGroupUtils gameGroupUtils,
            ICollectablesPool collectablesPool,
            IChunkSegmentService chunkSegmentService
        ) : base(commandBuffer)
        {
            _distanceCalculationService = distanceCalculationService;
            _game = game;
            _gameGroupUtils = gameGroupUtils;
            _collectablesPool = collectablesPool;
            _chunkSegmentService = chunkSegmentService;
        }

        protected override void Execute(ref ClearWayCommand command)
        {
            var clearWayData = command.Data;
            
            var chunkEntity = clearWayData.ChunkEntity;
            var targetAnchor = clearWayData.ChunkSide is EChunkSide.Left ? 0 : 1;
            var chunkNode = chunkEntity.ChunkNode.Value;
            var chunkNodes = chunkNode.NextNodes;

            ClearCollectables(clearWayData.MaxHeightCollectables);
            CollectAndTeleportSubTree(chunkNodes[targetAnchor]);
        }

        private void CollectAndTeleportSubTree(ChunkNode node)
        {
            if (node == null)
                return;

            CollectAndTeleportSubTree(node.NextNodes[0]);
            CollectAndTeleportSubTree(node.NextNodes[1]);

            var chunkEntity = _game.GetEntityWithUid(node.ChunkUid);
            TeleportChunk(chunkEntity);
        }

        private void TeleportChunk(GameEntity chunkEntity)
        {
            if (chunkEntity.IsDestroyed || !chunkEntity.IsActive)
                return;

            if (chunkEntity.Link.View is not ChunkView view)
                return;

            _chunkSegmentService.UnregisterChunk(chunkEntity);
            _distanceCalculationService.CalculateDistances(_chunkSegmentService.Segments);
            Teleport(view.ChunkWalls, x => x.transform);
            Teleport(view.Obstacles, x => x.transform);
            Teleport(view.BuiltInObstacles, x => x.Transform);

            view.transform.SetFarAwayPosition(view.transform.position);
        }

        private static void Teleport<T>(IEnumerable<T> collection, Func<T, Transform> selector)
        {
            if (collection == null) return;

            foreach (var item in collection)
            {
                if (item == null) continue;

                selector(item)?.SetFarAwayPosition(selector(item)!.position);
            }
        }
        
        private void ClearCollectables(float heightThreshold)
        {
            using var _ = _gameGroupUtils.GetCollectables(out var collectables,
                                                          entity => !entity.HasOwner && entity.HasCollectableItem && entity.IsActive);
            
            foreach (var collectable in collectables)
            {
                var collectablePosition = collectable.Position.Value;
                
                var playerHeight = _game.PlayerEntity.Transform.Value.position.y;
                var collectableHeight = collectablePosition.y;
                
                if (Mathf.Abs(playerHeight - collectableHeight) > heightThreshold)
                {
                    continue;
                }
                
                var collectableEntity = _game.GetEntityWithUid(collectable.Uid.Value);
                var collectableView = collectableEntity.Link.View as ItemCollectableView;
                var collectableType = collectableEntity.CollectableItem.Value;
                collectableEntity.IsActive = false;
                
                _collectablesPool.DespawnCollectable(collectableType,collectableView);
            }
        }
    }
}