using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Db.GameLevelsSettings.Impls;
using Ecs.Managers;
using Ecs.Views.Linkable.Impl.Chunk;
using Game.Providers;
using Game.Services.Obstacles;
using Game.Services.Providers;
using Game.Utils.Chunks;
using Game.Utils.Sequence;
using PdUtils.RandomProvider;
using R3;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Services.Chunks.Impl
{
    public sealed class WallService : IWallService, IDisposable
    {
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly GameContext _game;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IGenerationResourcesProvider _generationResourcesProvider;
        private readonly IObstacleService _obstacleService;
        private readonly IRandomProvider _randomProvider;
        private readonly ISequentialWallPool _sequentialWallPool;
        private readonly IWallPool _wallPool;

        private ObjectPool<GameObject> _lampsPool;

        public WallService(
            GameContext game,
            IWallPool wallPool,
            IRandomProvider randomProvider,
            IGameFieldProvider gameFieldProvider,
            IObstacleService obstacleService,
            IGenerationResourcesProvider generationResourcesProvider,
            ISequentialWallPool sequentialWallPool
        )
        {
            _game = game;
            _wallPool = wallPool;
            _randomProvider = randomProvider;
            _gameFieldProvider = gameFieldProvider;
            _obstacleService = obstacleService;
            _generationResourcesProvider = generationResourcesProvider;
            _sequentialWallPool = sequentialWallPool;

            InitializeLightPool();
            SubscribeForViewWasDestroyed();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async UniTask<(ChunkWallView leftWall, ChunkWallView rightWall)> CreateWallPair(
            EChunkWallType leftWallType,
            EChunkWallType rightWallType,
            CancellationToken token
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var (leftWall, leftWallRandomIndex) = _wallPool.SpawnWall(leftWallType);
            var leftWallEntity = GetOrCreateGenerativeWallEntity(leftWall, leftWallType, leftWallRandomIndex);

            await SetupBuildInObstacles(leftWall, leftWallType, leftWallEntity.Uid.Value, token);

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var (rightWall, rightWallRandomIndex) = _wallPool.SpawnWall(rightWallType);
            var rightWallEntity = GetOrCreateGenerativeWallEntity(rightWall, rightWallType, rightWallRandomIndex);

            await SetupBuildInObstacles(rightWall, rightWallType, rightWallEntity.Uid.Value, token);

            await SpawnLamps(leftWall.transform, leftWall, token);

            return (leftWall, rightWall);
        }

        public async UniTask<(ChunkWallView leftWall, ChunkWallView rightWall)> CreateSequenceWallPair(
            WallPairTypes wallPairTypes,
            CancellationToken token
        )
        {
            var leftWall = await SetupWall(wallPairTypes.LeftWall, token);
            var rightWall = await SetupWall(wallPairTypes.RightWall, token);

            await SpawnLamps(leftWall.transform, leftWall, token);

            return (leftWall, rightWall);

            async UniTask<ChunkWallView> SetupWall(SequenceWall sequenceWall, CancellationToken token)
            {
                var wallType = sequenceWall.WallType;
                var id = sequenceWall.WallPrefab.gameObject.GetInstanceID();

                var wall = _sequentialWallPool.SpawnWall(id);

                var wallEntity = CreateSequenceWallEntity(wall, wallType, id);

                await SetupBuildInObstacles(wall, wallType, wallEntity.Uid.Value, token);

                return wall;
            }
        }

        public async UniTask DespawnWallPair(ChunkWallView wall1, ChunkWallView wall2, CancellationToken token)
        {
            await DeactivateWall(wall1, token);
            await DeactivateWall(wall2, token);
        }

        public void ResetService()
        {
            InitializeLightPool();
        }

        private async UniTask SpawnLamps(Transform parentTransform, ChunkWallView wall, CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var lamps = _lampsPool.Get();

            lamps.transform.position = parentTransform.position;
            lamps.transform.rotation = parentTransform.rotation;
            lamps.transform.parent = parentTransform;

            wall.Lamps = lamps;
        }

        private async UniTask SetupBuildInObstacles(
            ChunkWallView wallView,
            EChunkWallType wallType,
            Uid ownerUid,
            CancellationToken token
        )
        {
            foreach (var obstacleGroupView in wallView.BuiltInObstacles)
                await _obstacleService.SetupWallObstacle(obstacleGroupView, wallType, ownerUid, token);
        }

        private void InitializeLightPool()
        {
            _lampsPool = new ObjectPool<GameObject>(
                () =>
                {
                    var lamps = _generationResourcesProvider.ChunkGenerationSettings.LampsPrefabs;
                    var randomIndex = _randomProvider.Range(0, lamps.Count);
                    return Object.Instantiate(lamps[randomIndex]);
                },
                view => { view.gameObject.SetActive(true); },
                view => { view.gameObject.SetActive(false); },
                view => { Object.Destroy(view.gameObject); },
                false,
                20,
                1000
            );
        }

        private GameEntity GetOrCreateWallEntity(ChunkWallView wall, EChunkWallType wallType)
        {
            return wall.EntityId == null ? CreateWallEntity(wall, wallType) : GetWallEntity(wall);
        }

        private GameEntity GetWallEntity(ChunkWallView wall)
        {
            var entityId = wall.EntityId!.Value;
            var wallEntity = _game.GetEntityWithUid(entityId);
            wallEntity.IsActive = true;

            return wallEntity;
        }

        private GameEntity CreateWallEntity(ChunkWallView wall, EChunkWallType wallType)
        {
            var wallEntity = _game.CreateEntity();
            var uid = UidGenerator.Next();
            wallEntity.AddUid(uid);
            wallEntity.AddWallType(wallType);
            wallEntity.IsActive = true;
            wall.Link(wallEntity);
            wallEntity.ReplaceLink(wall);

            return wallEntity;
        }

        private GameEntity GetOrCreateGenerativeWallEntity(ChunkWallView wall, EChunkWallType wallType, int randomIndex)
        {
            var wallEntity = GetOrCreateWallEntity(wall, wallType);
            wallEntity.ReplaceRandomIndex(randomIndex);

            return wallEntity;
        }

        private GameEntity CreateSequenceWallEntity(ChunkWallView wall, EChunkWallType wallType, int prefabId)
        {
            var wallEntity = GetOrCreateWallEntity(wall, wallType);
            wall.Activate();
            wallEntity.ReplaceSequencePrefabId(prefabId);

            return wallEntity;
        }

        private async UniTask DeactivateWall(ChunkWallView wall, CancellationToken token)
        {
            await ReleaseLamps(token);
            await ReleaseBuildInObstacles(wall, token);

            var wallEntity = _game.GetEntityWithUid(wall.EntityId!.Value);

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            if (wallEntity.HasSequencePrefabId)
            {
                var prefabId = wallEntity.SequencePrefabId.Value;
                _sequentialWallPool.DespawnWall(prefabId, wall);
            }
            else
            {
                var wallRandomIndex = wallEntity.RandomIndex.Value;
                var wallType = wallEntity.WallType.Value;
                _wallPool.DespawnWall(wallType, wall, wallRandomIndex);
            }

            wallEntity.IsActive = false;
            wall.Deactivate();

            if (wallEntity.HasRandomIndex)
                wallEntity.RemoveRandomIndex();

            if (wallEntity.HasSequencePrefabId)
                wallEntity.RemoveSequencePrefabId();

            return;

            async UniTask ReleaseLamps(CancellationToken token)
            {
                if (wall.Lamps == null)
                    return;

                await UniTask.Yield(PlayerLoopTiming.Update, token);

                var lamps = wall.Lamps;
                wall.Lamps = null;
                lamps.transform.parent = _gameFieldProvider.GameField.LampsRoot;
                _lampsPool.Release(lamps);
            }

            async UniTask ReleaseBuildInObstacles(ChunkWallView wallView, CancellationToken token)
            {
                foreach (var obstacleGroupView in wallView.BuiltInObstacles)
                    await _obstacleService.DespawnObstacle(obstacleGroupView, token);
            }
        }

        private void SubscribeForViewWasDestroyed()
        {
            _wallPool.OnViewOfEntityDestroyed
                .Subscribe(DestroyWallEntity)
                .AddTo(_disposable);

            _sequentialWallPool.OnViewOfEntityDestroyed
                .Subscribe(DestroyWallEntity)
                .AddTo(_disposable);
        }

        private void DestroyWallEntity(Uid uid)
        {
            DeleteWallEntityChildrenEntities(uid);
            DeleteWallEntityChildrenGroups(uid);

            var wallEntity = _game.GetEntityWithUid(uid);
            wallEntity.IsDestroyed = true;
        }

        private void DeleteWallEntityChildrenEntities(Uid groupUid)
        {
            var wallEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var wallEntity in wallEntities)
            {
                if (!wallEntity.IsObstacle)
                    continue;

                wallEntity.IsDestroyed = true;
            }
        }

        private void DeleteWallEntityChildrenGroups(Uid groupUid)
        {
            var obstacleEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var obstacleGroupEntity in obstacleEntities)
            {
                if (!obstacleGroupEntity.IsObstacleGroup)
                    continue;

                var uid = obstacleGroupEntity.Uid.Value;

                DeleteWallEntityChildrenEntities(uid);
                DeleteWallEntityChildrenGroups(uid);

                obstacleGroupEntity.IsDestroyed = true;
            }
        }
    }
}