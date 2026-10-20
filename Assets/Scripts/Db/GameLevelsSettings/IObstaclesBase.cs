using System.Collections.Generic;
using Ecs.Views.Linkable.Impl.Obstacles;
using Game.Utils.Obstacles;

namespace Db.GenerationSettings
{
    public interface IObstaclesBase
    {
        IReadOnlyDictionary<EObstacleType, IReadOnlyList<ObstacleGroupView>> Obstacles { get; }
        IReadOnlyDictionary<EObstacleType, ObstacleTypeInfo> ObstacleInfos { get; }
    }
}