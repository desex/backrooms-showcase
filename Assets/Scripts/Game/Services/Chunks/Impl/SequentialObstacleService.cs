using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Managers;
using Ecs.Utils;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Services.Obstacles;
using Game.Utils.Obstacles;
using R3;
using UnityEngine;

namespace Game.Services.Chunks.Impl
{
    //TODO: need class whole refactoring, little less local functions, methods queue etc
    public class SequentialObstacleService : ISequentialObstacleService, IDisposable
    {
        private readonly GameContext _game;
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly ISequentialObstaclePool _sequentialObstaclePool;

        private IDisposable _disposable;

        public SequentialObstacleService(
            ILinkedEntityRepository linkedEntityRepository,
            ISequentialObstaclePool sequentialObstaclePool,
            GameContext game
        )
        {
            _linkedEntityRepository = linkedEntityRepository;
            _sequentialObstaclePool = sequentialObstaclePool;
            _game = game;

            SubscribeForViewWasDestroyed();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async UniTask<(GameEntity obstacleGroupEntity, ObstacleGroupView obstacleGroupView)> CreateObstacle(
            EObstacleType obstacleType,
            int obstacleID,
            Vector3 position,
            Quaternion rotation,
            CancellationToken token,
            Transform parent = null
        )
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            var obstacleGroupView = _sequentialObstaclePool.SpawnObstacle(obstacleType, obstacleID);
            var obstacleTransform = obstacleGroupView.transform;

            ApplyTransform();

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            var obstacleEntityGroup = GetOrCreateEntity(obstacleGroupView, obstacleType, obstacleID);

            return (obstacleEntityGroup, obstacleGroupView);

            void ApplyTransform()
            {
                obstacleTransform.position = position;
                obstacleTransform.rotation = rotation;
                obstacleTransform.parent = parent;
            }
        }

        public async UniTask DespawnObstacle(ObstacleGroupView obstacleGroupView, CancellationToken token)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            if (!obstacleGroupView)
                return;

            obstacleGroupView.transform.parent = null;
            var uid = obstacleGroupView.EntityId;
            var obstacleGroupEntity = _game.GetEntityWithUid(uid!.Value);

            var prefabID = obstacleGroupEntity.SequencePrefabId.Value;

            DeactivateChildrenEntities(uid.Value);
            DeactivateChildrenGroups(uid.Value);

            obstacleGroupEntity.IsActive = false;
            obstacleGroupView.Deactivate();

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            _sequentialObstaclePool.DespawnObstacle(prefabID, obstacleGroupView);
        }

        private GameEntity GetOrCreateEntity(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            int prefabID
        )
        {
            return obstacleGroupView.EntityId == null
                ? CreateEntity(obstacleGroupView, obstacleType, prefabID)
                : GetEntity(obstacleGroupView, obstacleType, prefabID);
        }

        private GameEntity GetEntity(ObstacleGroupView obstacleGroupView, EObstacleType obstacleType, int prefabID)
        {
            var entityId = obstacleGroupView.EntityId!.Value;
            var entity = _game.GetEntityWithUid(entityId);
            entity.IsActive = true;

            entity.ReplaceObstacleType(obstacleType);
            entity.ReplaceSequencePrefabId(prefabID);

            ActivateChildrenEntities(obstacleGroupView, obstacleType, prefabID);
            ActivateChildrenGroups(obstacleGroupView, obstacleType, prefabID);
            obstacleGroupView.Activate();

            return entity;
        }

        private void ActivateChildrenEntities(ObstacleGroupView obstacleViews, EObstacleType obstacleType, int prefabID)
        {
            foreach (var obstacleView in obstacleViews.ChildrenObstacles)
            {
                var entityId = obstacleView.EntityId!.Value;
                var obstacleEntity = _game.GetEntityWithUid(entityId);
                obstacleEntity.IsActive = true;
                obstacleView.Activate();
                obstacleEntity.ReplaceObstacleType(obstacleType);
                obstacleEntity.ReplaceSequencePrefabId(prefabID);
            }
        }

        private void ActivateChildrenGroups(
            ObstacleGroupView parentObstacleGroupView,
            EObstacleType obstacleType,
            int prefabID
        )
        {
            foreach (var obstacleGroupView in parentObstacleGroupView.ChildrenObstacleGroups)
            {
                var entityId = obstacleGroupView.EntityId!.Value;
                var obstacleGroupEntity = _game.GetEntityWithUid(entityId);
                obstacleGroupEntity.IsActive = true;
                obstacleGroupView.Activate();
                obstacleGroupEntity.ReplaceObstacleType(obstacleType);
                obstacleGroupEntity.ReplaceSequencePrefabId(prefabID);

                ActivateChildrenEntities(obstacleGroupView, obstacleType, prefabID);
                ActivateChildrenGroups(obstacleGroupView, obstacleType, prefabID);
            }
        }

        private GameEntity CreateEntity(ObstacleGroupView obstacleGroupView, EObstacleType obstacleType, int prefabID)
        {
            var obstacleGroupEntity = _game.CreateEntity();
            var uid = UidGenerator.Next();
            obstacleGroupEntity.AddUid(uid);

            obstacleGroupEntity.AddObstacleType(obstacleType);
            obstacleGroupEntity.IsKillingInstantly = obstacleGroupView.IsKillingInstantly;
            obstacleGroupEntity.IsObstacleGroup = true;
            obstacleGroupEntity.ReplaceSequencePrefabId(prefabID);
            obstacleGroupView.Link(obstacleGroupEntity);
            obstacleGroupEntity.ReplaceLink(obstacleGroupView);
            obstacleGroupEntity.IsActive = true;
            obstacleGroupView.Activate();
            _linkedEntityRepository.Add(obstacleGroupView.Hash, obstacleGroupEntity);

            CreateChildrenEntities(obstacleGroupView, uid);
            CreateChildrenGroups(obstacleGroupView, uid, obstacleType);

            return obstacleGroupEntity;
        }

        private void CreateChildrenEntities(ObstacleGroupView obstacleViews, Uid ownerUid)
        {
            var isKillingInstantly = obstacleViews.IsKillingInstantly;

            foreach (var obstacleView in obstacleViews.ChildrenObstacles)
            {
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
            ObstacleGroupView parentObstacleGroupView,
            Uid ownerUid,
            EObstacleType obstacleType
        )
        {
            foreach (var obstacleGroupView in parentObstacleGroupView.ChildrenObstacleGroups)
            {
                var obstacleGroupEntity = _game.CreateEntity();
                var uid = UidGenerator.Next();
                obstacleGroupEntity.AddUid(uid);
                obstacleGroupEntity.IsKillingInstantly = obstacleGroupView.IsKillingInstantly;
                obstacleGroupEntity.AddObstacleType(obstacleType);
                obstacleGroupEntity.IsObstacleGroup = true;
                obstacleGroupEntity.AddOwner(ownerUid);
                obstacleGroupView.Link(obstacleGroupEntity);
                obstacleGroupEntity.ReplaceLink(obstacleGroupView);
                obstacleGroupEntity.IsActive = true;
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
            _disposable = _sequentialObstaclePool.OnViewOfEntityDestroyed.Subscribe(DestroyObstacleEntity);
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