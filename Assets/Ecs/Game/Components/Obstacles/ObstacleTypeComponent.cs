using Game.Utils.Obstacles;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Obstacles
{
    [Game]
    public class ObstacleTypeComponent : IComponent
    {
        public EObstacleType Value;
    }
}