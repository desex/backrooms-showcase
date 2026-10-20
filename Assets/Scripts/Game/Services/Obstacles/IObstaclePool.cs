using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Generation;
using Game.Utils.Obstacles;

namespace Game.Services.Obstacles
{
    public interface IObstaclePool : IViewPool<ObstacleGroupView>
    {
        (ObstacleGroupView obstacle, int randomIndex) SpawnObstacle(EObstacleType obstacleType);
        void DespawnObstacle(EObstacleType obstacleType, ObstacleGroupView obstacle, int randomIndex);
    }
}