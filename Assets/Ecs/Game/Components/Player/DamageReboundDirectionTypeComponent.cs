using Game.Utils.Rebound;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Player
{
    [Game]
    [Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public class DamageReboundDirectionTypeComponent : IComponent
    {
        public EReboundDirection Value;
    }
}