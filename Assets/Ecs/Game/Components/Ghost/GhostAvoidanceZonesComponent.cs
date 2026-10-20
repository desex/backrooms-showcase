using Game.Utils.Ghost;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Ghost
{
    [Game]
    public class GhostAvoidanceZonesComponent : IComponent
    {
        public GhostAvoidanceZonesData Value;
    }
}