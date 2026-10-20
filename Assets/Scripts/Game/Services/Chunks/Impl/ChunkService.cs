using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Managers;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Providers;
using Game.Services.CheckPoints;
using Game.Services.DistanceCalculation;
using Game.Services.Obstacles;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.DistanceCalculation;
using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using R3;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    public sealed class ChunkService : IChunkService, IDisposable
    {
        private readonly ICheckpointsPool _checkpointsPool;
        private readonly IChunkPool _chunkPool;
        private readonly ICommandBuffer _commandBuffer;
        private readonly IDistanceCalculationService _distanceCalculationService;
        private readonly IChunkSegmentService _chunkSegmentService;
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;

        private readonly HashSet<ChunkNode> _nodesToDelete = new HashSet<ChunkNode>();
        private readonly IObstacleService _obstacleService;
        private readonly ISequentialObstacleService _sequentialObstacleService;
        private readonly IWallService _wallService;

        private IDisposable _disposable;

        public ChunkService(
            ICheckpointsPool checkpointsPool,
            IChunkPool chunkPool,
            IWallService wallService,
            GameContext game,
            IGameFieldProvider gameFieldProvider,
            IObstacleService obstacleService,
            ISequentialObstacleService sequentialObstacleService,
            IDistanceCalculationService distanceCalculationService,
            ICommandBuffer commandBuffer,
            IChunkSegmentService chunkSegmentService
        )
        {
            _checkpointsPool = checkpointsPool;
            _chunkPool = chunkPool;
            _wallService = wallService;
            _game = game;
            _gameFieldProvider = gameFieldProvider;
            _obstacleService = obstacleService;
            _sequentialObstacleService = sequentialObstacleService;
            _distanceCalculationService = distanceCalculationService;
            _commandBuffer = commandBuffer;
            _chunkSegmentService = chunkSegmentService;

            SubscribeForViewWasDestroyed();
        }

        public async UniTask<GameEntity> CreateChunk(
            EChunkType chunkType,
            CancellationToken token,
            Transform previousChunkEndAnchor = null,
            GameEntity previousChunk = null,
            Vector3? positionOffset = null
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var (chunkView, randomIndex) = _chunkPool.SpawnChunk(chunkType);
            
            SetupTransform(chunkView, previousChunkEndAnchor, previousChunk, positionOffset);
            
            try
            {
                chunkView.Activate();
            }
            catch (Exception e)
            {
                DebugUtility.LogError($"chunkView.Activate exception, {e}", Color.red);
            }

            var chunkEntity = await GetOrCreateEntity(chunkView, token);
            var chunkUid = chunkEntity.Uid.Value;

            var chunkAnchors = new ChunkAnchors(
                chunkView.StartAnchor,
                chunkView.EndOnlyAnchor,
                chunkView.EndLeftAnchor,
                chunkView.EndRightAnchor,
                chunkView.MiddleAnchors
            );
            chunkEntity.ReplaceChunkAnchors(chunkAnchors);
            
            chunkEntity.ReplaceRandomIndex(randomIndex);
            ReplaceNode(chunkEntity, chunkUid, chunkType, previousChunk);

            await SetupBuildInObstacles(chunkView, chunkEntity, token);

            return chunkEntity;
        }

        private async UniTask<GameEntity> GetOrCreateEntity(ChunkView chunkView, CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            return chunkView.EntityId == null ? CreateEntity(chunkView) : GetEntity(chunkView);
        }
        
        private GameEntity CreateEntity(ChunkView chunkView)
        {
            var newChunkEntity = _game.CreateEntity();
            var uid = UidGenerator.Next();
            newChunkEntity.AddUid(uid);
            chunkView.Link(newChunkEntity);
            newChunkEntity.ReplaceLink(chunkView);
            newChunkEntity.IsActive = true;
            return newChunkEntity;
        }

        private GameEntity GetEntity(ChunkView chunkView)
        {
            var entityId = chunkView.EntityId!.Value;
            var entity = _game.GetEntityWithUid(entityId);
            entity.IsActive = true;

            return entity;
        }

        private void SetupTransform(
            ChunkView chunkView,
            Transform previousChunkEndAnchor = null,
            GameEntity previousChunk = null,
            Vector3? positionOffset = null
        )
        {
            var chunksRoot = _gameFieldProvider.GameField.ChunksRoot;
            var transform = chunkView.transform;

            if (previousChunk == null)
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.position = previousChunkEndAnchor!.position;
                transform.rotation = previousChunkEndAnchor!.rotation;
            }

            if (positionOffset.HasValue)
                transform.position += positionOffset.Value;

            transform.parent = chunksRoot;
        }

        private void ReplaceNode(
            GameEntity targetEntity, 
            Uid uid,
            EChunkType chunkType,
            GameEntity previousChunk = null)
        {
            ChunkNode chunkNode;

            if (previousChunk is null)
            {
                chunkNode = new ChunkNode(uid, chunkType);
            }
            else
            {
                chunkNode = new ChunkNode(uid, chunkType, previousChunk);
                var previousChunkNode = previousChunk.ChunkNode.Value;

                var i = previousChunkNode.NextNodes[0] == null ? 0 : 1;
                previousChunkNode.NextNodes[i] = chunkNode;
            }

            targetEntity.ReplaceChunkNode(chunkNode);
        }

        public async UniTask DeactivateChunk(ChunkNode rootNode, CancellationToken token)
        {
            if (rootNode == null)
                return;
            
            var entity = _game.GetEntityWithUid(rootNode.ChunkUid);
            if (entity != null)
                await DeactivateChunk(entity, token);
            await DeactivateChunk(rootNode.NextNodes[0], token);
            await DeactivateChunk(rootNode.NextNodes[1], token);
            rootNode.NextNodes[0] = null;
            rootNode.NextNodes[1] = null;
            rootNode.PreviousNode = null;
        }

        public async UniTask DeactivateChunk(GameEntity chunk, CancellationToken token)
        {
            if (chunk.IsDestroyed)
                return;

            if (!chunk.IsActive)
                return;

            _checkpointsPool.TryReturn(chunk.Uid.Value);

            var randomIndex = chunk.RandomIndex.Value;
            var chunkType = chunk.ChunkNode.Value.ChunkType;
            var view = (ChunkView)chunk.Link.View;
            view.Deactivate();

            _chunkSegmentService.UnregisterChunk(chunk);
            _distanceCalculationService.CalculateDistances(_chunkSegmentService.Segments);

            await DespawnChunk(chunkType, view, randomIndex, token);
            await DespawnWalls(view, token);
            await DespawnObstacles(view, chunk.IsSequential, token);

            //TODO:Reuse collectables entities
            _commandBuffer.CollectablesDespawn();

            ResetChunkEntity(chunk);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private async UniTask DespawnChunk(
            EChunkType chunkType, 
            ChunkView view, 
            int randomIndex,
            CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            _chunkPool.DespawnChunk(chunkType, view, randomIndex);
        }

        private async UniTask DespawnWalls(ChunkView view, CancellationToken token)
        {
            for (var i = 1; i < view.ChunkWalls.Count; i += 2)
                await _wallService.DespawnWallPair(view.ChunkWalls[i - 1], view.ChunkWalls[i], token);

            view.ChunkWalls.Clear();
        }

        private async UniTask DespawnObstacles(ChunkView view, bool isSequential, CancellationToken token)
        {
            if (isSequential)
                foreach (var o in view.Obstacles)
                    await _sequentialObstacleService.DespawnObstacle(o, token);
            else
                foreach (var o in view.Obstacles)
                    await _obstacleService.DespawnObstacle(o, token);

            foreach (var o in view.BuiltInObstacles)
                await _obstacleService.DespawnObstacle(o, token);

            view.Obstacles.Clear();
        }

        static void ResetChunkEntity(GameEntity chunkEntity)
        {
            chunkEntity.IsActive = false;
            chunkEntity.IsSequential = false;
            chunkEntity.IsVisited = false;

            if (chunkEntity.HasTutorialChunkData)
                chunkEntity.RemoveTutorialChunkData();
        }

        private async UniTask SetupBuildInObstacles(
            ChunkView chunkView,
            GameEntity chunkEntity,
            CancellationToken token
        )
        {
            var ownerUid = chunkEntity.Uid.Value;

            foreach (var obstacleGroupView in chunkView.BuiltInObstacles)
                await _obstacleService.SetupObstacle(
                    obstacleGroupView,
                    obstacleGroupView.ObstacleType,
                    ownerUid,
                    token
                );
        }

        private void SubscribeForViewWasDestroyed()
        {
            _disposable = _chunkPool.OnViewOfEntityDestroyed.Subscribe(DestroyChunkEntity);
        }

        private void DestroyChunkEntity(Uid uid)
        {
            DeleteObstacleEntityChildrenEntities(uid);
            DeleteObstacleEntityChildrenGroups(uid);

            var chunkEntity = _game.GetEntityWithUid(uid);
            chunkEntity.IsDestroyed = true;
        }

        private void DeleteObstacleEntityChildrenEntities(Uid groupUid)
        {
            var obstacleEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var obstacleEntity in obstacleEntities)
            {
                if (!obstacleEntity.IsObstacle)
                    continue;

                obstacleEntity.IsDestroyed = true;
            }
        }

        private void DeleteObstacleEntityChildrenGroups(Uid groupUid)
        {
            var obstacleEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var obstacleGroupEntity in obstacleEntities)
            {
                if (!obstacleGroupEntity.IsObstacleGroup)
                    continue;

                var uid = obstacleGroupEntity.Uid.Value;

                DeleteObstacleEntityChildrenEntities(uid);
                DeleteObstacleEntityChildrenGroups(uid);

                obstacleGroupEntity.IsDestroyed = true;
            }
        }
    }
}