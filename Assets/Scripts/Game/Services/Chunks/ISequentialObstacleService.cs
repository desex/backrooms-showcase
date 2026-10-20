using System.Threading;
using Cysharp.Threading.Tasks;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Obstacles;
using UnityEngine;

namespace Game.Services.Chunks
{
    public interface ISequentialObstacleService
    {
        UniTask<(GameEntity obstacleGroupEntity, ObstacleGroupView obstacleGroupView)> CreateObstacle(
            EObstacleType obstacleType,
            int obstacleID,
            Vector3 position,
            Quaternion rotation,
            CancellationToken token,
            Transform parent = null
        );

        UniTask DespawnObstacle(ObstacleGroupView obstacleGroupView, CancellationToken token);
    }
}