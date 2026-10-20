using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils;
using Game.Utils.Chunks;
using Game.Utils.ColorsUtils;
using Game.Utils.Obstacles;
using R3;
using UnityEngine;

namespace Game.Services.Obstacles.Impls
{
    public sealed class ObstacleService : IObstacleService, IDisposable
    {
        private readonly GameContext _game;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly IObstaclePool _obstaclePool;

        private IDisposable _disposable;

        public ObstacleService(
            GameContext game,
            IObstaclePool obstaclePool,
            ILinkedEntityRepository linkedEntityRepository
        )
        {
            _game = game;
            _obstaclePool = obstaclePool;
            _linkedEntityRepository = linkedEntityRepository;

            SubscribeForViewWasDestroyed();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async UniTask<(GameEntity obstacleGroupEntity, ObstacleGroupView obstacleGroupView)> CreateObstacle(
            EObstacleType obstacleType,
            Vector3 position,
            Vector3 offset,
            Quaternion rotation,
            CancellationToken token,
            Transform container = null
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            var (obstacleGroupView, randomIndex) = _obstaclePool.SpawnObstacle(obstacleType);

            ApplyTransform();

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var obstacleEntityGroup = GetOrCreateEntity(obstacleGroupView, obstacleType, randomIndex);

            return (obstacleEntityGroup, obstacleGroupView);

            void ApplyTransform()
            {
                var obstacleTransform = obstacleGroupView.transform;
                obstacleTransform.position = obstacleGroupView.IsOffsettable ? position + offset : position;
                obstacleTransform.rotation = rotation;
                obstacleTransform.parent = container;
            }
        }

        public async UniTask SetupObstacle(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            CancellationToken token
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            GetOrCreateEntity(obstacleGroupView, obstacleType, null);
        }

        public async UniTask SetupObstacle(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            Uid ownerUid,
            CancellationToken token
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var entity = GetOrCreateEntity(obstacleGroupView, obstacleType, null);
            entity.ReplaceOwner(ownerUid);
        }

        public async UniTask SetupWallObstacle(
            ObstacleGroupView obstacleGroupView,
            EChunkWallType wallType,
            Uid ownerUid,
            CancellationToken token
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var wallObstacle = GetOrCreateEntity(obstacleGroupView, EObstacleType.Wall, null);
            wallObstacle.ReplaceWallType(wallType);
            wallObstacle.ReplaceOwner(ownerUid);
        }

        public async UniTask DespawnObstacle(ObstacleGroupView obstacleGroupView, CancellationToken token)
        {
            if (!obstacleGroupView)
                return;

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var uid = obstacleGroupView.EntityId;
            obstacleGroupView.Deactivate();

            var obstacleGroupEntity = _game.GetEntityWithUid(uid!.Value);

            DeactivateChildrenGroups(uid!.Value);
            DeactivateChildrenEntities(uid!.Value);

            obstacleGroupEntity.IsActive = false;

            if (!obstacleGroupEntity.HasRandomIndex)
                return;

            var randomIndex = obstacleGroupEntity.RandomIndex.Value;
            var obstacleType = obstacleGroupEntity.ObstacleType.Value;

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            _obstaclePool.DespawnObstacle(obstacleType, obstacleGroupView, randomIndex);
        }

        private GameEntity GetOrCreateEntity(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            int? randomIndex
        )
        {
            return obstacleGroupView.EntityId == null
                ? CreateEntity(obstacleGroupView, obstacleType, randomIndex)
                : GetEntity(obstacleGroupView, obstacleType, randomIndex);
        }

        private GameEntity GetEntity(ObstacleGroupView obstacleGroupView, EObstacleType obstacleType, int? randomIndex)
        {
            var entityId = obstacleGroupView.EntityId!.Value;
            var entity = _game.GetEntityWithUid(entityId);
            entity.IsActive = true;

            if (randomIndex != null)
                entity.ReplaceRandomIndex(randomIndex.Value);

            entity.ReplaceObstacleType(obstacleType);

            obstacleGroupView.Activate();
            ActivateChildrenEntities(obstacleGroupView, obstacleType, randomIndex);
            ActivateChildrenGroups(obstacleGroupView, obstacleType, randomIndex);

            return entity;
        }

        private void ActivateChildrenEntities(
            ObstacleGroupView obstacleViews,
            EObstacleType obstacleType,
            int? randomIndex
        )
        {
            foreach (var obstacleView in obstacleViews.ChildrenObstacles)
            {
                var entityId = obstacleView.EntityId!.Value;
                obstacleView.Activate();
                var obstacleEntity = _game.GetEntityWithUid(entityId);
                obstacleEntity.IsActive = true;

                if (randomIndex != null)
                    obstacleEntity.ReplaceRandomIndex(randomIndex.Value);

                obstacleEntity.ReplaceObstacleType(obstacleType);
            }
        }

        private void ActivateChildrenGroups(
            ObstacleGroupView parentObstacleGroupView,
            EObstacleType obstacleType,
            int? randomIndex
        )
        {
            foreach (var obstacleGroupView in parentObstacleGroupView.ChildrenObstacleGroups)
            {
                var entityId = obstacleGroupView.EntityId!.Value;
                obstacleGroupView.Activate();
                var obstacleGroupEntity = _game.GetEntityWithUid(entityId);
                obstacleGroupEntity.IsActive = true;

                if (randomIndex != null)
                    obstacleGroupEntity.ReplaceRandomIndex(randomIndex.Value);

                obstacleGroupEntity.ReplaceObstacleType(obstacleType);

                ActivateChildrenEntities(obstacleGroupView, obstacleType, randomIndex);
                ActivateChildrenGroups(obstacleGroupView, obstacleType, randomIndex);
            }
        }

        private GameEntity CreateEntity(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            int? randomIndex
        )
        {
            var obstacleGroupEntity = _game.CreateEntity();
            var uid = UidGenerator.Next();
            obstacleGroupEntity.AddUid(uid);

            if (randomIndex.HasValue)
                obstacleGroupEntity.AddRandomIndex(randomIndex.Value);

            obstacleGroupEntity.AddObstacleType(obstacleType);
            obstacleGroupEntity.IsKillingInstantly = obstacleGroupView.IsKillingInstantly;
            obstacleGroupEntity.IsObstacleGroup = true;
            obstacleGroupEntity.IsActive = true;
            obstacleGroupView.Link(obstacleGroupEntity);
            obstacleGroupEntity.ReplaceLink(obstacleGroupView);
            _linkedEntityRepository.Add(obstacleGroupView.Hash, obstacleGroupEntity);
            obstacleGroupView.Activate();

            CreateChildrenEntities(obstacleGroupView, uid);
            CreateChildrenGroups(obstacleGroupView, uid, obstacleType);

            return obstacleGroupEntity;
        }

        private void CreateChildrenEntities(ObstacleGroupView obstacleViews, Uid ownerUid)
        {
            var isKillingInstantly = obstacleViews.IsKillingInstantly;

            foreach (var obstacleView in obstacleViews.ChildrenObstacles)
            {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
                if (obstacleView == null)
                    DebugUtility.Log(
                        $"{nameof(ObstacleService)} | The obstacle group with null obstacleViews: {obstacleViews.transform.parent.name}",
                        UtilsColors.ErrorColor
                    );
#endif
                var obstacleEntity = _game.CreateEntity();
                obstacleEntity.IsObstacle = true;
                obstacleEntity.IsActive = true;
                obstacleEntity.AddUid(UidGenerator.Next());
                obstacleEntity.AddOwner(ownerUid);
                obstacleEntity.IsKillingInstantly = isKillingInstantly;
                obstacleView.Link(obstacleEntity);
                obstacleEntity.ReplaceLink(obstacleView);
                obstacleView.Activate();
                _linkedEntityRepository.Add(obstacleView.Hash, obstacleEntity);
            }
        }

        private void CreateChildrenGroups(
            ObstacleGroupView obstacleGroupViews,
            Uid ownerUid,
            EObstacleType obstacleType
        )
        {
            foreach (var obstacleGroupView in obstacleGroupViews.ChildrenObstacleGroups)
            {
#if DEBUG_VERSION || (UNITY_EDITOR && !CHECK_FOR_RELEASE)
                if (obstacleGroupView == null)
                    DebugUtility.Log(
                        $"{nameof(ObstacleService)} | The obstacle group with null obstacleViews: {obstacleGroupViews.transform.parent.name}",
                        UtilsColors.ErrorColor
                    );
#endif

                var obstacleGroupEntity = _game.CreateEntity();
                var uid = UidGenerator.Next();
                obstacleGroupEntity.AddUid(uid);
                obstacleGroupEntity.IsKillingInstantly = obstacleGroupView.IsKillingInstantly;
                obstacleGroupEntity.AddObstacleType(obstacleType);
                obstacleGroupEntity.IsObstacleGroup = true;
                obstacleGroupEntity.IsActive = true;
                obstacleGroupEntity.AddOwner(ownerUid);
                obstacleGroupView.Link(obstacleGroupEntity);
                obstacleGroupEntity.ReplaceLink(obstacleGroupView);
                obstacleGroupView.Activate();
                _linkedEntityRepository.Add(obstacleGroupView.Hash, obstacleGroupEntity);

                CreateChildrenEntities(obstacleGroupView, uid);
                CreateChildrenGroups(obstacleGroupView, uid, obstacleType);
            }
        }

        private void DeactivateChildrenEntities(Uid groupUid)
        {
            var obstacleEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var obstacleEntity in obstacleEntities)
            {
                if (!obstacleEntity.IsObstacle)
                    continue;

                ((ObstacleView)obstacleEntity.Link.View).Deactivate();
                obstacleEntity.IsActive = false;
            }
        }

        private void DeactivateChildrenGroups(Uid groupUid)
        {
            var obstacleGroupEntities = _game.GetEntitiesWithOwner(groupUid);

            foreach (var obstacleGroupEntity in obstacleGroupEntities)
            {
                if (!obstacleGroupEntity.IsObstacleGroup)
                    continue;

                var uid = obstacleGroupEntity.Uid.Value;

                DeactivateChildrenGroups(uid);
                DeactivateChildrenEntities(uid);

                ((ObstacleGroupView)obstacleGroupEntity.Link.View).Deactivate();
                obstacleGroupEntity.IsActive = false;
            }
        }

        private void SubscribeForViewWasDestroyed()
        {
            _disposable = _obstaclePool.OnViewOfEntityDestroyed.Subscribe(DestroyObstacleEntity);
        }

        private void DestroyObstacleEntity(Uid uid)
        {
            DeleteObstacleEntityChildrenEntities(uid);
            DeleteObstacleEntityChildrenGroups(uid);

            var groupEntity = _game.GetEntityWithUid(uid);
            groupEntity.IsDestroyed = true;
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