using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Generation;
using Game.Utils.Obstacles;

namespace Game.Services.Obstacles
{
    public interface ISequentialObstaclePool : IViewPool<ObstacleGroupView>
    {
        ObstacleGroupView SpawnObstacle(EObstacleType obstacleType, int obstacleID);
        void DespawnObstacle(int obstacleID, ObstacleGroupView obstacle);
    }
}