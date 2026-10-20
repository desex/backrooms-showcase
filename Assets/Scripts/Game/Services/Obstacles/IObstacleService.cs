using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Managers;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Chunks;
using Game.Utils.Obstacles;
using UnityEngine;

namespace Game.Services.Obstacles
{
    public interface IObstacleService
    {
        UniTask<(GameEntity obstacleGroupEntity, ObstacleGroupView obstacleGroupView)> CreateObstacle(
            EObstacleType obstacleType,
            Vector3 position,
            Vector3 offset,
            Quaternion rotation,
            CancellationToken token,
            Transform container = null
        );

        UniTask SetupObstacle(ObstacleGroupView obstacleGroupView, EObstacleType obstacleType, CancellationToken token);

        UniTask SetupObstacle(
            ObstacleGroupView obstacleGroupView,
            EObstacleType obstacleType,
            Uid ownerUid,
            CancellationToken token
        );

        UniTask SetupWallObstacle(
            ObstacleGroupView obstacleGroupView,
            EChunkWallType wallType,
            Uid ownerUid,
            CancellationToken token
        );

        UniTask DespawnObstacle(ObstacleGroupView obstacleView, CancellationToken token);
    }
}