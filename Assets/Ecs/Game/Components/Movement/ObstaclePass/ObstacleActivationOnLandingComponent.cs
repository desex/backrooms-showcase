using Ecs.Utils.ObstaclePass;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Movement.ObstaclePass
{
    [Game]
    public class ObstacleActivationOnLandingComponent : IComponent
    {
        public EPassObstacleType Value;
    }
}