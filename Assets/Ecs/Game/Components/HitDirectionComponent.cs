using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Event(EventTarget.Self)]
    public class HitDirectionComponent : IComponent
    {
        public EHitDirection Value;
    }
}