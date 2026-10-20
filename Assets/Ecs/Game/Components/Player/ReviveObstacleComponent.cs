using Game.Utils.Obstacles;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    public class ReviveObstacleComponent : IComponent
    {
        public EObstacleType Value;
    }
}